using Models.Profile;
using TMPro;
using UnityEngine;

namespace View.Profile
{
	public class ClientProfileView : MonoBehaviour, IProfileView<ClientProfile>
	{
		[SerializeField] private TMP_Text _nameText;
		[SerializeField] private TMP_Text _testResultsText;
		[SerializeField] private TMP_Text _notesText;

		public void DisplayProfile(ClientProfile profile)
		{
			_nameText.text = $"Name: {profile.Name}";
			_testResultsText.text = $"Test Results: {string.Join(", ", profile.TestResults)}";
			_notesText.text = $"Notes: {profile.Notes}";
		}

		public GameObject GetGameObject()
		{
			return gameObject;
		}
	}
}
