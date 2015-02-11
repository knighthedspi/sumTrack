using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlay : View {

	public static GamePlay Instance;

	public Transform footerLeft;
	public Transform footerRight;
	public Transform headerLeft;
	public Transform headerRight;

	public List<History> history;

	private string BGM = "BGM_InGame";

	void Awake()
	{
		Instance = this;
		history = new List<History> ();
	}

	void Start()
	{
		Debug.Log("start------------------");
		StartCoroutine (WaitAndSetup ());
	}

	protected override void OnOpen (params object[] parameters)
	{
		if(SoundManager.isSound)
			SoundManager.Instance.PlayBGM(BGM);
	}

	private IEnumerator WaitAndSetup()
	{
		yield return new WaitForEndOfFrame ();
		if(AppManager.Instance.playingMaxLevel <= 1)
		{
			WindowManager.Instance.ChangeWindow (WindowName.BoardWindow,TransitionType.TopToBottom);
		}
		else
		{
			WindowManager.Instance.ChangeWindow (WindowName.OptionWindow,TransitionType.TopToBottom);
		}

		if (GoogleAnalytics.instance)
			GoogleAnalytics.instance.LogScreen("Game play: level " + AppManager.Instance.playingLevel);
	}

}
