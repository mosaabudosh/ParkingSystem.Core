

using MediatR;
using ParkingSystem.Application.Models;
using ParkingSystem.Application.Services.ParkingSpotsManagement.Commands;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;
using static ParkingSystem.Domain.Enum.Enums;

namespace ParkingSystem.Application.Services.BookingsManagement.Commands
{
    public class BookingCreationCommands
    {
        public class Command : IRequest<object>
        {
            public BookingModel BookingModel { get; set; }
            public Command(BookingModel bookingModel)
            {
                BookingModel = bookingModel;
            }
        }

        public class Handler(IUnitOfWork _unitOfWork, IMediator _mediator) : IRequestHandler<Command, object>
        {
            public async Task<object> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    Booking booking = new Booking()
                    {
                       ParkingSpotId = request.BookingModel.ParkingSpotId,
                       BookingStatusId = request.BookingModel.BookingStatusId,
                       UserId = new Guid(request.BookingModel.UserId),
                       CreationDate = DateTime.UtcNow,
                       Payment = new Payment()
                       {
                           Amount = request.BookingModel.Payment.Amount,
                           PaymentMethod = request.BookingModel.Payment.PaymentMethod,
                           PaymentTime = DateTime.UtcNow
                       }
                    };
                    ParkingSpotModel parkingSpotModel = new ParkingSpotModel()
                    {
                        ParkingSpotId = request.BookingModel.ParkingSpotId,
                        ParkingSpotSlots = request.BookingModel.ParkingSpotSlots
                    };
                    _unitOfWork.GetRepository<Booking>().Insert(booking);
                    dynamic parkingSpotUpdationResponse = await _mediator.Send(new ParkingSpotUpdationCommands.Command(parkingSpotModel, false));
                    if (!parkingSpotUpdationResponse.isSuccess)
                    {
                        return new { isSuccess = false, Message = parkingSpotUpdationResponse.Message };
                    }
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
