namespace View.Profile
{
	public interface IProfileView<T> : IStateHandler
	{
		public void DisplayProfile(T profile);
	}
}