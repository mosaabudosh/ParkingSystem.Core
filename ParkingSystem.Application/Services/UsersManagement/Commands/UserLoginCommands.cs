using MediatR;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Application.Models;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;

namespace ParkingSystem.Application.Services.UsersManagement.Commands
{
    public class UserLoginCommands
    {
        public class Command : IRequest<UserModel>
        {
            public string Email;
            public string Password;
            public Command(string email, string password)
            {
                Email = email;
                Password = password;
            }
            public class Handler(IUnitOfWork _unitOfWork,IAuthenticationService _authenticationService, ITokenHandler _tokenHandler, IMediator _mediator) : IRequestHandler<Command, UserModel>
            {
                public async Task<UserModel> Handle(Command request, CancellationToken cancellationToken)
                {
                    try
                    {
                        IQueryable<User> users = _unitOfWork.GetRepository<User>().GetByExpression(x => x.Email == request.Email , includedEntities: new List<System.Linq.Expressions.Expression<Func<User, object>>>
                        {
                            z => z.UserRoles,
                        });
                        User user = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(users);
                        if (user is null)
                        {
                            throw new Exception("Invalid Email Or Password");
                        }
                        if (!_authenticationService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                        {
                            throw new Exception("Invalid Email Or Password");
                        }
                        user = _tokenHandler.BuildUserTokens(user);
                        user.LoginDate = DateTime.UtcNow;
                        await _unitOfWork.CommitChanges();
                        UserModel userModel = new UserModel()
                        {
                            UserId = user.UserId.ToString(),
                            AccessToken = user.AccessToken,
                            Email = user.Email,
                            FullName = user.FullName,
                            Phone = user.Phone,
                            RoleIds = user.UserRoles.Select(s => s.RoleId).ToList(),
                            RoleId = user.UserRoles.Select(s => s.RoleId).FirstOrDefault()
                        };
                       
                        return userModel;
                    }
                    catch (Exception ex)
                    {
                        while (ex.InnerException is not null)
                        {
                            ex = ex.InnerException;
                        }
                        throw new Exception(ex.Message);

                    }
                }

                
            }
        }
    }
}
