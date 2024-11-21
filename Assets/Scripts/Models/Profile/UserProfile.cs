using System;
using System.Collections.Generic;

namespace PsyTest.Profile
{
	[Serializable]
	public abstract class UserProfile
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Login { get; set; }
	}
}