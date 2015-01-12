using UnityEngine;
using System;
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

	public static Vector3 ConvertToPosition (Vector2 pos)
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
	
	public static Dictionary<int,List<BlockInfo>> mapData;

	public static void loadAllMap()
	{
		Map data = Resources.Load("Map/map") as Map;
		mapData = new Dictionary<int, List<BlockInfo>>(data.dataArray.Length);
		for(int i = 0; i < data.dataArray.Length; i++)
		{
			List<BlockInfo> mapItem = new List<BlockInfo>();
			string [] lines = data.dataArray[i].Map.Split("\n"[0]);
			for(int j = 0; j < lines.Length; j++)
			{
				if(lines[j].Trim().Equals(""))
					break;
				string [] blockInfo = lines[j].Split(" "[0]);
				for(int k = 0; k < blockInfo.Length; k ++)
				{
					if(blockInfo[k].Trim().Equals(""))
						break;
					string [] info = blockInfo[k].Split(";"[0]);
					int num = 0;
					Int32.TryParse(info[0], out num);
					if(num!=0)
					{
						int type = 0;
						Int32.TryParse(info[1], out type);
						BlockInfo block = new BlockInfo();
						block.posInBoard = new Vector2(k, j);
						block.num = num;
						switch(type)
						{
							case 0:
								block.type = BlockType.origin;
								break;
							case 1:
								block.type = BlockType.normal;
								break;
							case 2:
								block.type = BlockType.normalTwice;
								break;
							case 3:
								block.type = BlockType.normalTri;
								break;
							default:
								break;
						}
						mapItem.Add(block);
					}
				}
			}
			mapData.Add(data.dataArray[i].Level, mapItem);
		}
	}

	public static List<BlockInfo> CreateBlockList(int level)
	{
		List<BlockInfo> infos = new List<BlockInfo> ();
		List<BlockInfo> data = mapData [level];

		for(int i = 0; i < mapData[level].Count; i ++)
		{
			BlockInfo blockInfo = new BlockInfo();
			blockInfo.posInBoard = mapData[level][i].posInBoard ;
			blockInfo.num = mapData[level][i].num;
			blockInfo.type = mapData[level][i].type;
			infos.Add(blockInfo);
		}
		return infos;
	}

	/// <summary>
	/// Moves to animatin using iTween
	/// </summary>


	public static void MoveToAnimation(GameObject go, Vector3 originPos, Vector3 targetPos,float time)
	{
		go.transform.localPosition = originPos;
		iTween.MoveTo(go,iTween.Hash("position",targetPos,"isLocal",true,"time",time,"easetype","linear"));
	}


	/// <summary>
	/// Moves to animatin using iTween
	/// </summary>

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

	/// <summary>
	/// Scales to animation using iTween
	/// </summary>

	public static void ScaleTo(GameObject go,Vector3 originScale, Vector3 scale,float time,string easeType, string callback , GameObject completeTarget)
	{
		go.transform.localScale = originScale;
		iTween.ScaleTo (go,iTween.Hash("scale",scale,"isLocal",true,"time",time,"easetype",easeType,"oncomplete",callback,"oncompletetarget",completeTarget));
	}

	/// <summary>
	/// Scales to animation using iTween
	/// </summary>

	public static void ScaleTo(GameObject go,Vector3 originScale, Vector3 scale, float time,string easeType)
	{
		go.transform.localScale = originScale;
		iTween.ScaleTo (go,iTween.Hash("scale",scale,"isLocal",true,"time",time,"easetype",easeType));
	}


}
 