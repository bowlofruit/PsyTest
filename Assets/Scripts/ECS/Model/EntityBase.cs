using DefaultEcs;
using UnityEngine;

namespace ECS.Model
{
	public class EntityBase : MonoBehaviour
	{
		public ABaseAdapter[] Adapters;

		public Entity Entity { get; private set; }

		public Entity CreateEntity(World world)
		{
			var entity = world.CreateEntity();
			Entity = entity;

			if (Adapters != null)
			{
				foreach (var adapter in Adapters)
				{
					adapter.Install(world, entity);
				}
			}

			return entity;
		}
	}

	public class EntityBaseComponent <T> : ABaseAdapter where T : struct
	{
		protected World World { get; private set; }
		protected Entity Entity { get; private set; }

		public override void Install(World world, Entity entity)
		{
			if (entity.Has<T>())
			{
				Debug.LogWarning($"Component already exists in entry {typeof(T)}", gameObject);
				return;
			}

			entity.Set<T>();

			World = world;
			Entity = entity;
		}
	}
}