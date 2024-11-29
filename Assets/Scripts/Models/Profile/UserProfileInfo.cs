namespace Models.Profile
{
	public interface IUserProfile
	{
		string UserId { get; set; }
		string Name { get; set; }
		string Login { get; set; }
		string Email { get; set; }
		string Role { get; set; }
	}
}