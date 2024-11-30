using System.Collections.Generic;
using System;
using View.Profile;
using Presenter.Profile;
using Models.Profile;
using Zenject;

namespace Installers
{
	public class PresenterInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			var authContainer = Container.Resolve<IAuthView>();

			Container.Bind<AuthPresenter>()
				.FromInstance(new AuthPresenter(
					Container.Resolve<FirebaseAuthService>(),
					authContainer,
					Container.Resolve<EventStateManager>()
				))
				.AsTransient()
				.OnInstantiated<AuthPresenter>((context, presenter) => authContainer.InitPresenter(presenter));

			BindProfilePresenter();
			BindTestPresenter();
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
		}

		private void BindTestPresenter()
		{
			UserTest userTest = CreateUserTest();
			List<Test> testList = CreateTestList();
			ITestListView testListView = Container.Resolve<ITestListView>();
			Container.Bind<TestPresenter>()
				.AsTransient()
				.WithArguments(Container.Resolve<ITestListView>(), testList, userTest)
				.OnInstantiated<TestPresenter>((context, presenter) => testListView.InitPresenter(presenter)); ;
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
				Description = description
			};
		}
	}
}