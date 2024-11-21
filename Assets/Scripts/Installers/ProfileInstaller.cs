using PsyTest.Profile;
using UnityEngine;
using Zenject;

public class ProfileInstaller : MonoInstaller
{
	[Header("Dependencies")]
	[SerializeField] private ProfileView _profileView;

	public override void InstallBindings()
	{
		Container.Bind<ProfileView>().FromInstance(_profileView).AsSingle();
		Container.Bind<ProfilePresenter>().AsSingle();

		Container.BindInterfacesAndSelfTo<ProfileTestInitializer>().AsSingle();
	}
}
