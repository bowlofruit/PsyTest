using Models.Profile;
using View.Profile;

namespace Presenter.Profile
{
	public class ProfilePresenter
	{
		private readonly IProfileView<IUserProfile> _view;
		private readonly IUserProfile _currentUser;

		public ProfilePresenter(ProfileViewFactory viewFactory, IUserProfile currentUser, string role)
		{
			_view = viewFactory.ResolveView(role);
			_currentUser = currentUser;
		}

		public void ShowProfile()
		{
			_view.DisplayProfile(_currentUser);
		}
	}
}