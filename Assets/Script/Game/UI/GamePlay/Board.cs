using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {


	private List<Block> blocks;
	public Block blockPrefab;
	public Block currentSelected { set; get; }

	private int _destroyCount = 0;
	private int _currentLevel ;
	private bool _isFinish = false;

	private int minBoundX = 0;
	private int minBoundY = 0;
	private int maxBoundX = 5;
	private int maxBoundY = 7;

	private BoardLine _boardLine;
	private GamePlayInfo _setup;
	public Vector2 boardSize;


	public delegate void OnGameFinish();
	public OnGameFinish finish;

	void Awake()
	{
		blocks = new List<Block> ();
		boardSize = new Vector2 ();
		_setup = AppManager.Instance.gamePlayInfo;
		_boardLine = GetComponent<BoardLine> ();
		GamePlayService.loadAllMap();
	}

	void Update()
	{
		if (_isFinish)
						return;
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

	public void Init(int level)
	{
		_currentLevel 	= level;
		_isFinish 		= false;
		List<BlockInfo> blockInfoes = GamePlayService.CreateBlockList (level);
		blocks.Clear ();

		foreach(BlockInfo bl in blockInfoes)
		{
			Debug.Log(bl.posInBoard);
		}


		blockInfoes = GamePlayService.AddStartToOrigin (blockInfoes);

		// calculate position
		transform.localPosition -= new Vector3 (-_setup.boardSize.x/2 + boardSize.x/2, _setup.boardSize.y/2 - boardSize.y/2);

		foreach(BlockInfo info in blockInfoes)
		{
			boardSize.x = (boardSize.x > info.posInBoard.x) ? boardSize.x : info.posInBoard.x;  
			boardSize.y = (boardSize.y > info.posInBoard.y) ? boardSize.y : info.posInBoard.y;

			// setuo block
			Block block = blockPrefab.Spawn();
			block.transform.parent = transform;
			block.transform.localScale = Vector3.zero;
			block.blockInfo = info;
			block.moveComplete = OnBlockMoveComplete;
			blocks.Add(block);

		}

		maxBoundY = (int)boardSize.y;
		maxBoundX = (int)boardSize.x;
		// calculate board size
		boardSize.x = (boardSize.x + 1) * _setup.blockSize.x;
		boardSize.y = (boardSize.y +1) * _setup.blockSize.y;
		Debug.Log (boardSize);


		
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
			StartCoroutine(StartGameAnim());
		}

	}

	private void OnBlockMoveComplete()
	{
		if (_isFinish)
						return;
		_isFinish = CheckWin ();
		if (_isFinish)
		{
			CreateRetangleAnim();
			finish ();
		}
						
	}

	private bool CheckWin ()
	{
		 List<Block> hasPointBlock = blocks.FindAll (x => (x.blockInfo.type == BlockType.origin || x.blockInfo.type == BlockType.normal
						|| x.blockInfo.type == BlockType.normalTri || x.blockInfo.type == BlockType.normalTwice));
		return (hasPointBlock.Count <= 0);
	}

	private void CreateRetangleAnim()
	{
		List<Vector3> lines = new List<Vector3> ();
		lines.Add (GamePlayService.ConvertToPosition (new Vector2 (minBoundX, minBoundY)) + new Vector3(-100,100,0));
		lines.Add (GamePlayService.ConvertToPosition (new Vector2 (maxBoundX, minBoundY)) + new Vector3(100,100,0));
		lines.Add (GamePlayService.ConvertToPosition (new Vector2 (maxBoundX, maxBoundY)) + new Vector3(100,-100,0));
		lines.Add (GamePlayService.ConvertToPosition (new Vector2 (minBoundX, maxBoundY)) + new Vector3(-100,-100,0));
		_boardLine.Init (lines);
	}




}
