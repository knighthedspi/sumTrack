using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayService  {

	public static bool CheckNeibor(Vector2 pos1, Vector2 pos2)
	{
//		Debug.Log (string.Format ("move pos1 x: {0}  y: {1}  pos x:{2}  y{3}", (int)pos1.x, (int)pos1.y, (int)pos2.x, (int)pos2.y));
		if (pos1.x != pos2.x && pos1.y != pos2.y)
						return false;
		else if(pos1.x == pos2.x)
		{
			return (pos1.y >= pos2.y -1 && pos1.y <= pos2.y +1);
			
		}
		else
		{
			return (pos1.x >= pos2.x-1 && pos1.x <= pos2.x +1);
		}
			
	}

	public static Vector2 ConvertToPosition (Vector2 pos)
	{
		GamePlayInfo setup = AppManager.Instance.gamePlayInfo;
		float x = pos.x * setup.blockSize.x - setup.boardSize.x ;
		float y = - pos.y * setup.blockSize.y + setup.boardSize.y ;
		return new Vector3 (x, y, 0);
	}

	/// <summary>
	/// add block start to each origin block
	/// </summary>
	public static List<BlockInfo>  AddStartToOrigin(List<BlockInfo> blockInfo )
	{
		List<BlockInfo> origins = blockInfo.FindAll (x => x.type == BlockType.origin);
		foreach(BlockInfo bi in origins)
		{
			BlockInfo start = new BlockInfo(bi);
			start.type = BlockType.start;
			blockInfo.Add(start);
		}
		return blockInfo;
	}

	public static void MoveToAnimation(GameObject go, Vector3 originPos, Vector3 targetPos,float time)
	{
		go.transform.localPosition = originPos;
		iTween.MoveTo(go,iTween.Hash("position",targetPos,"isLocal",true,"time",time,"easetype","linear"));
	}

	public static void MoveToAnimation(GameObject go,Vector3 originPos, Vector3 targetPos,float time,string callback,GameObject completeTarget)
	{
		if(string.IsNullOrEmpty(callback))
		{
			MoveToAnimation(go,originPos,targetPos,time);
		}
		else
		{
			go.transform.localPosition = originPos;
			iTween.MoveTo(go,iTween.Hash("position",targetPos,"isLocal",true,"time",time,"easetype","linear","oncomplete",callback,"oncompletetarget",completeTarget));
		}

	}

	public static List<BlockInfo> CreateBlockList()
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
 