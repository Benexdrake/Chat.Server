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
			await Clients.All.ReceiveStatus(true);
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await Clients.All.ReceiveStatus(false);
		}

		public async Task SendMessage(Message message)
		{
			_context.Messages.Add(message);
			await Clients.All.ReceiveMessage(message);
		}

	}
}
