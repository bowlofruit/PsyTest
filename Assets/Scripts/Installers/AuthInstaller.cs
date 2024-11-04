using UnityEngine;
using Zenject;

public class AuthInstaller : MonoInstaller
{
	[SerializeField] private AuthView _authView;

	public override void InstallBindings()
	{
		// Binding the AuthView to the IAuthView interface
		Container.Bind<IAuthView>().FromInstance(_authView).AsSingle();

		// Binding services
		Container.Bind<FirebaseAuthService>().AsSingle().NonLazy();
		Container.Bind<FirebaseDatabaseService>().AsSingle().NonLazy();

		// Binding presenters as transient
		Container.Bind<AuthPresenter>().AsTransient(); 
		Container.Bind<AdminPresenter>().AsTransient();
	}
}