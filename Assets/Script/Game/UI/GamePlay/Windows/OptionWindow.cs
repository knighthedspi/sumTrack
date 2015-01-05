using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionWindow : WindowItemBase {


	private int _index = 0;
	private UIGrid _uigrid;
	private List<OptionItem> _listItem;

	public List<GameObject> listPrefab;

	public int optionWidth 	= 200;
	public UILabel title;
	Vector3 originPos;

	protected override void Awake ()
	{
		base.Awake ();
		_uigrid = GetComponentInChildren<UIGrid>();
		_uigrid.cellWidth = optionWidth;
		_listItem = new List<OptionItem>();
	}

	void Start()
	{
		originPos = _uigrid.transform.localPosition;
	}

	public override void PreLoad ()
	{
		if(_listItem.Count <= 0)
			CreateOption();
		base.PreLoad ();
	}

	public override void OnDataLoaded ()
	{
		ChangeCurrentOption(_index);
		base.OnDataLoaded ();
	}

	public void OnMoveLeft()
	{
		if(_index <= 0) return;
		ChangeCurrentOption(_index -1);
	}

	public void OnMoveRight()
	{
		if(_index >= _listItem.Count-1) return;
		ChangeCurrentOption(_index + 1);
	}

	public void OnHomeBtnClick()
	{
		WindowManager.Instance.ChangeWindow (WindowName.BoardWindow,TransitionType.BottomToTop);
	}
	private void ChangeCurrentOption(int index)
	{
		Vector3 target = originPos - new Vector3(index * optionWidth, 0,0);
		Vector3 origin = _uigrid.transform.localPosition;
		_index = index;
		GamePlayService.MoveToAnimation(_uigrid.gameObject,origin,target,0.1f,"OnMoveFinish",this.gameObject);
	}

	private void ShowTitle()
	{
		title.text = _listItem[_index].optionName;
		GamePlayService.MoveToAnimation(title.gameObject,new Vector3(0,550,0),new Vector3(0,330,0),0.1f);
	}

	private void CreateOption()
	{
		for(int i = 0; i < listPrefab.Count; i ++)
		{
			GameObject go = NGUITools.AddChild(_uigrid.gameObject,listPrefab[i]);
			OptionItem ot = go.GetComponent<OptionItem>();
			_listItem.Add(ot);
			ot.optionName = "Option" + i.ToString();
		}
	}

	public void OnMoveFinish()
	{
		ShowTitle();
	}


}
