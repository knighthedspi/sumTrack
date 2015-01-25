using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionWindow : WindowItemBase {


	private int _index = 0;
	private UIScrollView _uigrid;
	private List<OptionItem> _listItem;

	public GameObject optionItemPreab;

	public int headerHeight 	= 200;

	public GameObject scrollFooter;

	private int currentLevel = 1;
	private List<LevelBlock> _listBlock;
//	public UILabel title;
	

	Vector3 originPos;

	protected override void Awake ()
	{
		base.Awake ();
		_uigrid = GetComponentInChildren<UIScrollView>();
//		_uigrid.cellWidth = optionWidth;
		_listItem = new List<OptionItem>();
		currentLevel = AppManager.Instance.playingLevel;
		_listBlock = new List<LevelBlock> ();
	}

	void Start()
	{
		originPos = _uigrid.transform.localPosition;
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
		int itemNum = AppManager.Instance.maxLevel / 17 + 1;
		for(int i = 0; i < itemNum; i ++)
		{
			GameObject go = NGUITools.AddChild(_uigrid.gameObject, optionItemPreab);
			OptionItem ot = go.GetComponent<OptionItem>();
			_listItem.Add(ot);
			ot.Init(i);
			ot.transform.localPosition = new Vector3(0,- 1024*i - headerHeight,0);
			ot.optionName = "Option" + i.ToString();
			_listBlock.AddRange(ot.blocks);
		}
		scrollFooter.transform.localPosition = new Vector3 (0, -1024 * (_listItem.Count - 0.5f) - headerHeight -50, 0);
	}

	public void OnMoveFinish()
	{
		if(title != null)
		{
			title.text = _listItem [_index].optionName;
			ShowTitle();
		}
			

	}


}
