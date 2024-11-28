using System;

namespace PsyTest.Profile
{
	[Serializable]
	public class UserProfileInfo
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
	}
}