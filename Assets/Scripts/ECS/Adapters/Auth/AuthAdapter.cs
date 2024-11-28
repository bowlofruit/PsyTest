using DefaultEcs;
using ECS.Components.Auth;
using ECS.Model;
using UnityEngine;

public class AuthAdapter : EntityBaseComponent<AuthComponent>
{
    [SerializeField] private AuthComponent _authData;

	public override void Install(World world, Entity entity)
	{
		base.Install(world, entity);

		ref var component = ref Entity.Get<AuthComponent>();

		component = _authData;
	}
}
