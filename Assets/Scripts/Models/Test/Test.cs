using System;
using UnityEngine.UI;

[Serializable]
public class Test
{
	public string Id;
	public string Name;
	public string Category;
	public string JsonUrl;
	public string Status;
	public DateTime CreatedAt;
	public DateTime UpdatedAt;
	public Image Logo;
	public string Description;
	public TestContainer Container;
}