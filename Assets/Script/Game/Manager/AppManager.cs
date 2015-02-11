using UnityEngine;
using System.Collections;


public class AppManager : Singleton<AppManager>
{
	public GamePlayInfo gamePlayInfo;
	public int playingLevel { set; get;}
	public int maxLevel { set; get; }
	public int playingMaxLevel { set; get;}


	void Start()
	{
		PlayerPrefs.SetInt (Config.CURRENT_MAX_LEVEL, 45);

		gamePlayInfo 			= new GamePlayInfo ();

		// block
		gamePlayInfo.blockSize 	= new Vector2 (100, 100);
		gamePlayInfo.blockNum 	= new Vector2 (6, 7);
		gamePlayInfo.boardSize 	= new Vector2 (gamePlayInfo.blockNum.x * gamePlayInfo.blockSize.y / 2,
		                                     gamePlayInfo.blockNum.y * gamePlayInfo.blockSize.y / 2);

		// level
		playingLevel 		= PlayerPrefs.GetInt(Config.CURRENT_LEVEL) == 0 ? 1 : PlayerPrefs.GetInt(Config.CURRENT_LEVEL);

		// playing max level
		int pMaxLevel 		= PlayerPrefs.GetInt (Config.CURRENT_MAX_LEVEL);
		playingMaxLevel 	= pMaxLevel == 0 ? playingLevel : pMaxLevel;

//		playingLevel = 1;
//		playingMaxLevel = 1;

		maxLevel = 45;
	}


}
