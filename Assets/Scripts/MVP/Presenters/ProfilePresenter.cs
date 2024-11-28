namespace PsyTest.Profile
{
	public class ProfilePresenter
	{
		private readonly ProfileView _view;

		public ProfilePresenter(ProfileView view)
		{
			_view = view;
		}

		public void ShowClientProfile(ClientProfile client)
		{
			string testResults = string.Join("\n", client.TestResults.ConvertAll(
				result => $"{result.TestId}: {result.ResultLabel} (Score: {result.TotalScore})"));

			_view.DisplayClientProfile(client.Name, client.Login, testResults);
		}

		public void ShowTherapistProfile(TherapistProfile therapist)
		{
			string certificates = string.Join("\n", therapist.Certificates);
			_view.DisplayTherapistProfile(therapist.Name, therapist.ContactInfo, certificates, therapist.ExperienceYears);
		}
	}
}