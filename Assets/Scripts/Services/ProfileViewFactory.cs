using Models.Profile;
using System;
using View.Profile;
using Zenject;

public class ProfileViewFactory
{
	private readonly DiContainer _container;

	public ProfileViewFactory(DiContainer container)
	{
		_container = container;
	}

	public IProfileView<IUserProfile> ResolveView(string role)
	{
		return role switch
		{
			"Therapist" => _container.Resolve<IProfileView<TherapistProfile>>() as IProfileView<IUserProfile>,
			"Client" => _container.Resolve<IProfileView<ClientProfile>>() as IProfileView<IUserProfile>,
			_ => throw new ArgumentException($"No view found for role: {role}"),
		};
	}
}
