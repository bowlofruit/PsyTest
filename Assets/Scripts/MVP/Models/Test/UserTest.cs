using System;

[Serializable]
public class UserTest
{
	public string UserId;
	public string TestId;
	public TestStatus Status;
	public int CurrentScore;
	public DateTime StartTime;
	public DateTime? EndTime;
}