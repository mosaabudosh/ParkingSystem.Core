

using MediatR;
using ParkingSystem.Application.Models;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;

namespace ParkingSystem.Application.Services.ParkingSpotsManagement.Query
{
    public class ParkingSpotsQuery
    {
        public class Query : IRequest<List<ParkingSpotModel>>
        {

        }

        public class Handler(IUnitOfWork _unitOfWork) : IRequestHandler<Query, List<ParkingSpotModel>>
        {
            public async Task<List<ParkingSpotModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    List<ParkingSpotModel> parkingSpots = _unitOfWork.GetRepository<ParkingSpot>().GetByExpressionAsNoTracking().Select(s => new ParkingSpotModel
                    {
                        ParkingSpotId = s.ParkingSpotId,
                        SpotNumber = s.SpotNumber,
                        ParkingSpotSlots = s.ParkingSpotSlots.Select(p => new ParkingSpotSlotModel()
                        {
                            EndTime = p.EndTime,
                            StartTime = p.StartTime,
                            IsOccupied = p.IsOccupied,
                            ParkingSpotId = p.ParkingSpotId,
                            ParkingSpotSlotId = p.ParkingSpotSlotId
                        }).ToList()
                    }).ToList();

                    return parkingSpots;
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
