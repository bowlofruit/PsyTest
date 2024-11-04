using System;
using System.Collections.Generic;
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

		UserTest userTest = new()
		{
			UserId = "user123",
			TestId = "test123",
			Status = TestStatus.NotStarted,
			CurrentScore = 0,
			StartTime = DateTime.Now
		};

		List<Test> testList = new()
		{
			new() {
				Id = "1",
				Name = "Тест на депресію Бека",
				Category = "Психологічні",
				JsonUrl = "https://example.com/test_depression.json",
				Status = "Active",
				CreatedAt = DateTime.Now.AddDays(-10),
				UpdatedAt = DateTime.Now,
				Logo = _testView.TestSprite,
				Description = "Тест для оцінки рівня депресії за методикою Бека.",
				Container = TestData.GetSampleTest()
			},
			new() {
				Id = "2",
				Name = "Тест на тривожність",
				Category = "Психологічні",
				JsonUrl = "https://example.com/test_anxiety.json",
				Status = "Active",
				CreatedAt = DateTime.Now.AddDays(-20),
				UpdatedAt = DateTime.Now,
				Logo = _testView.TestSprite,
				Description = "Тест для оцінки рівня тривожності.",
				Container = TestData.GetAnxietyTest()
			},
			new() {
				Id = "3",
				Name = "Тест на самооцінку",
				Category = "Психологічні",
				JsonUrl = "https://example.com/test_selfesteem.json",
				Status = "Active",
				CreatedAt = DateTime.Now.AddDays(-15),
				UpdatedAt = DateTime.Now,
				Logo = _testView.TestSprite,
				Description = "Тест для визначення рівня самооцінки.",
				Container = TestData.GetSelfEsteemTest()
			}
		};

		Container.Bind<TestPresenter>().AsTransient().WithArguments(_testView, testList, userTest);
	}
}