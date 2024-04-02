namespace Chat.Server.Models
{
	public class Message
	{
		private ulong _id;
		private string _userId;
		private string _content;
		private string _date;

        public Message()
        {
            _content = string.Empty;
			_date = string.Empty;
			UserId = string.Empty;
        }

		public Message(ulong id, string userId, string content, string date)
		{
			_id = id;
			_userId = userId;
			_content = content;
			_date = date;
		}

		public ulong Id { get => _id; set => _id = value; }
		public string UserId { get => _userId; set => _userId = value; }
		public string Content { get => _content; set => _content = value; }
		public string Date { get => _date; set => _date = value; }
	}
}
