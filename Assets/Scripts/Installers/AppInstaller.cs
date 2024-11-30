using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using View.Profile;
using Presenter.Profile;
using Models.Profile;
using View.MainMenu;
public class AppInstaller : MonoInstaller
{
	[SerializeField] private AuthView _authView;
	[SerializeField] private MainMenuView _mainMenuView;
	[SerializeField] private TestListView _testListView;

	[SerializeField] private ClientProfileView _clientProfileView;
	[SerializeField] private TherapistProfileView _therapistProfileView;

	public override void InstallBindings()
	{
		BindFirebaseServices();
		BindViews();
		BindStateMachine();
		BindEventStateManager();
		BindTestOptionsManager();
		BindPresenters();
	}

	private void BindFirebaseServices()
	{
		Container.Bind<FirebaseDatabaseService>().FromInstance(new FirebaseDatabaseService()).AsSingle().NonLazy();
		Container.Bind<FirebaseAuthService>().FromInstance(new FirebaseAuthService()).AsSingle().NonLazy();
	}

	private void BindViews()
	{
		Container.Bind<IAuthView>().To<AuthView>().FromInstance(_authView).AsSingle();
		Container.Bind<IMainMenuView>().To<MainMenuView>().FromInstance(_mainMenuView).AsSingle();
		Container.Bind<ITestListView>().To<TestListView>().FromInstance(_testListView).AsSingle();

		Container.Bind<IProfileView<ClientProfile>>().WithId("Client").To<ClientProfileView>().FromInstance(_clientProfileView).AsTransient();
		Container.Bind<IProfileView<TherapistProfile>>().WithId("Therapist").To<TherapistProfileView>().FromInstance(_therapistProfileView).AsTransient();

		Container.Bind<ProfileViewFactory>().AsSingle();
	}

	private void BindStateMachine()
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

	private void BindEventStateManager()
	{
		Container.Bind<EventStateManager>().FromInstance(new EventStateManager()).AsSingle();
	}

	private void BindTestOptionsManager()
	{
		Container.Bind<OptionsManager>()
			.FromInstance(new OptionsManager(_testListView.OptionsContainer.transform, _testListView.OptionPrefab))
			.AsSingle();
	}

	private void BindTestPresenter()
	{
		UserTest userTest = CreateUserTest();
		List<Test> testList = CreateTestList();
		Container.Bind<TestPresenter>().AsTransient().WithArguments(_testListView, testList, userTest);
	}

	private void BindPresenters()
	{
		BindTestPresenter();
		BindProfilePresenter();
		Container.Bind<AuthPresenter>()
			.FromInstance(new AuthPresenter(
				Container.Resolve<FirebaseAuthService>(),
				Container.Resolve<IAuthView>(),
				Container.Resolve<EventStateManager>()))
			.AsTransient();
	}

	private void BindProfilePresenter()
	{
		bool isClient = true;

		if (isClient)
		{
			Container.Bind<IUserProfile>().To<ClientProfile>().AsTransient();
			Container.Bind<ProfilePresenter>()
				.AsTransient()
				.WithArguments(
					Container.ResolveId<IProfileView<ClientProfile>>("Client"),
					Container.Resolve<ClientProfile>()
				);
		}
		else
		{
			Container.Bind<IUserProfile>().To<TherapistProfile>().AsTransient();
			Container.Bind<ProfilePresenter>()
				.AsTransient()
				.WithArguments(
					Container.ResolveId<IProfileView<TherapistProfile>>("Therapist"),
					Container.Resolve<TherapistProfile>()
				);
		}

		Container.Bind<ProfilePresenter>()
			.ToSelf()
			.AsTransient()
			.WithArguments(
				Container.Resolve<IProfileView<IUserProfile>>(),
				Container.Resolve<IUserProfile>()
			);
	}


	private UserTest CreateUserTest()
	{
		return new UserTest
		{
			UserId = "user123",
			TestId = "test123",
			Status = TestStatus.NotStarted,
			CurrentScore = 0,
			StartTime = DateTime.Now
		};
	}

	private List<Test> CreateTestList()
	{
		return new List<Test>
		{
			CreateTest("1", "Тест на депресію Бека", "Психологічні", "https://example.com/test_depression.json", "Тест для оцінки рівня депресії за методикою Бека."),
			CreateTest("2", "Тест на тривожність", "Психологічні", "https://example.com/test_anxiety.json", "Тест для оцінки рівня тривожності."),
			CreateTest("3", "Тест на самооцінку", "Психологічні", "https://example.com/test_selfesteem.json", "Тест для визначення рівня самооцінки.")
		};
	}

	private Test CreateTest(string id, string name, string category, string jsonUrl, string description)
	{
		return new Test
		{
			Id = id,
			Name = name,
			Category = category,
			JsonUrl = jsonUrl,
			Status = "Active",
			CreatedAt = DateTime.Now.AddDays(-10),
			UpdatedAt = DateTime.Now,
			Logo = _testListView.TestSprite,
			Description = description,
			Container = GetTestContainer(id)
		};
	}

	private TestContainer GetTestContainer(string testId)
	{
		return testId switch
		{
			"1" => TestData.GetSampleTest(),
			"2" => TestData.GetAnxietyTest(),
			"3" => TestData.GetSelfEsteemTest(),
			_ => null,
		};
	}
}