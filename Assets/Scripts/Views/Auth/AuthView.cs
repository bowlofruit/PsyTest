using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AuthView : MonoBehaviour, IAuthView
{
	[SerializeField] private TMP_InputField _emailField;
	[SerializeField] private TMP_InputField _passwordField;
	[SerializeField] private TMP_InputField _usernameField;
	[SerializeField] private Button _registerButton;
	[SerializeField] private Button _loginButton;
	[SerializeField] private TMP_Text _messageLabel;

	private AuthPresenter _presenter;

	public void InitPresenter(AuthPresenter authPresenter)
	{
		_presenter = authPresenter;

		SubscribesButton(authPresenter);
	}

	private void SubscribesButton(AuthPresenter authPresenter)
	{
		_registerButton.onClick.AddListener(() =>
			authPresenter.OnRegister(_emailField.text, _passwordField.text, _usernameField.text));
		_loginButton.onClick.AddListener(() =>
			authPresenter.OnLogin(_emailField.text, _passwordField.text));
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

	public void Activate() => gameObject.SetActive(true);

	public void Deactivate() => gameObject.SetActive(false);
}