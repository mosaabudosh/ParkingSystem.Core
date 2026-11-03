using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ParkingSystem.Application.Interfaces;
using ParkingSystem.Application.Models;
using ParkingSystem.Application.Services;
using ParkingSystem.Application.Services.BookingsManagement.Commands;
using ParkingSystem.Application.Services.BookingsManagement.Query;
using ParkingSystem.Application.Services.ParkingSpotsManagement.Query;

namespace ParkingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IMediator _mediator, IHubContext<NotificationHub> _notificationHub, ICheckTheDurationOfReservations _checkTheDurationOfReservations) : ControllerBase
    {

        [HttpPost]
        [Route("create-booking")]
        public async Task<ActionResult<object>> CreateBooking([FromBody] BookingModel bookingModel)
        {
            try
            {
                object bookingCreationResponse = await _mediator.Send(new BookingCreationCommands.Command(bookingModel));
                await _notificationHub.Clients.Group("ParkingSystemTeam").SendAsync("ParkingSystemTeam", $"Parking has been booked success.");
                return bookingCreationResponse;
            }
            catch (Exception ex)
            {
                return new { IsSuccess = false, Message = ex.Message };
            }
        }

        [HttpGet]
        [Route("check-spot-already-booked")]
        public async Task<ActionResult<object>> CheckSameSpotAlreadyBooked(string userId, int parkingSpotId)
        {
            try
            {
                return await _mediator.Send(new SameSpotAlreadyBookedQuery.Query(new Guid(userId), parkingSpotId));
            }
            catch (Exception ex)
            {
                return new { IsSuccess = false, Message = ex.Message };
            }
        }

        [HttpGet]
        [Route("check-duration")]
        public async Task<ActionResult<object>> CheckTheDuration()
        {
            try
            {
                _checkTheDurationOfReservations.CheckTheDuration(null,null);
                return Ok();
            }
            catch (Exception ex)
            {
                return new { IsSuccess = false, Message = ex.Message };
            }
        }

        [HttpGet]
        [Route("not-valid-bookings")]
        public async Task<ActionResult<List<NotValidBookingModel>>> GetNotValidBookings()
        {
            try
            {
                return await _mediator.Send(new NotValidBookingQuery.Query());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
