using Models.Profile;
using TMPro;
using UnityEngine;

namespace View.Profile
{
	public class TherapistProfileView : MonoBehaviour, IProfileView<TherapistProfile>
	{
		[SerializeField] private TMP_Text _nameText;
		[SerializeField] private TMP_Text _contactInfoText;
		[SerializeField] private TMP_Text _experienceText;
		[SerializeField] private TMP_Text _certificatesText;

		public void DisplayProfile(TherapistProfile profile)
		{
			_nameText.text = $"Name: {profile.Name}";
			_contactInfoText.text = $"Contact: {profile.ContactInfo}";
			_experienceText.text = $"Experience: {profile.ExperienceYears} years";
			_certificatesText.text = $"Certificates: {string.Join(", ", profile.Certificates)}";
		}

		public void Activate() => gameObject.SetActive(true);

		public void Deactivate() => gameObject.SetActive(false);
	}
}
