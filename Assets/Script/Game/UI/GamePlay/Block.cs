﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {

	const string CIRCLE_BG 		= "circle_bg";
	const string CIRCLE_NORMAL	= "circle0";
	const string CIRCLE_TWICE	= "circle1";
	const string CIRCLE_TRI		= "circle2";
	const string CIRCLE_DONE	= "done";
	const string RETANGLE_BG	= "retangle1";
	const string RETANGLE_NORMAL	= "retangle0";
	const string RETANGlE_DONE_BG 	= "done0";
	const string RETANGlE_DONE_FG 	= "done1";
	const string START				= "start";

	private BlockInfo _info;
	private GamePlay _gamePlay;
	private GamePlayInfo _gameSetup;
	private List<Block> _listMove;
	private bool _isMoving = false;
	private UIWidget _uiwidget;


	public UISprite background;
	public UISprite foreground;
	public UILabel numLabel;

	public BlockInfo blockInfo
	{
		set
		{
			_info 			= value;
			numLabel.text 	= (_info.num == 0) ? "" : _info.num.ToString();
			SetImageByType(_info.type);
			transform.localPosition = GamePlayService.ConvertToPosition(_info.posInBoard);
		}
		get
		{
			return _info;
		}
	}


	void Awake()
	{
		_gamePlay = GamePlay.Instance;
		_gameSetup = AppManager.Instance.gamePlayInfo;
		_listMove = new List<Block> ();
		_uiwidget = GetComponent<UIWidget> ();
	}

	void Start()
	{

	}

	public void OnSelected()
	{

	}

	public void OnOriginPassed()
	{
		int newType = (int)blockInfo.type - 1;
		if (newType < (int)BlockType.normalDone || newType > (int)BlockType.normalTri)
						return;
		BlockInfo newInfo 	= new BlockInfo (blockInfo);
		newInfo.type 		= (BlockType)newType;
		blockInfo = newInfo;

	}

	public void MoveTo(Block block)
	{
		BlockInfo info = block.blockInfo;

		if (info.type == BlockType.start || info.type == BlockType.normalDone || info.type == BlockType.originDone)
						return;
		if (_listMove.Find (x => x.blockInfo.posInBoard == block.blockInfo.posInBoard) != null)
						return;
		_listMove.Add (block);
		ProcessMove ();

	}

	private void ProcessMove()
	{
		Debug.Log ("list move ----- " + _listMove.Count.ToString ());
		if (_isMoving || _listMove.Count <= 0)
						return;
		if(GamePlayService.CheckNeibor( blockInfo.posInBoard, _listMove[0].blockInfo.posInBoard))
		{
			_isMoving = true;
			Vector3 target = _listMove [0].transform.localPosition;
			iTween.MoveTo (gameObject, iTween.Hash ("position", target, "time", 0.5f, "isLocal", true, "oncomplete", "OnMoveComplete"));
		}
		else 
		{
			_listMove.RemoveAt(0);
			ProcessMove();
		}

	}

	private void OnMoveComplete()
	{
		OnSelected ();

		Block passedBlock 	= _listMove [0];

		BlockInfo newInfo 		= new BlockInfo (blockInfo);
		newInfo.posInBoard 		= passedBlock.blockInfo.posInBoard;
		newInfo.num 			= blockInfo.num - passedBlock.blockInfo.num;
		newInfo.type 			= (newInfo.num == 0) ? BlockType.originDone : newInfo.type;
		blockInfo = newInfo;

		passedBlock.OnOriginPassed ();
		_listMove.RemoveAt (0);
		StartCoroutine (AfterMove());
	}

	IEnumerator AfterMove()
	{
		yield return new WaitForSeconds (0.2f);
		_isMoving = false;
		ProcessMove ();
	}




	private void SetImageByType(BlockType type)
	{
		_uiwidget.depth = 1;
		switch (type)
		{
		case BlockType.normal:
			background.spriteName = CIRCLE_BG;
			foreground.spriteName = CIRCLE_NORMAL;
			break;

		case BlockType.normalDone:
			background.spriteName = "";
			foreground.spriteName = CIRCLE_DONE;
			break;

		case BlockType.normalTri:
			background.spriteName = CIRCLE_BG;
			foreground.spriteName = CIRCLE_TRI;
			break;

		case BlockType.normalTwice:
			background.spriteName = CIRCLE_BG;
			foreground.spriteName = CIRCLE_TWICE;
			break;

		case BlockType.origin:
			background.spriteName = RETANGLE_BG;
			foreground.spriteName = RETANGLE_NORMAL;
			background.GetComponent<UIWidget>().depth = 4;
			foreground.GetComponent<UIWidget>().depth = 5;
			numLabel.GetComponent<UIWidget>().depth = 6;
			_uiwidget.depth = 3;
			break;

		case BlockType.start:
			background.spriteName = "";
			foreground.spriteName = START;
			break;

		case BlockType.originDone:
			background.spriteName = RETANGlE_DONE_BG;
			foreground.spriteName = RETANGlE_DONE_FG;
			numLabel.gameObject.SetActive(false);
			break;
		}

	}




}
