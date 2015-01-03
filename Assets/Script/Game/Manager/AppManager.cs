using UnityEngine;
using System.Collections;

public class AppManager : Singleton<AppManager>
{
	public GamePlayInfo gamePlayInfo;

	void Start()
	{
		gamePlayInfo = new GamePlayInfo ();
		gamePlayInfo.blockSize = new Vector2 (100, 100);
		gamePlayInfo.blockNum = new Vector2 (5, 7);
		gamePlayInfo.boardSize = new Vector2 (gamePlayInfo.blockNum.x * gamePlayInfo.blockSize.y / 2,
		                                     gamePlayInfo.blockNum.y * gamePlayInfo.blockSize.y / 2);

	}


}
