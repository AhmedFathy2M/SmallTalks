namespace SmallTalks.Services
{
	public class ChatService
	{
		private static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

		public static bool AddUserToList(string userToAdd)
		{
			lock(Users)
			{
				foreach(var user in Users) 
				{
					if(user.Key.ToLower() == userToAdd.ToLower())
					{
						return false;
					}
				}
				Users.Add(userToAdd, null);
				return true;
			}
		}

		public static void AddUserConnectionId(string user, string connectionId)
		{
			lock (Users)
			{
				
					if (Users.ContainsKey(user))
					{
					 Users[user] = connectionId;	
					}
					else
				{
					Users.Add(user, connectionId);
				}
				
				
			}
		}

		public static string GetUserByConnectionId(string connectionId)
		{
			lock (Users)
			{

				return Users.Where(x => x.Value == connectionId).Select(x => x.Key).FirstOrDefault();


			}
		}

		public static string GetConnectionIdByUser(string user)
		{
			lock (Users)
			{

				var currentUser= Users.Where(x => x.Key == user).Select(x => x.Value).FirstOrDefault();
				return currentUser;


			}
		}

		public static void RemoveUserFromList(string user)
		{
			lock (Users)
			{

				if(Users.ContainsKey(user))
				{
					Users.Remove(user);
				}


			}
		}

		public static string[] GetOnlineUsers()
		{
			lock (Users)
			{
				return Users.OrderBy(x=> x.Key).Select(x=>x.Key).ToArray();
			}
		}
	}
}
