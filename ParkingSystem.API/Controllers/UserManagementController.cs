using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkingSystem.API.Model;
using ParkingSystem.Application.Models;
using ParkingSystem.Application.Services.UsersManagement.Commands;

namespace ParkingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController(IMediator _mediator) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<object>> Registration([FromBody] UserModel userModel)
        {
            try
            {
                if(userModel.Password != userModel.RepeatPassword)
                {
                    return new { IsSuccess = false, Message = "CheckPassword" };
                }
                return await _mediator.Send(new UserRegistrationCommands.Command(userModel));
            }
            catch (Exception ex)
            {
                return new { IsSuccess = false, Message = ex.Message };
            }
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult<object>> Login(LoginModel loginModel)
        {
            try
            {
                return await _mediator.Send(new UserLoginCommands.Command(loginModel.Email, loginModel.Password));
            }
            catch (Exception ex)
            {
                return new { isSuccess = false, Message = ex.Message };
            }
        }


        [AllowAnonymous]
        [Route("logout")]
        [HttpPost]
        public async Task<ActionResult<object>> LogOut([FromBody] LogoutModel logoutModel)
        {
            try
            {
                return await _mediator.Send(new UserLogOutCommands.Command(logoutModel.Email));
            }
            catch (Exception ex)
            {
                return new { isSuccess = false, Message = ex.Message };
            }
        }

        [Route("fine-parked-not-valid-booking")]
        [HttpPut]
        public async Task<ActionResult<object>> FineOfParkedNotValidBooking([FromBody] FineOfParkedNotValidBookingModel fineOfParkedNotValidBookingModel)
        {
            try
            {
                return await _mediator.Send(new FineOfParkedNotValidBookingCommands.Command(fineOfParkedNotValidBookingModel));
            }
            catch (Exception ex)
            {
                return new { isSuccess = false, Message = ex.Message };
            }
        }
    }
}
