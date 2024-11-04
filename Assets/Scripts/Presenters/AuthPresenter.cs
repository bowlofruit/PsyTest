using System;

public class AuthPresenter
{
	private readonly IAuthView _authView;
	private readonly FirebaseAuthService _authService;

	public AuthPresenter(IAuthView authView, FirebaseAuthService authService)
	{
		_authView = authView;
		_authService = authService;
	}

	public async void OnRegister(string email, string password, string username)
	{
		try
		{
			var user = await _authService.RegisterUser(email, password, username);
			_authView.ShowSuccess("Registration successful. Please verify your email.");
		}
		catch (Exception ex)
		{
			_authView.ShowError("Registration failed: " + ex.Message);
		}
	}

	public async void OnLogin(string email, string password)
	{
		try
		{
			await _authService.LoginUser(email, password);
			_authView.ShowSuccess("Login successful.");
		}
		catch (Exception ex)
		{
			_authView.ShowError("Login failed: " + ex.Message);
		}
	}
}