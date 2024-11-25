using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuView : MonoBehaviour
{
	[SerializeField] private Button _testListButton;
	[SerializeField] private Button _profileButton;

	private AppStateMachine _stateMachine;

	[Inject]
	public void Construct(AppStateMachine stateMachine)
	{
		_stateMachine = stateMachine;

		_testListButton.onClick.AddListener(() => _stateMachine.SetState(AppState.TestList));
		_profileButton.onClick.AddListener(() => _stateMachine.SetState(AppState.Profile));
	}
}
