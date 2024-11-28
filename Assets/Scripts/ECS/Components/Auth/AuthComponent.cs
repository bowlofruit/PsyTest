using System;

namespace ECS.Components.Auth
{
	[Serializable]
	public struct AuthComponent
	{
		public string Email;
		public string Username;
		public string Password;
		public string Role;
	}
}
