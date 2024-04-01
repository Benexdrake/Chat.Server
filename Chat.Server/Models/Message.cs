namespace Chat.Server.Models
{
	public class Message
	{
		private ulong _id;
		private ulong _userId;
		private string _content;
		private DateTime _date;

        public Message()
        {
            _content = string.Empty;
			_date = DateTime.MinValue;
        }

		public Message(ulong id, ulong userId, string content, DateTime date)
		{
			_id = id;
			_userId = userId;
			_content = content;
			_date = date;
		}

		public ulong Id { get => _id; set => _id = value; }
		public ulong UserId { get => _userId; set => _userId = value; }
		public string Content { get => _content; set => _content = value; }
		public DateTime Date { get => _date; set => _date = value; }
	}
}
