using System;

public class AuthPresenter
{
	private readonly FirebaseAuthService _authService;
	private readonly AuthView _authView;
	private readonly EventManager _eventManager;

	public AuthPresenter(FirebaseAuthService authService, AuthView authView, EventManager eventManager)
	{
		_authService = authService;
		_authView = authView;
		_eventManager = eventManager;
	}

	public async void OnLogin(string email, string password)
	{
		try
		{
			await _authService.LoginUser(email, password);

			if (_authService.IsEmailVerified())
			{
				_authView.ShowSuccess("Login successful");
				_eventManager.Notify(AppState.MainMenu);
			}
			else
			{
				_authView.ShowError("Email not verified. Please check your inbox.");
			}
		}
		catch (Exception ex)
		{
			_authView.ShowError($"Login failed: {ex.Message}");
			UnityEngine.Debug.Log(ex);
		}
	}

	public async void OnRegister(string email, string password, string username)
	{
		try
		{
			await _authService.RegisterUser(email, password, username);
			_authView.ShowSuccess("Registration successful. Check your email for verification.");
		}
		catch (Exception ex)
		{
			_authView.ShowError($"Registration failed: {ex.Message}");
		}
	}
}
