using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class mapData
{
	[SerializeField]
	int level;
	
	[ExposeProperty]
	public int Level { get {return level; } set { level = value;} }
	
	[SerializeField]
	string normal;
	
	[ExposeProperty]
	public string Normal { get {return normal; } set { normal = value;} }
	
	[SerializeField]
	string normaltwice;
	
	[ExposeProperty]
	public string Normaltwice { get {return normaltwice; } set { normaltwice = value;} }
	
	[SerializeField]
	string normaltri;
	
	[ExposeProperty]
	public string Normaltri { get {return normaltri; } set { normaltri = value;} }
	
	[SerializeField]
	string origin;
	
	[ExposeProperty]
	public string Origin { get {return origin; } set { origin = value;} }
	
}