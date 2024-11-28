using DefaultEcs;
using DefaultEcs.System;
using ECS.Components.Auth;

namespace ECS.System.Auth
{
	public class AuthenticationSystem : AEntitySetSystem<float>
	{
		public AuthenticationSystem(World world) : base(world.GetEntities()
			.With<AuthComponent>()
			.AsSet())
		{

		}
	}
}