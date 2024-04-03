using Chat.Server.Models;

namespace Chat.Server.Hubs
{
	public interface IChatHub
	{
		Task ReceiveMessage(Message message);
		Task ReceiveMessage(object message);
		Task ReceiveStatus(ulong userId,bool status);
	}
}
