using System;

namespace Models.Authentication
{
	[Serializable]
	public struct FirebaseUserData
	{
		public string Username;
		public string Email;
		public string Role;
	}
}