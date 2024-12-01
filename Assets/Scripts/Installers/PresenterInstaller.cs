using Models.Profile;
using Presenter.Profile;
using Presenter.PsyTest;
using UnityEngine;
using Zenject;

namespace Installers
{
	public class PresenterInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindTestSelectionMediator();
			BindAuthPresenter();
			BindProfilePresenter();
			BindTestPresenter();
		}

		private void BindTestSelectionMediator()
		{
			Container.Bind<TestSelectionMediator>()
				.AsSingle();
		}

		private void BindAuthPresenter()
		{
			Container.Bind<AuthPresenter>()
				.AsTransient()
				.OnInstantiated<AuthPresenter>((ctx, presenter) =>
					Container.Resolve<IAuthView>().InitPresenter(presenter));
		}

		private void BindProfilePresenter()
		{
			Container.Bind<ProfilePresenter>()
				.AsTransient()
				.WithArguments("Client");
		}


		private void BindTestPresenter()
		{
			var testList = TestDataFactory.CreateTestList();

			Container.Bind<TestPresenter>()
				.AsTransient()
				.WithArguments(testList);
			Debug.Log("TestPresenter bound successfully");
		}
	}
}