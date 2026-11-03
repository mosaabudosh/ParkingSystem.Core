
using MediatR;
using ParkingSystem.Application.Models;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;

namespace ParkingSystem.Application.Services.ParkingSpotsManagement.Commands
{
    public class ParkingSpotUpdationCommands
    {
        public class Command : IRequest<object>
        {
            public ParkingSpotModel ParkingSpotModel { get; set; }
            public bool? IsCommit { get; set; }
            public Command(ParkingSpotModel parkingSpotModel, bool? isCommit = true)
            {
                ParkingSpotModel = parkingSpotModel;
                IsCommit = isCommit;
            }
        }

        public class Handler(IUnitOfWork _unitOfWork) : IRequestHandler<Command, object>
        {
            public async Task<object> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    List<int> parkingSpotSlotIds = request.ParkingSpotModel.ParkingSpotSlots.Select(s => s.ParkingSpotSlotId).ToList();
                    List<ParkingSpotSlot> parkingSpotSlots = _unitOfWork.GetRepository<ParkingSpotSlot>().GetByExpressionAsNoTracking(p => parkingSpotSlotIds.Contains(p.ParkingSpotSlotId)).ToList();
                    if(parkingSpotSlots.Any(x => x.IsOccupied))
                    {
                        return new { isSuccess = false, Message = "There is a reserved period." };
                    }
                    parkingSpotSlots = parkingSpotSlots.Select(s =>
                    {
                        s.IsOccupied = true;
                        return s;
                    }).ToList();
                    _unitOfWork.GetRepository<ParkingSpotSlot>().UpdateRange(parkingSpotSlots);

                    if (request.IsCommit.HasValue && request.IsCommit.Value)
                    {
                        await _unitOfWork.CommitChanges();
                    }
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
