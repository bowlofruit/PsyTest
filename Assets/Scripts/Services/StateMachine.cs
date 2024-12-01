using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	private AppStateEnum _currentState;
	private readonly Dictionary<AppStateEnum, IStateHandler> _stateHandlers;
	private readonly EventStateManager _eventStateManager;

	public StateMachine(Dictionary<AppStateEnum, IStateHandler> stateHandlers, EventStateManager eventStateManager)
	{
		_stateHandlers = stateHandlers;
		_eventStateManager = eventStateManager;

		_eventStateManager.Subscribe(SetState);
		_eventStateManager.Notify(AppStateEnum.AuthScreen);
	}

	public void SetState(AppStateEnum newState)
	{
		if (_currentState != newState)
		{
			foreach (var stateHandler in _stateHandlers.Values)
			{
				stateHandler?.Deactivate();
			}

			_currentState = newState;

			if (_stateHandlers.TryGetValue(_currentState, out var newStateHandler))
			{
				newStateHandler?.Activate();
			}
			else
			{
				Debug.LogError($"State {_currentState} not found in handlers.");
			}
		}
	}
}