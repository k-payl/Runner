using System.Runtime.InteropServices;
using GamePlay;
using UnityEngine;
using System.Collections;

namespace GamePlay
{
	[RequireComponent(typeof (CharacterController))]
	public class MotorController : MonoBehaviour, IMotorControl, IControllerForCamera
	{

		public float drivingSpeed;
		public float JumpSpeed;
		public float Gravity;
		public float Acelerate;
		public float driveCrossfade;
		public float turnSpeed;
		public float turnCrossfade;
		public AnimationClip driveAnimation;
		public AnimationClip TurnLeftAnimation;
		public AnimationClip TurnRightAnimation;
		
		

		private static MotorController instance;

		public static MotorController GetInstance()
		{
			return instance ?? (instance = FindObjectOfType(typeof (MotorController)) as MotorController);
		}

		private Vector3 move;
		private float currentTurnValue;
		private float currentTurnValueU;
		private CharacterController controllerCollider;
		private float verticalSpeed;
		private TrackAbstract track;

		private void Start()
		{
			instance = this;
			controllerCollider = GetComponent<CharacterController>();
			track = TrackAbstract.GetInstance();
		}

		private void Update()
		{
			
			currentTurnValue = 0;
			if (currentTurnValueU > 0)
			{
				if (transform.position.x < track.MaxX())
				{
					currentTurnValue = currentTurnValueU*turnSpeed;
					animation.CrossFade(TurnRightAnimation.name, turnCrossfade);
				}
			}
			else if (currentTurnValueU < 0)
			{
				if (transform.position.x > track.MinX())
				{
					currentTurnValue = currentTurnValueU * turnSpeed;
					animation.CrossFade(TurnLeftAnimation.name, turnCrossfade);
				}

			}
			else animation.CrossFade(driveAnimation.name, driveCrossfade);
				


			
			if (IsOnGround() && verticalSpeed <= 0)
			{
				verticalSpeed = 0;
			}
			else
			{
				verticalSpeed -= Gravity*Time.deltaTime;
			}

			
			move = new Vector3(currentTurnValue, verticalSpeed, drivingSpeed);
			controllerCollider.Move(move*Time.deltaTime);
		}

		public void TurnSmoothly(float value)
		{
			currentTurnValueU = value;
		}

		public void Jump(JumpDrection jumpDrection)
		{
			verticalSpeed = JumpSpeed;
		}

		public void Accelerate(float value)
		{
			throw new System.NotImplementedException();
		}

		public bool IsOnGround()
		{
			return Physics.Raycast(
				new Vector3(controllerCollider.bounds.center.x, controllerCollider.bounds.min.y,
					controllerCollider.bounds.center.z),
				Vector3.down,
				0.3f);

		}

		public Transform GetTransform()
		{
			return transform;
		}
	}
}