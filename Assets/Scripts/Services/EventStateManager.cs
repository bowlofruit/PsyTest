using System;

public class EventStateManager
{
	public Action<AppStateEnum> OnStateChangeRequested;

	public void Subscribe(Action<AppStateEnum> callback)
	{
		OnStateChangeRequested += callback;
	}

	public void Unsubscribe(Action<AppStateEnum> callback)
	{
		OnStateChangeRequested -= callback;
	}

	public void Notify(AppStateEnum state)
	{
		OnStateChangeRequested?.Invoke(state);
	}
}
