using MediatR;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;


namespace ParkingSystem.Application.Services.UsersManagement.Commands
{
    public class UserLogOutCommands
    {
        public class Command : IRequest<object>
        {
            public string Email;

            public Command(string email)
            {
                Email = email;
            }
            public class Handler(IUnitOfWork _unitOfWork) : IRequestHandler<Command, object>
            {
                public async Task<object> Handle(Command request, CancellationToken cancellationToken)
                {
                    try
                    {
                        IQueryable<User> users = _unitOfWork.GetRepository<User>().GetByExpressionAsNoTracking(x => x.Email == request.Email);
                        User user = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(users);
                        if (user is null)
                        {
                            return new { isSuccess = false, ErrorCode = "InvalidEmail" };
                        }
                        user.LogOutDate = DateTime.UtcNow;
                        user.AccessToken = string.Empty;
                        await _unitOfWork.CommitChanges();
                        return new { isSuccess = true };
                    }
                    catch (Exception ex)
                    {
                        while (ex.InnerException is not null)
                        {
                            ex = ex.InnerException;
                        }
                        return new { isSuccess = false, Message = ex.Message };
                    }
                }
            }
        }
    }
}
