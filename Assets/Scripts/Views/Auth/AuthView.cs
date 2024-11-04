using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AuthView : MonoBehaviour, IAuthView
{
	[SerializeField] private TMP_InputField _emailField;
	[SerializeField] private TMP_InputField _passwordField;
	[SerializeField] private TMP_InputField _usernameField;
	[SerializeField] private Button _registerButton;
	[SerializeField] private Button _loginButton;

	private AuthPresenter _presenter;

	[Inject]
	public void Construct(AuthPresenter presenter)
	{
		_presenter = presenter;
	}

	private void Start()
	{
		_registerButton.onClick.AddListener(() =>
			_presenter.OnRegister(_emailField.text, _passwordField.text, _usernameField.text));
		_loginButton.onClick.AddListener(() =>
			_presenter.OnLogin(_emailField.text, _passwordField.text));
	}

	public void ShowSuccess(string message)
	{
		Debug.Log("Success: " + message);
	}

	public void ShowError(string message)
	{
		Debug.LogError("Error: " + message);
	}
}
