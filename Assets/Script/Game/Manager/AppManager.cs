using UnityEngine;
using System.Collections;

public class AppManager : Singleton<AppManager>
{
	public GamePlayInfo gamePlayInfo;
	public int playingLevel { set; get;}
	public int maxLevel { set; get; }

	void Start()
	{
		gamePlayInfo = new GamePlayInfo ();
		gamePlayInfo.blockSize = new Vector2 (100, 100);
		gamePlayInfo.blockNum = new Vector2 (6, 7);
		gamePlayInfo.boardSize = new Vector2 (gamePlayInfo.blockNum.x * gamePlayInfo.blockSize.y / 2,
		                                     gamePlayInfo.blockNum.y * gamePlayInfo.blockSize.y / 2);
		playingLevel = 2;
		maxLevel = 45;


	}


}
