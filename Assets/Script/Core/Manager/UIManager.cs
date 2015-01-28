using UnityEngine;
using System.Collections;

public class UIManager : Singleton<UIManager> {

	public GameObject disableObj;
	private bool _isDisable;
	private DisableUI _disableUI;

	public UIRoot uiRoot{ private set; get; }
	public bool disable
	{
		get
		{
			return _isDisable;
		}
		set
		{
			if( _isDisable == value)
				return;
			_isDisable = value;
			if(uiRoot == null)
				CheckUIRoot();
			if(_isDisable)
			{
				GameObject go 	= NGUITools.AddChild(uiRoot.gameObject,disableObj);
				_disableUI 		= go.GetComponent<DisableUI>();
				NGUITools.BringForward(go);
				_disableUI.Show();
			}
			else
			{
				if(_disableUI != null && !_disableUI.gameObject.Equals(null))
				{
					_disableUI.Hide();
				}
				_disableUI = null;
			}
		}

	}

	void Awake()
	{

	}

	private void CheckUIRoot()
	{
		if (uiRoot != null)
						return;
		GameObject obj = GameObject.Find ("GamePlay");
		if (obj != null)
			uiRoot = obj.GetComponent<UIRoot> ();
		else
			Debug.LogError("Has no uiroot global");

	}
}
