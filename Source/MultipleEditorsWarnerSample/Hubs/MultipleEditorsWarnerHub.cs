using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultipleEditorsWarnerSample.Hubs
{
    public partial class MultipleEditorsWarnerHub : Hub
    {
        public const string HubPath = "/multipleeditorswarnerhub";

        public const string CallbackUsersChanged = "UsersChanged";

        public const string CallbackMessageReceived = "ReceiveMessage";
        
        private static readonly UserGroupManager GroupManager = new UserGroupManager();
        
        public async Task Connect(string groupName, string userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var group = GroupManager.AddUser(groupName, Context.ConnectionId, userName);

            await Clients.Groups(groupName).SendAsync(CallbackUsersChanged, group.UserChangeCount, group.Users.Select(a => a.Value).OrderBy(a => a).ToList());

            foreach (var message in group.Messages.OrderByDescending(a => a.TimeStamp))
            {
                await Clients.Caller.SendAsync(CallbackMessageReceived, message.UserName, message.TimeStamp, message.Message);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Group? group = GroupManager.RemoveUser(Context.ConnectionId);

            if (group != null)
            {
                await Clients.Groups(group.GroupName).SendAsync(CallbackUsersChanged, group.UserChangeCount, group.Users.Select(a => a.Value).OrderBy(a => a).ToList());
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string groupName, string userName, string message)
        {
            DateTime timestamp = DateTime.Now;

            GroupManager.AddMessage(groupName, userName, timestamp, message);

            await Clients.Groups(groupName).SendAsync(CallbackMessageReceived, userName, timestamp, message);
        }
    }
}
