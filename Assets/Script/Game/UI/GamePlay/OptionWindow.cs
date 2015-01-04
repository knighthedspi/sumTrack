using UnityEngine;
using System.Collections;

public class OptionWindow : WindowItemBase {

	public void OnHomeBtnClick()
	{
		WindowManager.Instance.ChangeWindow (WindowName.Board,TransitionType.BottomToTop);
	}
}
