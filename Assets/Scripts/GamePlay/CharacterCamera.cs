using System;
using GamePlay;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterCamera : MonoBehaviour
{
	public Transform target;
	public float Distance =5f;
	public float Height = 3f;
	public float Smooth = 6f;
	public float ShakeDuration;
	public float ShakeAmplitude;

	private static CharacterCamera instance;
	private IControllerForCamera characterController;
	private Transform _transform;
	private float lastOnGroundHeight;
	private float shakeIntenivity;
	private Quaternion offsetRotate;

	public static CharacterCamera Instance
	{
		get
		{
			return instance ?? (instance = FindObjectOfType(typeof(CharacterCamera)) as CharacterCamera);
		}
	}
	public void Start()
	{
		_transform = transform;
		if (LevelConfiguration.Instance.IsMotocycleLevel)
			characterController = MotorController.GetInstance();
		else
			characterController = Controller.GetInstance();
		if (!target && characterController != null)
		{
			target = characterController.GetTransform();
			//смещение
			//offsetRotate = Quaternion.AngleAxis(-20, Vector3.right);
		}

	}

	void OnDisable()
	{
		instance = null;
	}
   
	public void LateUpdate()
	{
		if(!target) return;

		Vector3 newPos = Vector3.zero;
		//Quaternion newRoate = Quaternion.identity;

		newPos.x = target.position.x;

		if(characterController.IsOnGround())
		{
			newPos.y = Height + target.position.y ;
			lastOnGroundHeight = target.position.y;
			//newRoate = Quaternion.identity;
		}
		else
		{
			newPos.y = Height + lastOnGroundHeight;
			//Vector3 direction = Vector3.Normalize(target.transform.position - _transform.position);
			//Debug.Log(direction.y);
			//if (direction.y < -0.4f)
			//	newRoate = Quaternion.LookRotation(target.transform.position - _transform.position, Vector3.up) * offsetRotate;
			//else
			//	newRoate = Quaternion.identity;
		}
		newPos.z = target.position.z - Distance;

		if (shakeIntenivity > 0)
		{
			newPos += Random.insideUnitSphere*shakeIntenivity;
			shakeIntenivity -= Time.deltaTime;
		}

		//лерпить из старого положения в новое
		_transform.position = Vector3.Lerp(_transform.position, newPos, Smooth*Time.deltaTime);
		//transform.rotation = Quaternion.Slerp(_transform.rotation, newRoate, Time.deltaTime * 4.0f);  


	}

	public void Shake()
	{
		shakeIntenivity = ShakeAmplitude;
	}
}
