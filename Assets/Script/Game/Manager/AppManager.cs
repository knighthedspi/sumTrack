using UnityEngine;
using System.Collections;

public class AppManager : Singleton<AppManager>
{
	public GamePlayInfo gamePlayInfo;
	public int playingLevel { set; get;}

	void Start()
	{
		gamePlayInfo = new GamePlayInfo ();
		gamePlayInfo.blockSize = new Vector2 (100, 100);
		gamePlayInfo.blockNum = new Vector2 (6, 7);
		gamePlayInfo.boardSize = new Vector2 (gamePlayInfo.blockNum.x * gamePlayInfo.blockSize.y / 2,
		                                     gamePlayInfo.blockNum.y * gamePlayInfo.blockSize.y / 2);
		playingLevel = PlayerPrefs.GetInt(Config.CURRENT_LEVEL) == 0 ? 1 : PlayerPrefs.GetInt(Config.CURRENT_LEVEL);

	}


}
