using System;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		// Реєстрація View
		Container.Bind<TestView>().FromComponentInHierarchy().AsSingle();

		// Реєстрація TestContainer
		Container.Bind<TestContainer>().FromNew().AsSingle();

		// Реєстрація Factory
		Container.Bind<ITestPresenterFactory>().To<TestPresenterFactory>().AsSingle();
	}

	public override void Start()
	{
		// Створення UserTest
		var userTest = new UserTest
		{
			UserId = "user123", // або отримати з реального користувача
			TestId = "test123", // замінити на фактичний ідентифікатор тесту
			Status = TestStatus.NotStarted,
			CurrentScore = 0,
			StartTime = DateTime.Now
		};

		// Отримати Factory з контейнера
		var presenterFactory = Container.Resolve<ITestPresenterFactory>();

		// Створити Presenter через Factory
		presenterFactory.Create(userTest);
	}
}
