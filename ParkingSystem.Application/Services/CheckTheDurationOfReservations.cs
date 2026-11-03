

using Microsoft.AspNetCore.SignalR;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Domain.IRepositories;
using System.Timers;
using static ParkingSystem.Domain.Enum.Enums;

namespace ParkingSystem.Application.Services
{
    public class CheckTheDurationOfReservations : ICheckTheDurationOfReservations
    {
        IUnitOfWork _unitOfWork;
        IHubContext<NotificationHub> _notificationHub;
        private readonly System.Timers.Timer _timer;

        public CheckTheDurationOfReservations(IUnitOfWork unitOfWork, IHubContext<NotificationHub> notificationHub)
        {
            _unitOfWork = unitOfWork;
            _notificationHub = notificationHub;
            this.CheckTheDuration(null,null);

            _timer = new System.Timers.Timer(900000);
            _timer.Elapsed += CheckTheDuration; 
            _timer.AutoReset = false; 
            _timer.Start();
        }

        public async void CheckTheDuration(object sender, ElapsedEventArgs e)
        {
            try
            {
                DateTime today = DateTime.UtcNow;
                List<ParkingSpotSlot> bookings = _unitOfWork.GetRepository<ParkingSpotSlot>().GetByExpressionAsNoTracking(p => p.IsOccupied && p.StartTime < today && p.EndTime > today,includedEntities:new List<System.Linq.Expressions.Expression<Func<ParkingSpotSlot, object>>> { z => z.ParkingSpot, z => z.ParkingSpot.Bookings.Where(x => x.BookingStatusId == (int)BookingStatuses.Active) }).ToList();
                if (bookings.Any())
                {
                    dynamic data = bookings.SelectMany(p => p.ParkingSpot.Bookings.Select(s => new
                    {
                        UserId = s.UserId,
                        Minutes = p.EndTime.Subtract(today).TotalMinutes,
                    })).GroupBy(x => new {x.UserId ,x.Minutes }).Where(a => a.Key.Minutes <= 15).ToList();
                    foreach (var item in data) 
                    {
                        await _notificationHub.Clients.Group("ParkingSystemTeam").SendAsync($"{item.Key.UserId}", $"For your information their parking will expire within {item.Key.Minutes?.ToString("0.00")} minutes.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
