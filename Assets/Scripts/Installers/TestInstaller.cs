using System;
using System.Text;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
	[SerializeField] private TestView _testView;

	public override void InstallBindings()
	{
		Container.Bind<OptionsManager>()
		.FromInstance(new OptionsManager(_testView.OptionsContainer.transform, _testView.OptionPrefab))
		.AsSingle();

		Container.Bind<TestView>().FromInstance(_testView).AsSingle();

		var userTest = new UserTest
		{
			UserId = "user123",
			TestId = "test123",
			Status = TestStatus.NotStarted,
			CurrentScore = 0,
			StartTime = DateTime.Now
		};

		var testContainer = TestData.GetSampleTest();

		Container.Bind<TestPresenter>().AsTransient()
			.WithArguments(_testView, testContainer, userTest);

		var presenter = Container.Resolve<TestPresenter>();
		presenter.StartTest();
	}
}