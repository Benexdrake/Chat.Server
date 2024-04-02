namespace Chat.Server.Models
{
	public class User
	{
		private string _id;
		private string _name;
		private string _avatar;

		

        public User()
        {
            _name = string.Empty;
			_avatar = string.Empty;
        }

		public User(string id, string name, string avatar)
		{
			_id = id;
			_name = name;
			_avatar = avatar;
		}

		public string Id { get => _id; set => _id = value; }
		public string Name { get => _name; set => _name = value; }
		public string Avatar { get => _avatar; set => _avatar = value; }
	}
}
