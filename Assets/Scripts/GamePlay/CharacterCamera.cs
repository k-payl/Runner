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
		

		newPos.x = target.position.x;

		if(characterController.IsOnGround())
		{
			newPos.y = Height + target.position.y ;
			lastOnGroundHeight = target.position.y;
			
		}
		else
		{
			newPos.y = Height + lastOnGroundHeight;
			
			
			
			
			
			
		}
		newPos.z = target.position.z - Distance;

		if (shakeIntenivity > 0)
		{
			newPos += Random.insideUnitSphere*shakeIntenivity;
			shakeIntenivity -= Time.deltaTime;
		}

		
		_transform.position = Vector3.Lerp(_transform.position, newPos, Smooth*Time.deltaTime);
		


	}

	public void Shake()
	{
		shakeIntenivity = ShakeAmplitude;
	}
}
