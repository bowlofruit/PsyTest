using System.Collections.Generic;
using View.Profile;
using Models.Profile;

public class AppStateMachine
{
	private AppState _currentState;

	private readonly AuthView _authView;
	private readonly MainMenuView _mainMenuView;
	private readonly TestListView _testListView;
	private readonly IProfileView<IUserProfile> _profileView;
	private readonly FirebaseAuthService _authService;
	private readonly IUserProfile _currentUserProfile;

	public AppState CurrentState => _currentState;
	
	public AppStateMachine(AuthView authView, MainMenuView mainMenuView, TestListView testListView, IProfileView<IUserProfile> profileView, FirebaseAuthService authService, EventManager eventManager)
	{
		_authView = authView;
		_mainMenuView = mainMenuView;
		_testListView = testListView;
		_profileView = profileView;
		_authService = authService;

		eventManager.Subscribe(SetState);

		SetState(AppState.AuthScreen);
	}

	public void SetState(AppState state)
	{
		_currentState = state;

		switch (state)
		{
			case AppState.AuthScreen:
				ShowAuthScreen();
				break;

			case AppState.MainMenu:
				ShowMainMenu();
				break;

			case AppState.TestList:
				ShowTestList();
				break;

			case AppState.Profile:
				ShowProfile();
				break;
		}
	}

	private void ShowAuthScreen()
	{
		_authView.gameObject.SetActive(true);
		_mainMenuView.gameObject.SetActive(false);
		_testListView.gameObject.SetActive(false);
		_profileView.GetGameObject().SetActive(false);
	}

	private void ShowMainMenu()
	{
		_authView.gameObject.SetActive(false);
		_mainMenuView.gameObject.SetActive(true);
		_testListView.gameObject.SetActive(false);
		_profileView.GetGameObject().SetActive(false);
	}

	private void ShowTestList()
	{
		_authView.gameObject.SetActive(false);
		_mainMenuView.gameObject.SetActive(false);
		_testListView.gameObject.SetActive(true);
		_profileView.GetGameObject().SetActive(false);
	}

	private async void ShowProfile()
	{
		var userProfile = await _authService.GetUserProfile();

		_profileView.DisplayProfile(_currentUserProfile);

		_profileView.GetGameObject().SetActive(true);
		_authView.gameObject.SetActive(false);
		_mainMenuView.gameObject.SetActive(false);
		_testListView.gameObject.SetActive(false);
	}

	private string FormatTestResults(List<TestResult> testResults)
	{
		if (testResults == null || testResults.Count == 0)
			return "No test results available.";

		var formattedResults = string.Join("\n", testResults.ConvertAll(
			result => $"{result.TestId}: {result.ResultLabel} (Score: {result.TotalScore})"));
		return formattedResults;
	}
}
