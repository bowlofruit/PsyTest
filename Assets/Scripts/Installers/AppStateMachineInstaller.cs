using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using View.Profile;
using Presenter.Profile;
using Models.Profile;

public class AppStateMachineInstaller : MonoInstaller
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
		BindEventManager();
		BindOptionsManager();
		BindUserTestData();
		BindPresenters();
		BindStateMachine();
	}

	private void BindFirebaseServices()
	{
		Container.Bind<FirebaseAuthService>().AsSingle().NonLazy();
		Container.Bind<FirebaseDatabaseService>().AsSingle().NonLazy();
	}

	private void BindViews()
	{
		Container.Bind<IAuthView>().To<AuthView>().FromInstance(_authView).AsSingle();
		Container.Bind<MainMenuView>().FromInstance(_mainMenuView).AsSingle();
		Container.Bind<ITestListView>().To<TestListView>().FromInstance(_testListView).AsSingle();

		Container.Bind<IProfileView<ClientProfile>>().WithId("Client").To<ClientProfileView>().AsTransient();
		Container.Bind<IProfileView<TherapistProfile>>().WithId("Therapist").To<TherapistProfileView>().AsTransient();

		Container.Bind<ProfileViewFactory>().AsSingle();
	}

	private void BindStateMachine()
	{
		Container.Bind<AppStateMachine>().AsSingle();
	}

	private void BindEventManager()
	{
		Container.Bind<EventManager>().AsSingle();
	}

	private void BindOptionsManager()
	{
		Container.Bind<OptionsManager>()
			.FromInstance(new OptionsManager(_testListView.OptionsContainer.transform, _testListView.OptionPrefab))
			.AsSingle();
	}

	private void BindUserTestData()
	{
		UserTest userTest = CreateUserTest();
		List<Test> testList = CreateTestList();
		Container.Bind<TestPresenter>().AsTransient().WithArguments(_testListView, testList, userTest);
	}

	private void BindPresenters()
	{
		Container.Bind<AuthPresenter>().AsTransient();
		Container.Bind<ProfilePresenter>().AsTransient();
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