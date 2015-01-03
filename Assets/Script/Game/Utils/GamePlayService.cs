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
}
 