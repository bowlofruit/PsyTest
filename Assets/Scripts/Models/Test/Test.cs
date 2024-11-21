using System;
using UnityEngine;

[Serializable]
public class Test
{
	public string Id;
	public string Name;
	public string Category;
	public string JsonUrl;
	public string Status; //TODO Save data
	public DateTime CreatedAt;
	public DateTime UpdatedAt;
	public Sprite Logo;
	public string Description;
	public TestContainer Container;
}