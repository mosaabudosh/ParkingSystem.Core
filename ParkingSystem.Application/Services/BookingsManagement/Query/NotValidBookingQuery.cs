using MediatR;
using ParkingSystem.Application.Models;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;
using static ParkingSystem.Domain.Enum.Enums;


namespace ParkingSystem.Application.Services.BookingsManagement.Query
{
    public class NotValidBookingQuery
    {
        public class Query : IRequest<List<NotValidBookingModel>>
        {
        
        }

        public class Handler(IUnitOfWork _unitOfWork) : IRequestHandler<Query, List<NotValidBookingModel>>
        {
            public async Task<List<NotValidBookingModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    DateTime today = DateTime.UtcNow;
                    List<ParkingSpotSlot> notValidBookingQuerys = _unitOfWork.GetRepository<ParkingSpotSlot>().GetByExpression(p => p.IsOccupied && p.EndTime < today, includedEntities: new List<System.Linq.Expressions.Expression<Func<ParkingSpotSlot, object>>> { z => z.ParkingSpot, z => z.ParkingSpot.Bookings.Where(x => x.BookingStatusId == (int)BookingStatuses.Active) }).ToList();

                    List<Guid> userIds = notValidBookingQuerys.SelectMany(p => p.ParkingSpot.Bookings.Select(s => s.UserId)).ToList();
                     _unitOfWork.GetRepository<User>().GetByExpression(p => userIds.Contains(p.UserId)).ToList();

                    List <NotValidBookingModel> notValidBookings = notValidBookingQuerys.SelectMany(p => p.ParkingSpot.Bookings.GroupBy(x => x.UserId).Select(s => new NotValidBookingModel
                    {
                        UserId = s.Key.ToString(),
                        BookingId = s.FirstOrDefault().BookingId.ToString(),
                        Date = s.FirstOrDefault().CreationDate,
                        UserFullName = s.FirstOrDefault().User.FullName,
                        Minutes = today.Subtract(p.EndTime).TotalMinutes,
                        UserHasFines = s.FirstOrDefault().User.HasFines
                    })).ToList();
                    return notValidBookings;
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
