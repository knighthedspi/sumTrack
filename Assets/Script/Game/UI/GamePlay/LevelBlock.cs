using UnityEngine;
using System.Collections;

public class LevelBlock : MonoBehaviour {
	private bool _isChecked;
	private int _level;

	public UILabel levelText;
	public UISprite background;
	public bool isChecked
	{
		set
		{
			_isChecked = value;
			background.spriteName = (_isChecked) ? "level_checked"  : "level";
		}
		get
		{
			return _isChecked;
		}
	}

	public int level { 
		set
		{
			_level = value;
			if(_level > AppManager.Instance.maxLevel)
			{
				levelText.text = "";
				background.spriteName = "";
			}
			else
			{
				levelText.text = _level.ToString();
				isChecked = (_level < AppManager.Instance.playingMaxLevel);
			}

		}
		get
		{
			return _level;
		}
	}

	public void OnClick()
	{
		Debug.Log("On click");
		if (_level > AppManager.Instance.playingMaxLevel)
						return;

		AppManager.Instance.playingLevel = _level;
		WindowManager.Instance.ChangeWindow (WindowName.BoardWindow,TransitionType.TopToBottom);
	}

}
