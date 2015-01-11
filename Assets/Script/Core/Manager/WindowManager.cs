using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WindowManager : Singleton<WindowManager> {

	private List<WindowItemBase> _windowItems;
	private WindowItemBase _currentWindow;
	private bool _isChanging = false;
	private TransitionType _transType;

	public Transform footerLeft;
	public Transform footerRight;
	public Transform headerLeft;
	public Transform headerRight;


	void Awake()
	{
		_windowItems = new List<WindowItemBase> ();
	}

	public void ChangeWindow(WindowName wName,TransitionType transType)
	{
		string windowName = wName.ToString ();
		if ((_currentWindow != null && _currentWindow.name == windowName) || _isChanging)
						return;
		_isChanging = true;
		WindowItemBase windowItem 	= _windowItems.Find (x => x.name == windowName);
		if(windowItem == null)
		{
			WindowItemBase wib = FindWindow(windowName);
			windowItem = (wib == null) ? CreateWindow(windowName) : wib;
		}
		windowItem.name 			= windowName;
		_transType 					= transType;
		windowItem.PreLoad ();
	}

	public void OnWindowInitDone(WindowItemBase item)
	{
		if (!_isChanging)
						return;
		if(_currentWindow != null)
		{
			_currentWindow.TransitionOut (_transType);
			item.TransitionIn (_transType);
		}
		else 
		{
			// init first window
			item.transform.localPosition = Vector3.zero;
			item.ShowHeaderFooter();
			OnWindowMoveDone(item);
		}

	}

	public void OnWindowMoveDone(WindowItemBase item)
	{
		if (!_isChanging)
						return;
		_currentWindow = item;
		_isChanging = false;

	}

	private WindowItemBase FindWindow(string name)
	{
		Transform wdow = transform.Find (name);
		WindowItemBase item = (wdow == null) ? null : wdow.GetComponent<WindowItemBase> ();
		return item;
	}

	private WindowItemBase CreateWindow(string name)
	{
		GameObject go = Resources.Load (Config.WINDOW_PATH + name) as GameObject;
		GameObject wd = NGUITools.AddChild (gameObject, go);
		WindowItemBase item = wd.GetComponent<WindowItemBase> ();

		if (item == null)
						Debug.LogError ("has no window item script attach");
		else 
						_windowItems.Add (item);
		return item;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
