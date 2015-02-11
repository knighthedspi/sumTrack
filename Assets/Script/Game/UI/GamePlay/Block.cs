using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : MonoBehaviour {

	const string CIRCLE_NORMAL	= "lv1";
	const string CIRCLE_TWICE	= "lv2";
	const string CIRCLE_TRI		= "lv3";
	const string CIRCLE_QUAD	= "lv4";
	const string CIRCLE_DONE	= "check1";
	const string RETANGLE_BG	= "start";
	const string RETANGLE_NORMAL	= "";
	const string RETANGlE_DONE_BG 	= "done";
	const string RETANGlE_DONE_FG 	= "";
	const string START				= "check2";
	const string ERROR 			= "error";

	private BlockInfo _info;
	private GamePlay _gamePlay;
	private GamePlayInfo _gameSetup;
	private List<Block> _listMove;
	private bool _isMoving = false;
	private UIWidget _uiwidget;


	public UISprite background;
	public UILabel numLabel;

	public BlockOriginAnim anim;

	public delegate void MoveComplete();
	public MoveComplete moveComplete;

	private string SE_MC = "SE_Enter";

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
		if (anim == null)
						return;
		GameObject go = NGUITools.AddChild (gameObject, anim.gameObject);


	}

	public void OnOriginPassed()
	{
		int newType = (int)blockInfo.type - 1;
		if (newType < (int)BlockType.normalDone || newType > (int)BlockType.normalQuad)
						return;
		BlockInfo newInfo 	= new BlockInfo (blockInfo);
		newInfo.type 		= (BlockType)newType;
		blockInfo = newInfo;

	}

	public void MoveTo(Block block)
	{
		BlockInfo info = block.blockInfo;

		if (info.type == BlockType.start || info.type == BlockType.normalDone || 
		    info.type == BlockType.originDone || info.type == BlockType.error)
						return;
		if (_listMove.Find (x => x.blockInfo.posInBoard == block.blockInfo.posInBoard) != null)
						return;
		_listMove.Add (block);
		ProcessMove ();

	}
	

	private void ProcessMove()
	{
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
			iTween.MoveTo (gameObject, iTween.Hash (
				"position", target,
				"time", 0.2f, 
				"isLocal", true,
				"easetype","easeOutElastic",
				"oncomplete", "OnMoveComplete"));
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

	//#TODO add sound

	private void OnMoveComplete()
	{
		OnSelected ();

		Block passedBlock 	= _listMove [0];

		//------ history -------
		History his = new History ();
		his.origin 	= new BlockInfo (blockInfo);
		his.after 	= new BlockInfo (passedBlock.blockInfo);
		his.number = _gamePlay.history.Count + 1;
		_gamePlay.history.Add (his);

		//-----



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

		SoundManager.Instance.PlaySE(SE_MC);
	}

	IEnumerator AfterMove()
	{
		yield return new WaitForSeconds (0.2f);
		_isMoving = false;
		ProcessMove ();
	}


	public void ScaleAnimWhenFinish()
	{
		Vector3 scaleTo = (blockInfo.type == BlockType.originDone) ? new Vector3 (1.1f, 1.1f, 1.1f) : Vector3.one;

		GamePlayService.ScaleTo (gameObject,transform.localScale, scaleTo,0.15f,"easeOutBack");
	}



	private void SetImageByType(BlockType type)
	{
		_uiwidget.depth = 1;

		UIWidget uwBG = background.GetComponent<UIWidget> (); 
		uwBG.height = (int) blockInfo.sizeImage.y;
		uwBG.width = (int) blockInfo.sizeImage.x;

		numLabel.color = Color.black;
		switch (type)
		{
		case BlockType.normal:
			background.spriteName = CIRCLE_NORMAL;
			break;

		case BlockType.normalDone:
			background.spriteName = CIRCLE_DONE;
			transform.localScale = Vector3.zero;
			break;

		case BlockType.normalTri:
			background.spriteName = CIRCLE_TRI;
			break;

		case BlockType.normalTwice:
			background.spriteName = CIRCLE_TWICE;
			break;
		case BlockType.normalQuad:
			background.spriteName = CIRCLE_QUAD; 
			break;

		case BlockType.origin:
			background.spriteName = RETANGLE_BG;
			background.GetComponent<UIWidget>().depth = 4;
			numLabel.GetComponent<UIWidget>().depth = 6;
			numLabel.color = Color.white;
			_uiwidget.depth = 3;
			break;

		case BlockType.start:
			background.spriteName = START;
			transform.localScale = Vector3.zero;
			break;

		case BlockType.originDone:
			background.spriteName = RETANGlE_DONE_BG;
			background.GetComponent<UIWidget>().depth = 4;
			_uiwidget.depth = 3;
			numLabel.text = "";
			break;
		case BlockType.error:
			background.spriteName = ERROR;
			break;

		}

	}




}
