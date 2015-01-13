using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {


	private List<Block> blocks;
	public Block blockPrefab;
	public Block currentSelected { set; get; }

	private int _destroyCount = 0;
	private int _currentLevel ;

	private List<Block> _historyBlocks;
	private List<BlockInfo> _historyBlockInfos;

	void Awake()
	{
		blocks = new List<Block> ();
		GamePlayService.loadAllMap();
		_historyBlocks = new List<Block>();
		_historyBlockInfos = new List<BlockInfo>();
	}

	void Update()
	{
		if(Input.GetMouseButton(0))
		{
			if(UICamera.hoveredObject != null )
			{
				GameObject hoverObj = UICamera.hoveredObject;
				// check with current select
				if(currentSelected != null && currentSelected.gameObject == hoverObj)
					return;
				
				// check is existed or not
				Block hoverBlock = blocks.Find(x => x.gameObject == hoverObj);
				if(hoverBlock == null)
					return;
				if(_historyBlocks.Find(x => x == hoverBlock) == null)
				{
					Debug.Log("Add block " + hoverBlock.blockInfo.num + " at pos " + hoverBlock.blockInfo.posInBoard);
					_historyBlocks.Add(hoverBlock);
					_historyBlockInfos.Add(hoverBlock.blockInfo);
				}

				if(hoverBlock.blockInfo.type == BlockType.origin)
				{
					currentSelected = hoverBlock;
					hoverBlock.OnSelected();
				}
				else 
				{
					if(currentSelected != null)
						currentSelected.MoveTo(hoverBlock);
				}
			}
		}

		else 
		{
			currentSelected = null;
		}
	}

	public void Init(int level)
	{
		_currentLevel = level;
		List<BlockInfo> blockInfoes = GamePlayService.CreateBlockList (level);
		blocks.Clear ();
		blockInfoes = GamePlayService.AddStartToOrigin (blockInfoes);

		foreach(BlockInfo info in blockInfoes)
		{
			Block block = blockPrefab.Spawn();
			block.transform.parent = transform;
			block.transform.localScale = Vector3.zero;
			block.blockInfo = info;
			blocks.Add(block);
		}
		
		StartCoroutine (StartGameAnim ());
	}

	public IEnumerator StartGameAnim()
	{
		List<Block> originBlock = blocks.FindAll (x => x.blockInfo.type == BlockType.origin);
		List<Block> normalBlock = blocks.FindAll (x => x.blockInfo.type != BlockType.origin);
		foreach(Block origin in originBlock)
		{
			GamePlayService.ScaleTo(origin.gameObject,Vector3.zero,Vector3.one,1.5f,Config.EASE_SCALE_OUT);
		}

		yield return new WaitForSeconds (0.2f);

		foreach(Block normal in normalBlock)
		{
			GamePlayService.ScaleTo(normal.gameObject,Vector3.zero,Vector3.one,1.5f,Config.EASE_SCALE_OUT);
		}
	}

	public void ResetGameAnim()
	{
		foreach(Block bl in blocks)
		{
			GamePlayService.ScaleTo(bl.gameObject,Vector3.one,Vector3.zero,1f,Config.EASE_SCALE_IN,"OnBlockDestroy",this.gameObject);
		}
	}

	private void OnBlockDestroy()
	{
		_destroyCount ++;
		if(_destroyCount >= blocks.Count)
		{
			_destroyCount = 0;
			foreach(Block bl in blocks)
			{
				bl.Destroy();
			}
			blocks.Clear();
			Init (_currentLevel);
		}

	}

	public void OnUndoAction()
	{
		if(_historyBlocks.Count < 1)
			return;
		Block currentBlock = _historyBlocks[_historyBlocks.Count - 1];
		BlockInfo currentBlockInfo = _historyBlockInfos[_historyBlockInfos.Count - 1];
		Debug.Log("Restore block " + currentBlock.blockInfo.posInBoard + ";" + currentBlockInfo.num + " with type is " + currentBlockInfo.type.ToString() );
		Block matchBlock = blocks.Find(x => x.transform.localPosition == currentBlock.transform.localPosition);
		if(matchBlock!=null)
			matchBlock.blockInfo = currentBlockInfo;
		currentBlock.blockInfo = currentBlockInfo;
		Debug.Log("Restore block 2 " + currentBlock.blockInfo.posInBoard + ";" + currentBlockInfo.num + " with type is " + currentBlock.blockInfo.type.ToString() );
		_historyBlocks.Remove(currentBlock);
		_historyBlockInfos.Remove(currentBlockInfo);
	}

	

}
