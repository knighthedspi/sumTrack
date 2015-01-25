using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionItem : MonoBehaviour {

	public string optionName	{ set; get; }
	public GameObject blockPrefabs;
	public int level { set; get;}
	public List<LevelBlock> blocks ;

	private int levelCount = 17;


	public void Init(int num)
	{
		blocks = new List<LevelBlock> ();
		int minLevel = num * levelCount + 1;
		level = minLevel;
		for(int i = 0; i < levelCount; i ++)
		{
			GameObject go = NGUITools.AddChild(gameObject,blockPrefabs);
			LevelBlock lb = go.GetComponent<LevelBlock>();
			lb.level = minLevel + i;
			int numPosX = (i+1) % 3;
			numPosX = (numPosX > 1) ?  -1 : numPosX;

			int numPosY = (i - 1)/3 + 1;
			int sign = (numPosY % 2 == 0) ? 1 : -1;
			if(i != 0)
				lb.transform.localPosition = new Vector3(sign * numPosX * 159, 420 - numPosY * 137,0);
			else 
				lb.transform.localPosition = new Vector3(numPosX * 159, 420);
			blocks.Add(lb);
		}
	}

}
