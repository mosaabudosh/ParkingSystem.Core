

using MediatR;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;
using static ParkingSystem.Domain.Enum.Enums;

namespace ParkingSystem.Application.Services.BookingsManagement.Query
{
    public class SameSpotAlreadyBookedQuery
    {
        public class Query : IRequest<object>
        {
            public Guid UserId { get; set; }
            public int ParkingSpotId { get; set; }
            public Query(Guid userId, int parkingSpotId)
            {
                UserId = userId;
                ParkingSpotId = parkingSpotId;
            }
        }

        public class Handler(IUnitOfWork _unitOfWork) : IRequestHandler<Query, object>
        {
            public async Task<object> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    Booking exsistBooking = _unitOfWork.GetRepository<Booking>().GetByExpressionAsNoTracking(p => p.UserId == request.UserId && p.ParkingSpotId == request.ParkingSpotId && p.BookingStatusId == (int)BookingStatuses.Active).FirstOrDefault();
                    if(exsistBooking is not null)
                    {
                        return new { isSuccess = true, isExsist = true };
                    }
                    return new { isSuccess = true, isExsist = false};
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
