using Models.Profile;
using UnityEngine;
using View.MainMenu;
using View.Profile;
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

		public override void InstallBindings()
		{
			Container.Bind<IAuthView>().To<AuthView>().FromInstance(_authView).AsSingle();
			Container.Bind<IMainMenuView>().To<MainMenuView>().FromInstance(_mainMenuView).AsSingle();
			Container.Bind<ITestListView>().To<TestListView>().FromInstance(_testListView).AsSingle();

			Container.Bind<IProfileView<ClientProfile>>().WithId("Client").To<ClientProfileView>().FromInstance(_clientProfileView).AsTransient();
			Container.Bind<IProfileView<TherapistProfile>>().WithId("Therapist").To<TherapistProfileView>().FromInstance(_therapistProfileView).AsTransient();

			Container.Bind<OptionsManager>()
				.FromInstance(new OptionsManager(_testListView.OptionsContainer.transform, _testListView.OptionPrefab))
				.AsSingle();

			Container.Bind<ProfileViewFactory>().FromInstance(new ProfileViewFactory(Container)).AsSingle();
		}
	}
}