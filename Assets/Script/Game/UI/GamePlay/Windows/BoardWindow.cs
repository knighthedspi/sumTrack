using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardWindow : WindowItemBase {
	private Dictionary<int, Board> dicBoard ;
	private Board _currentBoard;
	private int _nextLevel;

	public GameObject boardPrefab;


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
			GamePlayService.MoveToAnimation(_currentBoard.gameObject,_currentBoard.transform.localPosition,_currentBoard.transform.localPosition + new Vector3(-1000,0,0),0.5f);
		}
		else
		{
			_currentBoard = board;
			StartCoroutine(_currentBoard.StartGameAnim());
		}

	}

	public void OnLevelLoaded()
	{
		ShowHeaderFooter ();
		_currentBoard.gameObject.SetActive (false);
		AppManager.Instance.playingLevel = _nextLevel;
		_currentBoard = dicBoard [_nextLevel];
		StartCoroutine (_currentBoard.StartGameAnim ());
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

	public void OnGameFinish()
	{
		HideHeaderFooter ();
		StartCoroutine (NextLevel());
	}

	IEnumerator NextLevel()
	{
		yield return new WaitForSeconds (2f);
		LoadLevel (AppManager.Instance.playingLevel + 1);
	}


}
