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
			await Console.Out.WriteLineAsync();
			//var user = _context.Users.FirstOrDefault(x => x.ConnectionId.Equals(Context.ConnectionId));
			//if (user != null)
			//{
			//	var avatar = "./avatar.jpg";
			//	if (user.Avatar != "")
			//		avatar = $"https://cdn.discordapp.com/avatars/{user.Id}/{user.Avatar}.webp";


			//	var m = new
			//	{
			//		content = "joined Chat",
			//		date = "",
			//		avatarUrl = avatar,
			//		username = user.Name
			//	};
			//	await Clients.All.ReceiveMessage(m);
			//}
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			//var user = _context.Users.FirstOrDefault(x => x.ConnectionId.Equals(Context.ConnectionId));
			//if (user != null)
			//{
			//	var avatar = "./avatar.jpg";
			//	if (user.Avatar != "")
			//		avatar = $"https://cdn.discordapp.com/avatars/{user.Id}/{user.Avatar}.webp";


			//	var m = new
			//	{
			//		content = "Exit Chat",
			//		date = "",
			//		avatarUrl = avatar,
			//		username = user.Name
			//	};
			//	await Clients.All.ReceiveMessage(m);
			//}
		}

		public async Task SendMessage(Message message)
		{
			_context.Messages.Add(message);
			_context.SaveChanges();

			var user = _context.Users.FirstOrDefault(x => x.Id == message.UserId);
			if (user != null)
			{
				var avatar = "./avatar.jpg";
				if (user.Avatar != "")
					avatar = $"https://cdn.discordapp.com/avatars/{user.Id}/{user.Avatar}.webp";
				

				var m = new {
					content= message.Content,
					date= message.Date,
					avatarUrl= avatar,
					username=user.Name
				};
				await Clients.All.ReceiveMessage(m);
			}
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
