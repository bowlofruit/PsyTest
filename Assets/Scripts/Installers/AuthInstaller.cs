using UnityEngine;
using Zenject;

public class AuthInstaller : MonoInstaller
{
	[SerializeField] private AuthView _authView;

	public override void InstallBindings()
	{
		Container.Bind<IAuthView>().FromInstance(_authView).AsSingle();

		Container.Bind<FirebaseAuthService>().AsSingle().NonLazy();
		Container.Bind<FirebaseDatabaseService>().AsSingle().NonLazy();

		Container.Bind<AuthPresenter>().AsTransient(); 
		Container.Bind<AdminPresenter>().AsTransient();
	}
}