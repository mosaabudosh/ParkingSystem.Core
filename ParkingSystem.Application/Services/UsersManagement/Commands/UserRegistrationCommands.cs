using MediatR;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Application.Models;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;

namespace ParkingSystem.Application.Services.UsersManagement.Commands
{
    public class UserRegistrationCommands
    {
        public class Command : IRequest<object>
        {
            public UserModel UserModel { get; set; }
            public Command(UserModel userModel)
            {
                UserModel = userModel;
            }

            public class Handler(IUnitOfWork _unitOfWork, IAuthenticationService _authenticationService) : IRequestHandler<Command, object>
            {
                public async Task<object> Handle(Command request, CancellationToken cancellationToken)
                {
                    try
                    {
                        IQueryable<User> usersQueryable = _unitOfWork.GetRepository<User>().GetByExpression(x => x.Email == request.UserModel.Email, x => x.OrderBy(z => z.Email));
                        if (usersQueryable.Any())
                        {
                            return new { isSuccess = false , ErrorCode = "AlreadyExsist" };
                        }
                        User user = new User()
                        {
                            FullName = request.UserModel.FullName,
                            Phone = request.UserModel.Phone,
                            Email = request.UserModel.Email,
                            
                            UserRoles = new List<UserRole>(),
                            CreationDate = DateTime.UtcNow,
                        };
                        byte[] passwordHash, passwordSalt;
                        _authenticationService.HashPassword(request.UserModel.Password, out passwordHash, out passwordSalt);
                        user.PasswordHash = passwordHash;
                        user.PasswordSalt = passwordSalt;
                        user.UserRoles.Add(new UserRole
                        {
                            RoleId = request.UserModel.RoleId ?? 0,
                        });
                        
                        _unitOfWork.GetRepository<User>().Insert(user);
                        await _unitOfWork.CommitChanges();
                        return new { isSuccess = true };

                    }
                    catch (Exception ex)
                    {
                        while (ex.InnerException is not null)
                        {
                            ex = ex.InnerException;
                        }
                        return new { isSuccess = false , Message = ex.Message };
                    }
                }
            }

        }
    }
}
