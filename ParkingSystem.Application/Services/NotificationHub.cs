using Microsoft.AspNetCore.SignalR;


namespace ParkingSystem.Application.Services
{
    public class NotificationHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            string group = Context.GetHttpContext().Request.Query["group"];
            Groups.AddToGroupAsync(Context.ConnectionId, group);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string group = Context.GetHttpContext().Request.Query["group"];
            Groups.RemoveFromGroupAsync(Context.ConnectionId, group);

            return base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId() => Context.ConnectionId;

        public async Task NotificationsBroadcasting(string notificationGroup, Object data)
        {
            await Clients.Group($"{notificationGroup}").SendAsync($"{notificationGroup}", data);
        }
    }
}
