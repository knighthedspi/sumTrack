using UnityEngine;
using System.Collections;

public class DialogHint : DialogBase {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBackgroundClick()
	{
		DialogManager.Instance.Complete ();
	}

	public static void Create()
	{
		DialogData dialogData = new DialogData ();
		dialogData.dialogType = DialogType.DialogHint;
		DialogManager.Instance.OpenDialog (dialogData);
	}
}
