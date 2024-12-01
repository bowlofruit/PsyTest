﻿using System.Collections.Generic;
using Zenject;
using View.Profile;
using Models.Profile;
using View.PsyTest;

public class StateMachineInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<EventStateManager>().AsSingle().NonLazy();

		EventStateManager eventStateManager = Container.Resolve<EventStateManager>();
		var stateMachine = new StateMachine(CreateStateHandlers(), eventStateManager);

		Container.Bind<StateMachine>()
			.FromInstance(stateMachine)
			.OnInstantiated<StateMachine>((ctx, stateMachine) =>
			{
				var mainMenuView = ctx.Container.Resolve<IMainMenuView>();
				mainMenuView.Init(stateMachine);
			})
			.NonLazy();
	}

	private Dictionary<AppStateEnum, IStateHandler> CreateStateHandlers()
	{
		var authView = Container.Resolve<IAuthView>();
		var mainMenuView = Container.Resolve<IMainMenuView>();
		var testListView = Container.Resolve<ITestListView>();
		var testQuestionView = Container.Resolve<TestQuestionsView>();
		var testResultView = Container.Resolve<TestResultView>();
		var profileView = Container.Resolve<IProfileView<ClientProfile>>();

		var stateHandlers = new Dictionary<AppStateEnum, IStateHandler>
		{
			{ AppStateEnum.AuthScreen, authView },
			{ AppStateEnum.MainMenu, mainMenuView },
			{ AppStateEnum.TestList, testListView },
			{ AppStateEnum.TestQuestion, testQuestionView },
			{ AppStateEnum.TestResult, testResultView },
			{ AppStateEnum.Profile, profileView }
		};

		return stateHandlers;
	}
}