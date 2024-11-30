namespace Installers
{
	using Zenject;

	public class FirebaseBindingInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<FirebaseDatabaseService>().FromInstance(new FirebaseDatabaseService()).AsSingle().NonLazy();
			Container.Bind<FirebaseAuthService>().FromInstance(new FirebaseAuthService()).AsSingle().NonLazy();
		}
	}
}