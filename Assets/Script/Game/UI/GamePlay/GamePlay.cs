using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlay : MonoBehaviour {

	public static GamePlay Instance;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("start------------------");
		WindowManager.Instance.ChangeWindow (WindowName.Board,TransitionType.TopToBottom);
	}




}
