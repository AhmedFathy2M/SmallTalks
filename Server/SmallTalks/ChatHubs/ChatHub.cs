using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SmallTalks.Dtos;
using SmallTalks.Services;

namespace SmallTalks.ChatHubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(MessageDto message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SmallTalk");
			await Clients.Caller.SendAsync("UserConnected");
			await DisplayOnlineUsers();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SmallTalk");
            var user = ChatService.GetUserByConnectionId(Context.ConnectionId);
            ChatService.RemoveUserFromList(user);
            await DisplayOnlineUsers();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddUserConnectionId(string displayName)
        {
            ChatService.AddUserConnectionId(displayName, Context.ConnectionId);
            await DisplayOnlineUsers();
        }

        public async Task ReceiveMessage(MessageDto message)
        {
            await Clients.Group("SmallTalk").SendAsync("NewMessage", message);
        }

        public async Task CreatePrivateChat(MessageDto message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            await Groups.AddToGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectionId = ChatService.GetConnectionIdByUser(message.To);
            await Groups.AddToGroupAsync(toConnectionId, privateGroupName);
            await Clients.Client(toConnectionId).SendAsync("OpenPrivateChat", message);
        }

        public async Task ReceivePrivateMessage(MessageDto message)
        {
            string privateGroupName = GetPrivateGroupName(message.From, message.To);
            await Clients.Group(privateGroupName).SendAsync("NewPrivateMessage", message);
        }

        public async Task RemovePrivateChat(string from, string to)
        {
            string privateGroupName = GetPrivateGroupName(from, to);
            await Clients.Group(privateGroupName).SendAsync("ClosePrivateChat");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, privateGroupName);
            var toConnectionId = ChatService.GetConnectionIdByUser(to);
            await Groups.RemoveFromGroupAsync(toConnectionId, privateGroupName);
        }

		
		public async Task ReceiveAudio(MessageDto message)
        {
            await Clients.Group("SmallTalk").SendAsync("NewAudio", message);
        }
        public async Task ReceiveImage(MessageDto message)
        {
            await Clients.Group("SmallTalk").SendAsync("NewImage", message);
        }

		

		private async Task DisplayOnlineUsers()
        {
            var onlineUsers = ChatService.GetOnlineUsers();
            await Clients.Groups("SmallTalk").SendAsync("OnlineUsers", onlineUsers);
        }

        private string GetPrivateGroupName(string from, string to)
        {
            var stringCompare = string.CompareOrdinal(from, to) < 0;
            return stringCompare ? $"{from}-{to}" : $"{to}-{from}";
        }
    }
}