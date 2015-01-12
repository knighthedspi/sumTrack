using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {

	const string CIRCLE_BG 		= "";
	const string CIRCLE_NORMAL	= "lv1";
	const string CIRCLE_TWICE	= "lv2";
	const string CIRCLE_TRI		= "lv3";
	const string CIRCLE_DONE	= "done";
	const string RETANGLE_BG	= "origin";
	const string RETANGLE_NORMAL	= "";
	const string RETANGlE_DONE_BG 	= "done1";
	const string RETANGlE_DONE_FG 	= "";
	const string START				= "";
	const string ERROR 			= "error";

	private BlockInfo _info;
	private GamePlay _gamePlay;
	private GamePlayInfo _gameSetup;
	private List<Block> _listMove;
	private bool _isMoving = false;
	private UIWidget _uiwidget;


	public UISprite background;
	public UISprite foreground;
	public UILabel numLabel;

	public delegate void MoveComplete();
	public MoveComplete moveComplete;

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
		// if block changed to done
		if(blockInfo.type != BlockType.origin)
		{
			_listMove.Clear();
			return ;
		}
		if(GamePlayService.CheckNeibor( blockInfo.posInBoard, _listMove[0].blockInfo.posInBoard))
		{
			_isMoving = true;
			Vector3 target = _listMove [0].transform.localPosition;
			iTween.MoveTo (gameObject, iTween.Hash ("position", target, "time", 0.2f, "isLocal", true,"easetype","easeOutElastic", "oncomplete", "OnMoveComplete"));
		}
		else 
		{
			_listMove.RemoveAt(0);
			ProcessMove();
		}

	}

	public void Destroy()
	{
		Destroy (gameObject);
	}

	private void OnMoveComplete()
	{
		OnSelected ();

		Block passedBlock 	= _listMove [0];

		BlockInfo newInfo 		= new BlockInfo (blockInfo);
		newInfo.posInBoard 		= passedBlock.blockInfo.posInBoard;
		newInfo.num 			= blockInfo.num - passedBlock.blockInfo.num;
		if(newInfo.num == 0)
		{
			newInfo.type = BlockType.originDone;
		}
		else if(newInfo.num < 0)
		{
			newInfo.type = BlockType.error; 
		}

		blockInfo = newInfo;

		passedBlock.OnOriginPassed ();
		_listMove.RemoveAt (0);
		moveComplete ();
		StartCoroutine (AfterMove());
	}

	IEnumerator AfterMove()
	{
		yield return new WaitForSeconds (0.2f);
		_isMoving = false;
		ProcessMove ();
	}


//	public void ScaleAnimWhenFinish(string complete, GameObject targer)
//	{
//		GamePlayService.ScaleTo (gameObject,transform.localScale,Vector3(1.1f,1.1f,1),0.1f,);
//	}



	private void SetImageByType(BlockType type)
	{
		_uiwidget.depth = 1;
		numLabel.color = Color.black;
		switch (type)
		{
		case BlockType.normal:
			background.spriteName = CIRCLE_BG;
			foreground.spriteName = CIRCLE_NORMAL;
			break;

		case BlockType.normalDone:
			background.spriteName = "";
			foreground.spriteName = CIRCLE_DONE;
			transform.localScale = Vector3.zero;
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
			numLabel.color = Color.white;
			_uiwidget.depth = 3;
			break;

		case BlockType.start:
			background.spriteName = "";
			foreground.spriteName = START;
			transform.localScale = Vector3.zero;
			break;

		case BlockType.originDone:
			background.spriteName = RETANGlE_DONE_BG;
			foreground.spriteName = RETANGlE_DONE_FG;

			numLabel.gameObject.SetActive(false);
			break;
		case BlockType.error:
			background.spriteName = ERROR;
//			numLabel.text = "";
			break;
		}

	}




}
