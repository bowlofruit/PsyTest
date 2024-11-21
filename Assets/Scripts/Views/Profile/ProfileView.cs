using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PsyTest.Profile
{
	public class ProfileView : MonoBehaviour
	{
		[Header("Client UI Elements")]
		[SerializeField] private GameObject _clientPanel;
		[SerializeField] private TMP_Text _clientNameText;
		[SerializeField] private TMP_Text _clientLoginText;
		[SerializeField] private TMP_Text _clientTestResultsText;

		[Header("Therapist UI Elements")]
		[SerializeField] private GameObject _therapistPanel;
		[SerializeField] private TMP_Text _therapistNameText;
		[SerializeField] private TMP_Text _therapistContactText;
		[SerializeField] private TMP_Text _therapistExperienceText;
		[SerializeField] private TMP_Text _therapistCertificatesText;

		public void DisplayClientProfile(string name, string login, string testResults)
		{
			_clientPanel.SetActive(true);
			_therapistPanel.SetActive(false);

			_clientNameText.text = $"Name: {name}";
			_clientLoginText.text = $"Login: {login}";
			_clientTestResultsText.text = $"Test Results:\n{testResults}";
		}

		public void DisplayTherapistProfile(string name, string contactInfo, string certificates, int experience)
		{
			_clientPanel.SetActive(false);
			_therapistPanel.SetActive(true);

			_therapistNameText.text = $"Name: {name}";
			_therapistContactText.text = $"Contact: {contactInfo}";
			_therapistCertificatesText.text = $"Certificates:\n{certificates}";
			_therapistExperienceText.text = $"Experience: {experience} years";
		}
	}
}