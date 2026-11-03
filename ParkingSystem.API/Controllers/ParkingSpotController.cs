using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Application.Models;
using ParkingSystem.Application.Services.ParkingSpotsManagement.Query;

namespace ParkingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        [Route("parking-spots")]
        public async Task<ActionResult<List<ParkingSpotModel>>> GetParkingSpots()
        {
            try
            {
                return await _mediator.Send(new ParkingSpotsQuery.Query());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
