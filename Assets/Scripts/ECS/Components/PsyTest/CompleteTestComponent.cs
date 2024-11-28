using System;

namespace ECS.Components.PsyTest
{
	public struct CompleteTestComponent
	{
		public string UserId;
		public string TestId;
		public TestStatus Status;
		public int CurrentScore;
		public DateTime StartTime;
	}
}