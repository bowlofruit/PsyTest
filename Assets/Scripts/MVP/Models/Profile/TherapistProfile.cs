using System.Collections.Generic;

namespace PsyTest.Profile
{
	public class TherapistProfile : UserProfileInfo
	{
		public string LastName { get; set; }
		public string ContactInfo { get; set; }
		public int ExperienceYears { get; set; }
		public List<string> Certificates { get; set; } = new();
	}
}