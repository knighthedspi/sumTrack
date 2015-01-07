using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class MapData
{
	[SerializeField]
	int level;
	
	[ExposeProperty]
	public int Level { get {return level; } set { level = value;} }
	
	[SerializeField]
	string map;
	
	[ExposeProperty]
	public string Map { get {return map; } set { map = value;} }
	
	[SerializeField]
	int width;
	
	[ExposeProperty]
	public int Width { get {return width; } set { width = value;} }
	
	[SerializeField]
	int height;
	
	[ExposeProperty]
	public int Height { get {return height; } set { height = value;} }
	
}