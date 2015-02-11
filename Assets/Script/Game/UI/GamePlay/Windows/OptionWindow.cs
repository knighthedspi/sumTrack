using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionWindow : WindowItemBase {


	private int _index = 0;

	private List<OptionItem> _listItem;

	public GameObject optionItemPreab;


	public GameObject menuBar;
	public GameObject grid;

	const int BLOCK_PER_PAGE = 17;
	private int currentLevel = 1;
	private int _currentPage = 0;
	private List<Vector2> _touchList;
	private List<LevelBlock> _listBlock;
	private bool _isMoving = false;
	private bool _isShowMenu = false;
	private Color[] colors = new Color[] {Color.red, Color.blue, Color.yellow,Color.black, Color.gray};
//	public UILabel title;
	

	Vector3 originPos;

	protected override void Awake ()
	{
		base.Awake ();
//		_uigrid.cellWidth = optionWidth;
		_listItem = new List<OptionItem>();
		currentLevel = AppManager.Instance.playingLevel;
		_listBlock = new List<LevelBlock> ();
		_currentPage = currentLevel / BLOCK_PER_PAGE;
		_touchList = new List<Vector2> ();

	}

	void Start()
	{
	}

	public override void PreLoad ()
	{
		if(_listItem.Count <= 0)
		{
			CreateOption();

		}

			
			
		base.PreLoad ();
	}

	public override void OnDataLoaded ()
	{
		base.OnDataLoaded ();
	}

	public override void OnTransInFinish ()
	{
		base.OnTransInFinish ();
		StartCoroutine(UpdateStatus());
	}



	public void OnHomeBtnClick()
	{
		WindowManager.Instance.ChangeWindow (WindowName.BoardWindow,TransitionType.TopToBottom);
	}

	public void OnReviewBtnCLick()
	{
#if UNITY_ANDROID
		Application.OpenURL("market://details?id=vn.ktech.sumtrack/");
#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/app/id966189502");
#endif
	}

	public IEnumerator UpdateStatus()
	{
		List<LevelBlock> lists = _listBlock.FindAll (x => x.level >= currentLevel && x.level < AppManager.Instance.playingLevel);
		foreach(LevelBlock lb in lists)
		{
			lb.level = lb.level;
			yield return new WaitForSeconds(0.2f);
		}
		currentLevel = AppManager.Instance.playingLevel;
	}




	private void CreateOption()
	{
		int itemNum = AppManager.Instance.maxLevel / BLOCK_PER_PAGE + 1;
		for(int i = 0; i < itemNum; i ++)
		{
			GameObject go = NGUITools.AddChild(grid, optionItemPreab);
			OptionItem ot = go.GetComponent<OptionItem>();
			_listItem.Add(ot);
			ot.Init(i);
			ot.transform.localPosition = new Vector3(0,- 1024*i ,0);
			ot.optionName = "Option" + i.ToString();
			_listBlock.AddRange(ot.blocks);

			// item color
			Color cl = (i < colors.Length) ? colors[i] : colors[colors.Length -1];
			ot.bgColor = cl;
		}
//		scrollFooter.transform.localPosition = new Vector3 (0, -1024 * (_listItem.Count - 0.5f) - headerHeight -50, 0);
	}

	public void OnMoveFinish()
	{
		if(title != null)
		{
			title.text = _listItem [_index].optionName;
			ShowTitle();
		}
			

	}

	public void OnMenuBtnClick()
	{
		Debug.Log("On menu button click");
		ShowMenu ();
	}


	// Menu bar
	private void ShowMenu()
	{
		if (_isShowMenu)
						return;
		iTweenEvent.GetEvent (menuBar, "Show").Play ();
	}

	private void HideMenu()
	{
		if (!_isShowMenu)
						return;
		iTweenEvent.GetEvent (menuBar, "Hide").Play ();
	}

	private void OnShowComplete()
	{
		_isShowMenu = true;
	}

	private void OnHideComplete()
	{
		_isShowMenu = false;
	}

	// End Menu bar

	public void OnMoveTop()
	{
		if (_isMoving)
						return;
		_currentPage = (_currentPage <= 0) ? 0 : _currentPage - 1;
		MoveToPage (_currentPage);
	}

	public void OnMoveDown()
	{
		if (_isMoving)
						return;
		_currentPage = (_currentPage >= (_listItem.Count - 1)) ? _listItem.Count - 1 : _currentPage + 1;
		MoveToPage (_currentPage);
	}

	private void MoveToPage(int page)
	{
		if (page < 0 || page >= _listItem.Count)
						return;
		_isMoving = true;
		iTweenEvent.GetEvent (menuBar, "Hide").Play ();
		Vector3 newPos = new Vector3 (0, page * 1024, 0);
		iTween.MoveTo (grid,iTween.Hash(
			"position",newPos,
			"isLocal",true,
			"time",0.7f,
			"easetype",iTween.EaseType.linear,
			"oncomplete","moveComplete",
			"oncompletetarget",this.gameObject));
	}

	private void moveComplete()
	{
		_isMoving = false;
	}

	// Touch event

	void Update()
	{
		if(Input.GetMouseButtonUp(0))
		{
			HideMenu();
		}
		if(Input.GetMouseButton(0))
		{
			Vector2 touch = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			Debug.Log(touch);
			_touchList.Add(touch);
			if(_touchList.Count >= 3)
			{
				float angle = Vector2.Angle(Vector2.up,_touchList[2] - _touchList[0]);

				if(angle >= 135)
				{
					OnMoveTop();
				}
				else if(angle <= 45)
				{

					OnMoveDown();
				}
				_touchList.Clear();
			}
		}
		else
		{
			_touchList.Clear();
		}
	}


}
