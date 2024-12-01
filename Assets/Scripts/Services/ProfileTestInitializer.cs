using Models.Profile;
using Presenter.Profile;
using System.Collections.Generic;
using Zenject;

public class ProfileTestInitializer : IInitializable
{
	private readonly ProfilePresenter _profilePresenter;

	public ProfileTestInitializer(ProfilePresenter profilePresenter)
	{
		_profilePresenter = profilePresenter;
	}

	public void Initialize()
	{
		ClientProfile client = new()
		{
			Name = "Olga",
			Login = "olga123",
			TestResults = new List<TestResult>
			{
				new TestResult { TestId = "T001", ResultLabel = "Mild Depression", TotalScore = 15 },
				new TestResult { TestId = "T002", ResultLabel = "Moderate Anxiety", TotalScore = 8 }
			}
		};
	}
}