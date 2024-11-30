using UnityEngine;
using UnityEngine.UI;

namespace View.MainMenu
{
	public class MainMenuView : MonoBehaviour, IStateHandler, IMainMenuView
	{
		[SerializeField] private Button _testListButton;
		[SerializeField] private Button _profileButton;

		public void Init(StateMachine stateMachine)
		{
			_testListButton.onClick.AddListener(() => stateMachine.SetState(AppStateEnum.TestList));
			_profileButton.onClick.AddListener(() => stateMachine.SetState(AppStateEnum.Profile));
		}

		public void Activate() => gameObject.SetActive(true);

		public void Deactivate() => gameObject.SetActive(false);
	}
}