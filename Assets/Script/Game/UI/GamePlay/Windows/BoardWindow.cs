using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardWindow : WindowItemBase {
	private Dictionary<int, Board> dicBoard ;
	public GameObject boardPrefab;
	public int currentLevel;

	protected override void Awake ()
	{
		base.Awake ();
		dicBoard = new Dictionary<int,Board > ();
	}

	public override void PreLoad ()
	{
		// TODO : fix level , should be loaded from appManager
//		int level = AppManager.Instance.playingLevel;
		int level = 1;
		Board board = dicBoard.ContainsKey (level) ? dicBoard [level] : CreateNewBoard (level);
		title.text = string.Format ("Level " + level.ToString ());
		base.PreLoad ();
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
			dicBoard[level] = board;
		}
		return board;
	}

	public void OnOptionBtnClick()
	{
		WindowManager.Instance.ChangeWindow (WindowName.OptionWindow, TransitionType.BottomToTop);
	}


}
