using UnityEngine;
using System;
using Random = UnityEngine.Random;




public class Floater : MonoBehaviour {

	public float halfAmplitude; 
	public float period;
	public bool randomInitPosition;

	#region private vars

	protected bool activated; 
	private Transform _transform;
	private float calculatedConst;
	private float lastTime;
	private Vector3 savedPosition;
	#endregion

	void Start()
	{
		_transform = transform;
		Activate();
	}

	void Update ()
	{
		if (activated)
		{
			float y1 = Mathf.Sin(lastTime);
			float y2 = Mathf.Sin(lastTime + Time.deltaTime*calculatedConst);
			lastTime += Time.deltaTime*calculatedConst;
			_transform.Translate(Vector3.up*(y2 - y1)*halfAmplitude, Space.World);
		}
	}

	[ContextMenu("Activate floating")]
	public void Activate()
	{
		if (!activated)
		{
			
			savedPosition = transform.position;

			
			if (period > 0.01)
				calculatedConst = Mathf.PI*2f/period;
			else
			{
				activated = false;
				Debug.Log("Floater: Set period > " + 0.01f + " in "+name);
				return;
			}

			
			if (randomInitPosition)
			{
				float randomizeOffset = Random.Range(0, 10f);
				float v = Mathf.Sin(-randomizeOffset);
				transform.Translate(Vector3.up * v * halfAmplitude, Space.World);
				lastTime = -randomizeOffset;
			}
			else
				lastTime = 0;

			activated = true;
		}
	}

	[ContextMenu("Deactivate floating")]
	public virtual void Deactivate()
	{
		if (activated)
		{
			activated = false;
		   
		}
	}

}
