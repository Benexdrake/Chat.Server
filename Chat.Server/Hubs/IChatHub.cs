using Chat.Server.Models;

namespace Chat.Server.Hubs
{
	public interface IChatHub
	{
		Task ReceiveMessage(Message message);
		Task ReceiveStatus(bool status);
	}
}
