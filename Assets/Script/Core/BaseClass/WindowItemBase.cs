using UnityEngine;
using System.Collections;

public enum WindowName
{
	BoardWindow = 0,
	OptionWindow,
}

public enum TransitionType
{
	TopToBottom = 0,
	BottomToTop,
	LeftToRight,
	RightToLeft,
}

public class WindowItemBase : MonoBehaviour {
	public string name { set; get;}
	private Animator _animator;

	protected virtual void Awake()
	{
		_animator = GetComponent<Animator> ();
	}

	public virtual void PreLoad()
	{
		gameObject.SetActive (true);
		OnDataLoaded ();
	}

	public virtual void OnDataLoaded()
	{
		WindowManager.Instance.OnWindowInitDone (this);
	}

	public virtual void TransitionIn(TransitionType trans)
	{
		string transIn = trans.ToString() + "In";
		if (_animator != null)
						_animator.Play (transIn);
	}

	public virtual void OnTransInFinish()
	{
		WindowManager.Instance.OnWindowMoveDone (this);
	}

	public virtual void TransitionOut(TransitionType trans)
	{
		string transOut = trans.ToString() + "Out";
		if (_animator != null)
						_animator.Play (transOut);
	}

	public virtual void OnTransOutFinish()
	{
		gameObject.SetActive (false);
	}
}
