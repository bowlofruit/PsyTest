using System;
using System.Collections.Generic;

namespace Installers
{
	public class TestDataFactory
	{
		public static UserTest CreateUserTest(
			string userId = "user123",
			string testId = "test123",
			TestStatus status = TestStatus.NotStarted,
			int initialScore = 0)
		{
			return new UserTest
			{
				UserId = userId,
				TestId = testId,
				Status = status,
				CurrentScore = initialScore,
				StartTime = DateTime.Now
			};
		}

		public static List<Test> CreateTestList()
		{
			return new List<Test>
			{
				CreateTest("1", "Beck Depression Test", "Psychological", "https://example.com/test_depression.json", "Test for assessing depression."),
				CreateTest("2", "Anxiety Test", "Psychological", "https://example.com/test_anxiety.json", "Test for assessing anxiety levels."),
				CreateTest("3", "Self-Esteem Test", "Psychological", "https://example.com/test_selfesteem.json", "Test for determining self-esteem.")
			};
		}
		public static Test CreateTest(string id, string name, string category, string jsonUrl, string description)
		{
			return new Test
			{
				Id = id,
				Name = name,
				Category = category,
				JsonUrl = jsonUrl,
				Status = "Active",
				CreatedAt = DateTime.Now.AddDays(-10),
				UpdatedAt = DateTime.Now,
				Description = description
			};
		}
	}

}