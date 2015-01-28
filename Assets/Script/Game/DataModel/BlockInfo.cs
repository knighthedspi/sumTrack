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
	error,

	level,
	levelDone,
}


public class BlockInfo  {
	private BlockType _type;
	public int num;
	public Vector2 posInBoard;
	public int id { set; get; }
	public Vector2 sizeImage { set; get;}

	public BlockType type
	{
		set
		{
			_type = value;
			SetupByType();
		
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
		this.id 		= info.id;
	}

	public void SetupByType()
	{
		switch(_type)
		{
		case BlockType.normalDone:
			this.num = 0;
			sizeImage = new Vector2(40,40);
			break;

		case BlockType.start:
			this.num = 0;
			sizeImage = new Vector2(50,50);
			break;

		case BlockType.originDone:
			this.num = 0;
			sizeImage = new Vector2(100,100);
			break;

		case BlockType.origin:
			sizeImage = new Vector2(100,100);
			break;

		default:
			sizeImage = new Vector2(73,73);
			break;

		}
	}

	public bool ComparePos(BlockInfo info)
	{
		return (this.posInBoard.x == info.posInBoard.x && this.posInBoard.y == info.posInBoard.y);
	}


}
