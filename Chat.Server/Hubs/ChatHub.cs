using Chat.Server.Data;
using Chat.Server.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server.Hubs
{
	public class ChatHub : Hub<IChatHub>
	{
		private readonly ChatDbContext _context;

		public ChatHub(ChatDbContext context)
        {
			_context = context;
		}
		public override async Task OnConnectedAsync()
		{
			//var user = _context.Users.Where(x => x.ClientId.Equals(Context.ConnectionId)).FirstOrDefault();
			//await Groups.AddToGroupAsync(Context.ConnectionId, "status");
			//await Clients.Group("status").ReceiveStatus(user.Id, true);
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			//var user = _context.Users.Where(x => x.ClientId.Equals(Context.ConnectionId)).FirstOrDefault();
			//await Clients.Group("status").ReceiveStatus(user.Id, false);
		}

		public async Task SendMessage(Message message)
		{
			_context.Messages.Add(message);
			_context.SaveChanges();
			await Clients.All.ReceiveMessage(message);
		}

		//public async Task SendMessage(string message)
		//{
		//	var id = message.id;
		//	await Console.Out.WriteLineAsync(id);
		//	await Console.Out.WriteLineAsync();
		//}

		public async Task SendStatus(ulong userId, bool isOnline)
		{
			await Clients.Group("onlineStatus").ReceiveStatus(userId, isOnline);
		}

	}
}
