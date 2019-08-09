using System;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// Скрипт для вращения объекта
/// </summary>
public class Rotator : MonoBehaviour
{
    public int axis;
    public float period;
    public int direction;// -1 -право.  1 - лево
    public bool randomize;
    private bool activated;
    private Transform _transform;
    private Vector3 rotateVector;
    private float timeCorrection;


    [ContextMenu("Activate")]
    public void Activate()
    {
        if (!activated)
        {

            //если надо, то рандомим начальное положение
            if (randomize)
            {
                switch (axis)
                {
                    case 0:rotateVector = new Vector3(Random.Range(0f, 360f), 0f, 0f);break;
                    case 1:rotateVector = new Vector3(0f, Random.Range(0f, 360f), 0f);break;
                    case 2:rotateVector = new Vector3(0f, 0f, Random.Range(0f, 360f));break;
                }
                transform.Rotate(rotateVector, Space.World);
            }

            rotateVector = Vector3.zero;
            activated = true;
        }
    }

    [ContextMenu("Deactivate")]
    public void Deactivate()
    {
        activated = false;
    }

	void Start()
	{
       // if (gameObject.name == "FCMF_ventilator") Debug.Log("axis=" + axis + "; period=" + period + "; direction=" + direction);
	    _transform = transform;
        timeCorrection = 380f;
        Activate();
	}

	void Update () 
	{
	    try
	    {
	        if (activated)
	        {
	            if (period <float.Epsilon)
	            {
	                period = 2;
                    //Debug.Log("set period=2");
	            }
	            switch (axis)
	            {
	                case 0:
	                    rotateVector = Vector3.right*Time.deltaTime*direction*timeCorrection/period;
	                    break;
	                case 1:
	                    rotateVector = Vector3.up*Time.deltaTime*direction*timeCorrection/period;
	                    break;
	                case 2:
	                    rotateVector = Vector3.forward*Time.deltaTime*direction*timeCorrection/period;
	                    break;
	            }
                //if (gameObject.name == "FCMF_ventilator")
                //{
                //    Debug.Log("axis=" + axis + "; period=" + period + "; direction=" + direction + "; timeCorrection=" + timeCorrection + "rotateVector=" + rotateVector);
                //}
                //if (gameObject.name == "FCMF_ventilator") Debug.Log("rotateVector="+rotateVector);
                //if (rotateVector.x != float.NaN && rotateVector.y != float.NaN && rotateVector.z != float.NaN)
	                _transform.Rotate(rotateVector, Space.World);
                
	        }
	    }
	    catch (DivideByZeroException)
	    {
	        Debug.Log("Rotator.Update(): Division by zero. set period correctly");
	    }
	}


#if UNITY_EDITOR

    /// <summary>
    /// Интерфейс для RoatatorEditor
    /// </summary>
    public int Direction
    {
        get { if (direction == -1) return 0; else return 1; }
        set { if (value == 0) direction = -1; else direction = 1; }
    }
    public string[] strDirs = new[] { "X", "Y", "Z" };
    public string[] strSides = new[] { "Right", "Left" };

#endif
}
