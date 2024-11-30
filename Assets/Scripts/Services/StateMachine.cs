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
	}

	public void SetState(AppStateEnum newState)
	{
		if (_currentState != newState)
		{
			if (_stateHandlers.TryGetValue(_currentState, out var currentStateHandler))
			{
				currentStateHandler?.Deactivate();
			}
			else
			{
				Debug.LogWarning($"State {_currentState} not found in handlers.");
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