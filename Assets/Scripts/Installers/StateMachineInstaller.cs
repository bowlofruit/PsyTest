using Models.Profile;
using System.Collections.Generic;
using View.Profile;
using View.MainMenu;
using Zenject;

namespace Installers
{
	public class StateMachineInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<EventStateManager>().FromInstance(new EventStateManager()).AsSingle();
			Container.Bind<ProfileViewFactory>().FromInstance(new ProfileViewFactory(Container)).AsSingle();

			Container.Bind<StateMachine>().AsSingle().WithArguments(
				new Dictionary<AppStateEnum, IStateHandler>
				{
					{ AppStateEnum.AuthScreen, Container.Resolve<IAuthView>() },
					{ AppStateEnum.MainMenu, Container.Resolve<IMainMenuView>() },
					{ AppStateEnum.TestList, Container.Resolve<ITestListView>() },
					{ AppStateEnum.Profile, ResolveProfileView() }
                }
			).OnInstantiated<StateMachine>((ctx, stateMachine) =>
			{
				var mainMenuView = ctx.Container.Resolve<MainMenuView>();
				mainMenuView.Init(stateMachine);
			});
		}

		private IProfileView<IUserProfile> ResolveProfileView()
		{
			string role = "Client";
			var factory = Container.Resolve<ProfileViewFactory>();
			return factory.ResolveView(role);
		}
	}
}
