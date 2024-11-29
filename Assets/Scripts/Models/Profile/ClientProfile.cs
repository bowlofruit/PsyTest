using System;
using System.Collections.Generic;

namespace Models.Profile
{

	[Serializable]
	public class ClientProfile : IUserProfile
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }

		public List<TestResult> TestResults { get; set; }
		public string Notes { get; set; }
		public string ProfileLink { get; set; }
	}
}