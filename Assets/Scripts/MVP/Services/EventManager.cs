using System;

public class EventManager
{
	public Action<AppState> OnStateChangeRequested;

	public void Subscribe(Action<AppState> callback)
	{
		OnStateChangeRequested += callback;
	}

	public void Unsubscribe(Action<AppState> callback)
	{
		OnStateChangeRequested -= callback;
	}

	public void Notify(AppState state)
	{
		OnStateChangeRequested?.Invoke(state);
	}
}
