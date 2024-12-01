using Models.Profile;
using Presenter.Profile;
using Presenter.PsyTest;
using Zenject;

namespace Installers
{
	public class PresenterInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindAuthPresenter();
			BindProfilePresenter();
			BindTestPresenter();
		}

		private void BindAuthPresenter()
		{
			Container.Bind<AuthPresenter>().AsTransient()
				.OnInstantiated<AuthPresenter>((ctx, presenter) =>
					Container.Resolve<IAuthView>().InitPresenter(presenter))
				.NonLazy();
		}

		private void BindProfilePresenter()
		{
			bool isClient = true;

			var profileViewFactory = Container.Resolve<ProfileViewFactory>();
			var userProfile = Container.Resolve<IUserProfile>();

			Container.Bind<ProfilePresenter>()
				.AsTransient()
				.WithArguments(profileViewFactory, userProfile, isClient ? "Client" : "Therapist");
		}

		private void BindTestPresenter()
		{
			var testListView = Container.Resolve<ITestListView>();
			var testList = TestDataFactory.CreateTestList();
			var userTest = TestDataFactory.CreateUserTest();

			Container.Bind<TestPresenter>().AsTransient()
				.WithArguments(testListView, testList, userTest)
				.NonLazy();
		}
	}
}