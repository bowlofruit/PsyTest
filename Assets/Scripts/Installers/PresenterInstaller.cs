using Models.Profile;
using Presenter.Profile;
using System;
using System.Collections.Generic;
using View.Profile;
using Zenject;

namespace Installers
{
	public class PresenterInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindAuthPresenter();
			BindProfilePresenter();
			BindTestPresenter();
		}

		private void BindAuthPresenter()
		{
			Container.Bind<AuthPresenter>().AsTransient()
				.OnInstantiated<AuthPresenter>((context, presenter) =>
					Container.Resolve<IAuthView>().InitPresenter(presenter));
		}

		private void BindProfilePresenter()
		{
			bool isClient = true;

			if (isClient)
			{
				Container.Bind<IUserProfile>().To<ClientProfile>().AsTransient();
			}
			else
			{
				Container.Bind<IUserProfile>().To<TherapistProfile>().AsTransient();
			}

			Container.Bind<ProfilePresenter>().AsTransient()
			.WithArguments(
				Container.Resolve<IProfileView<IUserProfile>>(),
				Container.Resolve<IUserProfile>()
			);

		}

		private void BindTestPresenter()
		{
			UserTest userTest = CreateUserTest();
			List<Test> testList = CreateTestList();

			Container.Bind<TestPresenter>().AsTransient()
				.WithArguments(testList, userTest)
				.OnInstantiated<TestPresenter>((context, presenter) =>
					Container.Resolve<ITestListView>().InitPresenter(presenter));
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
				CreateTest("1", "Beck Depression Test", "Psychological", "https://example.com/test_depression.json", "Test for assessing depression."),
				CreateTest("2", "Anxiety Test", "Psychological", "https://example.com/test_anxiety.json", "Test for assessing anxiety levels."),
				CreateTest("3", "Self-Esteem Test", "Psychological", "https://example.com/test_selfesteem.json", "Test for determining self-esteem.")
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
				Description = description
			};
		}
	}
}
