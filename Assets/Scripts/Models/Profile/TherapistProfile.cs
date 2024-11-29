using System;
using System.Collections.Generic;

namespace Models.Profile
{
	[Serializable]
	public class TherapistProfile : IUserProfile
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public string LastName { get; set; }
		public string ContactInfo { get; set; }
		public int ExperienceYears { get; set; }
		public List<string> Certificates { get; set; }
	}
}