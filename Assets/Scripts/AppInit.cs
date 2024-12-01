using UnityEngine;
using Zenject;

public class AppInit : MonoBehaviour
{
	private EventStateManager _eventManager;

	[Inject]
	private void Construct(EventStateManager eventStateManager)
	{
		_eventManager = eventStateManager;
	}

	private void Start()
	{
		_eventManager.Notify(AppStateEnum.AuthScreen);
	}
}