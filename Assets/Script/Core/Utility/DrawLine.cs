using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {
	public  Vector3 _source;
	public  Vector3 _destination;
	private UIWidget _uiWidget;
	private float _speed;
	public float _time;
	private bool _isDrawFinish;
//	private int _fistHeight;
	private float _count;
	public EventDelegate.Callback OnDrawComplete;

	void Awake()
	{
		_uiWidget = GetComponent<UIWidget> ();
	}

	void Start()
	{

	}

	public void StartDraw(Vector3 source,Vector3 des, float time)
	{
		_source = source;
		_destination = des;
		_time = time;
		_isDrawFinish = false;
//		_fistHeight = _uiWidget.height;

		// angular
		Vector3 direction = _destination - _source;
		Vector3 from = Vector3.up;
		float angle = Mathf.DeltaAngle(Mathf.Atan2(from.y, from.x) * Mathf.Rad2Deg,
		                               Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

		Debug.Log ("angle " + angle.ToString ());
		transform.localEulerAngles = new Vector3 (0, 0, angle);
		transform.localPosition = source;
		_speed = Vector3.Distance (_source, _destination) * 1f / time;
		_count = 0;
		_uiWidget.depth = 4;
	}

	void Update()
	{
		if (_isDrawFinish)
						return;
		_count += Time.deltaTime;

		if(_count >= _time)
		{
			_isDrawFinish = true;
			if(OnDrawComplete != null)
				OnDrawComplete();
			_count = _time;
		}
		_uiWidget.height = (int)(_speed * _count/transform.localScale.y);


	}



}
