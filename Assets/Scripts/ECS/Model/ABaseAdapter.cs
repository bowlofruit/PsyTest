using DefaultEcs;
using UnityEngine;

namespace ECS.Model
{
	public abstract class ABaseAdapter : MonoBehaviour
	{
		public virtual void Install(World world, Entity entity) 
		{

		}
	}
}