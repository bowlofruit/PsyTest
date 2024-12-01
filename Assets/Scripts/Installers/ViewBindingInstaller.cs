using Models.Profile;
using UnityEngine;
using View.MainMenu;
using View.Profile;
using View.PsyTest;
using Zenject;

namespace Installers
{
	public class ViewBindingInstaller : MonoInstaller
	{
		[SerializeField] private AuthView _authView;
		[SerializeField] private MainMenuView _mainMenuView;
		[SerializeField] private TestListView _testListView;
		[SerializeField] private ClientProfileView _clientProfileView;
		[SerializeField] private TherapistProfileView _therapistProfileView;
		[SerializeField] private TestQuestionsView _testQuestionsView;
		[SerializeField] private TestResultView _testResultView;

		public override void InstallBindings()
		{
			BindAuthView();
			BindMainView();
			BindTestListView();
			BindTestQuestionsView();
			BindTestResultView();
			BindProfilesView();
			BindProfilesFactory();
		}

		private void BindProfilesFactory()
		{
			Container.Bind<ProfileViewFactory>()
				.AsSingle()
				.NonLazy();
		}

		private void BindProfilesView()
		{
			Container.Bind<IProfileView<ClientProfile>>()
				.WithId("Client")
				.To<ClientProfileView>()
				.FromInstance(_clientProfileView)
				.AsTransient();

			Container.Bind<IProfileView<TherapistProfile>>()
				.WithId("Therapist")
				.To<TherapistProfileView>()
				.FromInstance(_therapistProfileView)
				.AsTransient();
		}

		private void BindAuthView()
		{
			Container.Bind<IAuthView>()
				.To<AuthView>()
				.FromInstance(_authView)
				.AsSingle();
		}

		private void BindMainView()
		{
			Container.Bind<IMainMenuView>()
				.To<MainMenuView>()
				.FromInstance(_mainMenuView)
				.AsSingle();
		}

		private void BindTestListView()
		{
			Container.Bind<OptionsManager>()
				.FromInstance(new OptionsManager(
					_testQuestionsView.OptionsContainer.transform,
					_testQuestionsView.OptionPrefab))
				.AsSingle();

			Container.Bind<ITestListView>()
				.To<TestListView>()
				.FromInstance(_testListView)
				.AsSingle();
		}

		private void BindTestQuestionsView()
		{
			Container.Bind<TestQuestionsView>()
				.FromInstance(_testQuestionsView)
				.AsSingle();
		}

		private void BindTestResultView()
		{
			Container.Bind<TestResultView>()
				.FromInstance(_testResultView)
				.AsSingle();
		}
	}
}
