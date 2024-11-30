using Models.Profile;
using System.Collections.Generic;
using View.Profile;

public partial class StateMachine
{
	private AppStateEnum _currentState;
	private readonly Dictionary<AppStateEnum, IStateHandler> _stateHandlers;

	public StateMachine(Dictionary<AppStateEnum, IStateHandler> stateHandlers)
	{
		_stateHandlers = stateHandlers;
		SetState(AppStateEnum.AuthScreen);
	}

	public void SetState(AppStateEnum newState)
	{
		if (_currentState != newState)
		{
			_stateHandlers[_currentState]?.Deactivate();
			_currentState = newState;
			_stateHandlers[_currentState]?.Activate();
		}
	}
}