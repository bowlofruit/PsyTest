using UnityEngine;

public interface IAuthView
{
	GameObject GetGameObject();
	void ShowSuccess(string message);

	void ShowError(string message);
}