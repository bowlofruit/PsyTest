using System.Collections.Generic;

namespace PsyTest.Profile
{
	public class ClientProfile : UserProfileInfo
	{
		public List<TestResult> TestResults { get; set; } = new();
		public string Notes { get; set; }
		public string ProfileLink { get; set; }
	}
}