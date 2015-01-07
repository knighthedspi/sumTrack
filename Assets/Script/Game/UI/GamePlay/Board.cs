using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : WindowItemBase {


	private List<Block> blocks;
	public Block blockPrefab;
	public Block currentSelected { set; get; }

	public override void PreLoad ()
	{
		if(blocks.Count <= 0)
			Init (GamePlayService.CreateBlockList ());
		base.PreLoad ();
	}

	protected override void Awake ()
	{
		base.Awake ();
		blocks = new List<Block> ();
		GamePlayService.loadAllMap();
	}
//	void Awake()
//	{
//		 blocks = new List<Block> ();
//
//	}

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

	public void Init(List<BlockInfo> blockInfoes)
	{
		blocks.Clear ();
		blockInfoes = GamePlayService.AddStartToOrigin (blockInfoes);

		foreach(BlockInfo info in blockInfoes)
		{
			Block block = blockPrefab.Spawn();
			block.transform.parent = transform;
			block.transform.localScale = new Vector3(1,1,1);
			block.blockInfo = info;
			blocks.Add(block);
		}
		
	}

	public void OnOptionBtnClick()
	{
		WindowManager.Instance.ChangeWindow (WindowName.Option, TransitionType.TopToBottom);
	}



}
