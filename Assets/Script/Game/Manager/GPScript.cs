using UnityEngine;
using System.Collections;

public class GPScript : MonoBehaviour {

	void Awake () {
		GrowthPush.Initialize(3822, "My9i44cylY5BnLHVL28kWFWZBfAeH4n3", GrowthPush.Environment.Development, true, "763147213931");
		GrowthPush.TrackEvent("Launch");
		GrowthPush.SetDeviceTags();
		GrowthPush.ClearBadge();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
