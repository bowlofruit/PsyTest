using Models.Profile;
using TMPro;
using View.Profile;

namespace Presenter.Profile
{
	public class ProfilePresenter
	{
		private readonly IProfileView<IUserProfile> _view;
		private readonly IUserProfile _currentUser;

		public ProfilePresenter(IProfileView<IUserProfile> view, IUserProfile currentUser)
		{
			_view = view;
			_currentUser = currentUser;
		}

		public void ShowClientProfile(ClientProfile client)
		{
			string testResults = string.Join("\n", client.TestResults.ConvertAll(
				result => $"{result.TestId}: {result.ResultLabel} (Score: {result.TotalScore})"));

			_view.DisplayProfile(_currentUser);
		}
	}
}