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
	[SerializeField] private TMP_Text _messageLabel;

	private AuthPresenter _presenter;

	[Inject]
	public void Construct(AuthPresenter presenter)
	{
		_presenter = presenter;

		_registerButton.onClick.AddListener(() =>
			_presenter.OnRegister(_emailField.text, _passwordField.text, _usernameField.text));
		_loginButton.onClick.AddListener(() =>
			_presenter.OnLogin(_emailField.text, _passwordField.text));
	}

	public void ShowSuccess(string message)
	{
		_messageLabel.text = "Success: " + message;
		_messageLabel.color = Color.green;
	}

	public void ShowError(string message)
	{
		_messageLabel.text = "Error: " + message;
		_messageLabel.color = Color.red;
	}

	public GameObject GetGameObject()
	{
		return gameObject;
	}
}
