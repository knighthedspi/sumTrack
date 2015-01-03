using UnityEngine;
using System.Collections;

public enum BlockType
{
	normalDone = 0,
	normal ,
	normalTwice,
	normalTri,

	origin,
	originDone,
	start,
}


public class BlockInfo  {
	private BlockType _type;
	public int num;
	public Vector2 posInBoard;

	public BlockType type
	{
		set
		{
			_type = value;
			if (_type == BlockType.normalDone || _type == BlockType.start || _type == BlockType.originDone)
				this.num = 0;
		}
		get
		{
			return _type;
		}
	}
	
	public BlockInfo()
	{
	}

	public BlockInfo (BlockInfo info)
	{
		this.num 		= info.num;
		this.type 		= info.type;
		this.posInBoard = info.posInBoard;

	}
}
