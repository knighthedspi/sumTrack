using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardLine : MonoBehaviour {

	private  List<Vector3> _linePos;
	public DrawLine linePrefab;
	private int _drawNum = 0;
	public  float time = 1;

	private EventDelegate.Callback OnComplete;

	void Awake()
	{
		_linePos = new List<Vector3> ();
	}

	public void Init(List<Vector3> lines)
	{
		_drawNum = 0;
		_linePos = lines;
		Draw ();
	}

	private void Draw()
	{

		if (_drawNum >= _linePos.Count )
		{
			if(OnComplete != null)
				OnComplete();
			return;
		}
			
		int next = (_drawNum == _linePos.Count - 1) ? 0 : _drawNum + 1;
		Vector3 source 		= _linePos [_drawNum];
		Vector3 des 		= _linePos [next];
		_drawNum ++;
		GameObject go = NGUITools.AddChild (gameObject, linePrefab.gameObject) as GameObject;
		DrawLine dl = go.GetComponent<DrawLine>();
		go.transform.localScale = new Vector3 (0.1f, 0.1f, 1);
		dl.OnDrawComplete = Draw;
		dl.StartDraw (source, des, time);
			
	}
}
