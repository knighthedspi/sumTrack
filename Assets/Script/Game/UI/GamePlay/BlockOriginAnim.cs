using UnityEngine;
using System.Collections;

public class BlockOriginAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UIWidget widget = GetComponent<UIWidget> ();
		widget.depth = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnAnimComplete()
	{
		Destroy (gameObject);
	}
}
