using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardWindow : WindowItemBase {
	private Dictionary<int, Board> dicBoard ;
	private Board _currentBoard;
	private int _nextLevel;

	public GameObject boardPrefab;
	public Transform homeBtn;
	public Transform soundBtn;
	public Transform undoBtn;
	public Transform replayBtn;
	public Transform clearObj;
	public Transform footerText;
	public Transform share;
	public Transform next;

	private bool _isProcessing = false;

	private string SE_CLEAR = "Jingle_Clear";

	protected override void Awake ()
	{
		base.Awake ();
		dicBoard = new Dictionary<int,Board > ();
	}

	public override void PreLoad ()
	{
		// TODO : fix level , should be loaded from appManager
		int level = AppManager.Instance.playingLevel;
		LoadLevel (level);
		base.PreLoad ();
	}

	public void LoadLevel(int level)
	{
		if (_currentBoard != null && level == AppManager.Instance.playingLevel)
						return;
		_nextLevel = level;
		Board board = dicBoard.ContainsKey (level) ? dicBoard [level] : CreateNewBoard (level);

		title.text = string.Format ("Level " + level.ToString ());
		if(_currentBoard != null)
		{
			Vector3 oldPos = board.transform.localPosition;
			GamePlayService.MoveToAnimation(board.gameObject,oldPos + new Vector3(1000,0,0),oldPos,0.5f,"OnLevelLoaded",this.gameObject);
			GamePlayService.MoveToAnimation(_currentBoard.gameObject,_currentBoard.transform.localPosition,
			                                _currentBoard.transform.localPosition + new Vector3(-1000,0,0),0.5f);
		}
		else
		{
			_currentBoard = board;
			StartCoroutine(_currentBoard.StartGameAnim());
		}



	}

	void Start()
	{
		// setup header footer button
		homeBtn.localPosition 	= new Vector3 (WindowManager.Instance.headerLeft.localPosition.x,0,0);
		soundBtn.localPosition 	= new Vector3 (WindowManager.Instance.headerRight.localPosition.x,0,0);
		undoBtn.localPosition 	= new Vector3 (WindowManager.Instance.footerLeft.localPosition.x,0,0);
		replayBtn.localPosition 	= new Vector3 (WindowManager.Instance.footerRight.localPosition.x,0,0);
	}

	public void OnLevelLoaded()
	{
		ShowHeaderFooter ();
		_currentBoard.gameObject.SetActive (false);
		AppManager.Instance.playingLevel = _nextLevel;
		_currentBoard = dicBoard [_nextLevel];
		StartCoroutine (_currentBoard.StartGameAnim ());

		_isProcessing = false;
	}
	private Board CreateNewBoard(int level)
	{
		GameObject go = NGUITools.AddChild (gameObject, boardPrefab) as GameObject;
		Board board = go.GetComponent<Board> ();
		if(board == null)
		{
			Debug.LogError("not found board scrip attach");
		}
		else
		{
			board.Init (level);
			board.finish = OnGameFinish;
			dicBoard[level] = board;
		}
		return board;
	}

	public void OnOptionBtnClick()
	{
		WindowManager.Instance.ChangeWindow (WindowName.OptionWindow, TransitionType.BottomToTop);
	}

	public void OnResetBtnClick()
	{
		dicBoard [AppManager.Instance.playingLevel].ResetGameAnim ();
	}

	public void OnUndoBtnClick()
	{
		dicBoard [AppManager.Instance.playingLevel].OnUndoAction ();
	}

	//#TODO add sound
	public void OnGameFinish()
	{
		HideHeaderFooter ();
		StartCoroutine (TitleAnimWhenFinish());
//		StartCoroutine (NextLevel());
		SoundManager.Instance.PlaySE(SE_CLEAR);
	}

	IEnumerator NextLevel()
	{
//		yield return new WaitForSeconds (2f);
		title.color = Color.white;
		title.fontSize = 45;
		GamePlayService.MoveToAnimation (share.gameObject,share.localPosition , share.localPosition +
		                                 new Vector3 (-1000, 0, 0), 0.2f);
		
		GamePlayService.MoveToAnimation (next.gameObject,next.localPosition , next.localPosition  +  
		                                 new Vector3 (1000, 0, 0), 0.2f);

		GamePlayService.MoveToAnimation (title.gameObject, title.transform.localPosition, title.transform.localPosition +
		                                 new Vector3 (0,500,0), 0.2f);
		// cleaer label
		GamePlayService.MoveToAnimation (clearObj.gameObject, clearObj.localPosition, clearObj.localPosition +
		                                 new Vector3(1000,0,0), 0.2f);
		GamePlayService.MoveToAnimation (footerText.gameObject,footerText.localPosition , footerText.localPosition +
		                                 new Vector3 (1000, 0, 0), 0.2f);

		yield return new WaitForSeconds (0.2f);
		LoadLevel (AppManager.Instance.playingLevel + 1);
	}


	private IEnumerator TitleAnimWhenFinish()
	{
		float titlePosY = _currentBoard.boardSize.y / 2 + 100;
		clearObj.localPosition = new Vector3 (-1000,titlePosY,0);
		// title
		GamePlayService.MoveToAnimation (title.gameObject, title.transform.localPosition, new Vector3 (0,titlePosY + 30,0), 0.3f);
		yield return new WaitForSeconds (0.3f);

		// cleaer label
		GamePlayService.MoveToAnimation (clearObj.gameObject, clearObj.localPosition,new Vector3(100,titlePosY +30 ,0), 0.2f);
		yield return new WaitForSeconds (0.2f);

		// title
		title.color = new Color (135f / 255, 135f / 255, 135f / 255, 1f);
		title.fontSize = 60;
		GamePlayService.MoveToAnimation (title.gameObject, title.transform.localPosition, title.transform.localPosition +
		                                 new Vector3 (-100,0,0), 0.2f);
		yield return new WaitForSeconds (0.2f);


		// footer text
		footerText.localPosition = new Vector3 (1000, -titlePosY, 0);
		GamePlayService.MoveToAnimation (footerText.gameObject,footerText.localPosition ,
		                                 new Vector3 (0, -titlePosY, 0), 0.2f);
		yield return new WaitForSeconds (0.2f);

		// share
		share.localPosition = new Vector3 (-1000, - titlePosY - 60,0);
		GamePlayService.MoveToAnimation (share.gameObject,share.localPosition , 
		                                 new Vector3 (-150, -titlePosY - 60, 0), 0.2f);

		next.localPosition = new Vector3 (1000, - titlePosY - 60,0);
		GamePlayService.MoveToAnimation (next.gameObject,next.localPosition , 
		                                 new Vector3 (150, -titlePosY - 60, 0), 0.2f);
		yield return new WaitForSeconds (0.2f);

		// board block scale
		StartCoroutine (_currentBoard.StartScaleAnim ());
	}

	public void OnShareClick()
	{

	}

	public void OnNextClick()
	{
		if (_isProcessing)
						return;
		_isProcessing = true;
		StartCoroutine (NextLevel());
	}


}
