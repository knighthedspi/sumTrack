using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlay : MonoBehaviour {

	public static GamePlay Instance;

	public Transform footerLeft;
	public Transform footerRight;
	public Transform headerLeft;
	public Transform headerRight;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("start------------------");
		WindowManager.Instance.ChangeWindow (WindowName.BoardWindow,TransitionType.TopToBottom);
	}




}
