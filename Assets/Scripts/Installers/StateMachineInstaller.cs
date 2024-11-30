using System.Collections.Generic;
using View.Profile;
using Models.Profile;
using View.MainMenu;

namespace Installers
{
	using Zenject;

	public class StateMachineInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<StateMachine>().AsSingle().WithArguments(
				new Dictionary<AppStateEnum, IStateHandler>
				{
					{ AppStateEnum.AuthScreen, Container.Resolve<IAuthView>() },
					{ AppStateEnum.MainMenu, Container.Resolve<IMainMenuView>() },
					{ AppStateEnum.TestList, Container.Resolve<ITestListView>() },
					{ AppStateEnum.Profile, Container.Resolve<IProfileView<IUserProfile>>() }
				}
			).OnInstantiated<StateMachine>((ctx, stateMachine) =>
			{
				var mainMenuView = ctx.Container.Resolve<MainMenuView>();
				mainMenuView.Init(stateMachine);
			});
		}
	}
}
