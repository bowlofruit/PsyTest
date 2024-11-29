using Models.Profile;
using Unity.VisualScripting;
using UnityEngine;

namespace View.Profile
{
	public interface IProfileView<T> where T : IUserProfile
	{
		public void DisplayProfile(T profile);

		public GameObject GetGameObject();
	}
}
