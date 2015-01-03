using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlay : MonoBehaviour {

	public static GamePlay Instance;
	public Board board;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		Debug.Log("start------------------");
		board.Init (CreateBlockList ());
//		CreateBlockList ();
	}

	List<BlockInfo> CreateBlockList()
	{
		List<BlockInfo> infos = new List<BlockInfo> ();
		for(int i = 0; i< 9; i++)
		{
			BlockInfo Inf = new BlockInfo();
			Inf.num = i;
			Inf.type = BlockType.normal;
			infos.Add(Inf);
		}
		infos [0].posInBoard = new Vector2 (2, 2);
		infos [0].num = 11;
		infos [0].type = BlockType.origin;

		infos [1].posInBoard = new Vector2 (1, 3);
		infos [1].num = 1;

		infos [2].posInBoard = new Vector2 (2, 3);
		infos [2].num = 5;
		infos [2].type = BlockType.normalTwice;

		infos [3].posInBoard = new Vector2 (0, 4);
		infos [3].num = 10;
		infos [3].type = BlockType.origin;

		infos [4].posInBoard = new Vector2 (1, 4);
		infos [4].num = 3;

		infos [5].posInBoard = new Vector2 (2, 4);
		infos [5].num = 4;
		infos [5].type = BlockType.normalTri;

		infos [6].posInBoard = new Vector2 (3, 4);
		infos [6].num = 2;
		infos [6].type = BlockType.normalTwice;

		infos [7].posInBoard = new Vector2 (2, 5);
		infos [7].num = 3;

		infos [8].posInBoard = new Vector2 (3, 5);
		infos [8].num = 12;
		infos [8].type = BlockType.origin;


		return infos;
	}


}
