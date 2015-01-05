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

	public GameObject header;
	public GameObject footer;
	public UILabel title;

	protected virtual void Awake()
	{
		_animator = GetComponent<Animator> ();
	}

	public virtual void PreLoad()
	{
		gameObject.SetActive (true);

		if(header != null)
		{
			header.transform.localPosition = Config.HEADER_OUT;
			header.SetActive(false);
		}
			
		if(footer != null)
		{
			footer.transform.localPosition = Config.FOOTER_OUT;
			footer.SetActive(false);
		}
		if (title != null)
						title.gameObject.SetActive (false);

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
		ShowHeaderFooter ();
		WindowManager.Instance.OnWindowMoveDone (this);
	}

	public virtual void TransitionOut(TransitionType trans)
	{
		string transOut = trans.ToString() + "Out";
		HideHeaderFooter ();
		if (_animator != null)
						_animator.Play (transOut);
	}

	public virtual void OnTransOutFinish()
	{
		gameObject.SetActive (false);
	}

	public void ShowHeaderFooter()
	{
		if (header != null)
		{
			header.SetActive(true);
			GamePlayService.MoveToAnimation (header, Config.HEADER_OUT, Config.HEADER_IN, 0.5f);
		}
			
		if(footer != null)
		{
			footer.SetActive(true);
			GamePlayService.MoveToAnimation (footer, Config.FOOTER_OUT, Config.FOOTER_IN, 0.5f);
		}
		ShowTitle ();

	}

	public void HideHeaderFooter()
	{
		if (header != null)
						GamePlayService.MoveToAnimation (header, Config.HEADER_IN, Config.HEADER_OUT, 0.5f);
		if(footer != null)
				GamePlayService.MoveToAnimation (footer, Config.FOOTER_IN, Config.FOOTER_OUT, 0.5f);

	}

	protected void ShowTitle()
	{
		if (title == null)
						return;
		title.gameObject.SetActive (true);
		GamePlayService.MoveToAnimation(title.gameObject,new Vector3(0,550,0),new Vector3(0,330,0),0.1f);
	}
}
