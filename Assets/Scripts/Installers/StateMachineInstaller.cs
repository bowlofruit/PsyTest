using System;
using System.Collections.Generic;
using View.MainMenu;
using Zenject;

namespace Installers
{
	public class StateMachineInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<EventStateManager>().FromInstance(new EventStateManager()).AsSingle();

			Container.Bind<StateMachine>().AsSingle().WithArguments(
				new Dictionary<AppStateEnum, Func<IStateHandler>>
				{
					{ AppStateEnum.AuthScreen, () => Container.Resolve<IAuthView>() },
					{ AppStateEnum.MainMenu, () => Container.Resolve<IMainMenuView>() },
					{ AppStateEnum.TestList, () => Container.Resolve<ITestListView>() },
					{ AppStateEnum.Profile, () => ResolveProfileView() }
				}
			).OnInstantiated<StateMachine>((ctx, stateMachine) =>
			{
				var mainMenuView = ctx.Container.Resolve<MainMenuView>();
				mainMenuView.Init(stateMachine);

				stateMachine.SetState(AppStateEnum.AuthScreen);
			});
		}

		private IStateHandler ResolveProfileView()
		{
			string role = "Client";
			var factory = Container.Resolve<ProfileViewFactory>();
			return factory.ResolveView(role);
		}
	}
}