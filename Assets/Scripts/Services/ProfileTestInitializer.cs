using PsyTest.Profile;
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
		// Створюємо тестові профілі
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

		TherapistProfile therapist = new()
		{
			Name = "Ivan",
			ContactInfo = "ivan.petrov@gmail.com",
			ExperienceYears = 10,
			Certificates = new List<string> { "CBT Certification", "EMDR Training" }
		};

		// Відображення профілю клієнта
		_profilePresenter.ShowClientProfile(client);

		// Альтернативно: Відображення профілю терапевта
		// _profilePresenter.ShowTherapistProfile(therapist);
	}
}