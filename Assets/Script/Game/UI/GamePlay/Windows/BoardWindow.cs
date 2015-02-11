using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Profile;
using Soomla;



public class BoardWindow : WindowItemBase {
	private Dictionary<int, Board> dicBoard ;
	private Board _currentBoard;
	private int _nextLevel;

	public GameObject SoundSettingBtn;
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
	private string SE_Button = "SE_Back";

	private string SHARE = "I completed level {0} in Plus: puzzle. Check it out! #plusgame http://plus-game.tk";

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


	void Update()
	{
		if (undoBtn != null) 
						undoBtn.GetComponent<UIButton> ().enabled = (GamePlay.Instance.history.Count != 0);
	}

	public void LoadLevel(int level)
	{
		if (_currentBoard != null && dicBoard.ContainsKey(level) && _currentBoard == dicBoard[level])
						return;
		_nextLevel = level;
		Board board = dicBoard.ContainsKey (level) ? dicBoard [level] : CreateNewBoard (level);

		title.text = string.Format ("Level " + level.ToString ());
		if(_currentBoard != null )
		{
			Vector3 oldPos = board.transform.localPosition;
			GamePlayService.MoveToAnimation(board.gameObject,oldPos + new Vector3(1000,0,0),oldPos,0.5f,"OnLevelLoaded",this.gameObject);

			//if new board is not current board
			if(_currentBoard != board)
			{
				GamePlayService.MoveToAnimation(_currentBoard.gameObject,_currentBoard.transform.localPosition,
				                                _currentBoard.transform.localPosition + new Vector3(-1000,0,0),0.5f);
			}

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
		ChangeSpriteSoundBtn();
		if (GoogleAnalytics.instance)
			GoogleAnalytics.instance.LogScreen("BoardWindow: level " + AppManager.Instance.playingLevel);
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
		if(SoundManager.Instance.getIsSound())
			SoundManager.Instance.PlaySE(SE_Button);
		WindowManager.Instance.ChangeWindow (WindowName.OptionWindow, TransitionType.BottomToTop);
	}

	public void OnResetBtnClick()
	{
		if(SoundManager.Instance.getIsSound())
			SoundManager.Instance.PlaySE(SE_Button);
		dicBoard [AppManager.Instance.playingLevel].ResetGameAnim ();
	}

	public void OnUndoBtnClick()
	{
		if(SoundManager.Instance.getIsSound())
			SoundManager.Instance.PlaySE(SE_Button);
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

	public void shareCallBack()
	{
		Debug.Log("Thank you for sharing");
	}

	public void OnShareClick()
	{
		if(SoundManager.Instance.getIsSound())
			SoundManager.Instance.PlaySE(SE_Button);
		SoomlaProfile.Login(Provider.FACEBOOK);
		if(SoomlaProfile.IsLoggedIn(Provider.FACEBOOK))
			SoomlaProfile.UpdateStatus(Provider.FACEBOOK, string.Format(SHARE, AppManager.Instance.playingLevel));
	}

	public void OnNextClick()
	{
		if(SoundManager.Instance.getIsSound())
			SoundManager.Instance.PlaySE(SE_Button);
		if (_isProcessing)
						return;
		_isProcessing = true;
		StartCoroutine (NextLevel());
	}

	public void OnHintClick()
	{
		Debug.Log("hint click");
		DialogHint.Create();
	}

	public void OnSoundClick()
	{
		if(SoundManager.Instance.getIsSound())
			SoundManager.Instance.PlaySE(SE_Button);
		SoundManager.Instance.setIsSound(!SoundManager.Instance.getIsSound());
		ChangeSpriteSoundBtn();
	}

	public void ChangeSpriteSoundBtn()
	{
		UIButton btn = SoundSettingBtn.GetComponent<UIButton>();
		if (SoundManager.Instance.getIsSound())
		{
			btn.normalSprite = "volume";
		}
		else
		{
			btn.normalSprite = "mute";
		}
	}
}
