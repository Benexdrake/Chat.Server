namespace Chat.Server.Models
{
	public class User
	{
		private string _id;
		private string _name;
		private string _avatar;
		private string _connectionId;

		public User()
        {
			_id = string.Empty;
			_connectionId = string.Empty;
            _name = string.Empty;
			_avatar = string.Empty;
        }

		public User(string id, string name, string avatar, string connectionId)
		{
			_id = id;
			_name = name;
			_avatar = avatar;
			_connectionId = connectionId;
		}

		public string Id { get => _id; set => _id = value; }
		public string ConnectionId { get => _connectionId; set => _connectionId = value; }
		public string Name { get => _name; set => _name = value; }
		public string Avatar { get => _avatar; set => _avatar = value; }
	}
}
