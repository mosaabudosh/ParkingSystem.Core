

using MediatR;
using ParkingSystem.Application.Models;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;

namespace ParkingSystem.Application.Services.UsersManagement.Commands
{
    public class FineOfParkedNotValidBookingCommands
    {
        public class Command : IRequest<object>
        {
            public FineOfParkedNotValidBookingModel FineOfParkedNotValidBookingModel { get; set; }
            public Command(FineOfParkedNotValidBookingModel fineOfParkedNotValidBookingModel)
            {
                FineOfParkedNotValidBookingModel = fineOfParkedNotValidBookingModel;
            }
        }

        public class Handler(IUnitOfWork _unitOfWork) : IRequestHandler<Command, object>
        {
            public async Task<object> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    User user = _unitOfWork.GetRepository<User>().GetByExpressionAsNoTracking(p => p.UserId == new Guid(request.FineOfParkedNotValidBookingModel.UserId)).FirstOrDefault();
                    if (user.TotalFines.HasValue)
                    {
                        user.TotalFines += request.FineOfParkedNotValidBookingModel.Fines;
                    }
                    else
                    {
                        user.TotalFines = request.FineOfParkedNotValidBookingModel.Fines;
                    }
                    user.HasFines = true;
                    _unitOfWork.GetRepository<User>().Update(user);

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
