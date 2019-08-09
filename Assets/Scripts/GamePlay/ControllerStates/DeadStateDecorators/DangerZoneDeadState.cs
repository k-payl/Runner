using UnityEngine;
using System.Collections;
using LevelGeneration;

namespace GamePlay
{
	class DangerZoneDeadState : AbstractDeadStateDecorator
	{
		public float delayTime = 0.4f;
		
		private float timeOfDeath;
		private float timer;
		private int direction;

		public DangerZoneDeadState(Controller controller) : base(controller, DeadReason.DangerZone)
		{
			direction = 1;
			timer = Time.time;
			timeOfDeath = 1f;
			horizontSpeed = controller.ControllerParams.RunSpeed;
		}

		public override Vector3 UpdatePosition()
		{
			return  new Vector3( CalcXSpeed(), CalcYSpeed()*direction, CalcZSpeed());	
		}

		protected override float CalcYSpeed()
		{
			float res;
			if (!IsGrounded())
			{
			   res = verticalSpeed -= controller.ControllerParams.Gravity * Time.deltaTime;
			}
			else
			{
				res = 0f; 
			}
			

			return res;
		}
		protected override float CalcZSpeed()
		{
			float res;
			if ( (Time.time - timer) >= delayTime )
			{
				
				PlayFinishAnimation();
				if ( (Time.time - timer - delayTime) < timeOfDeath )
				{
					res = controller.ControllerParams.DangerZoneForwardSpeed.Evaluate((Time.time - timer - delayTime) / timeOfDeath);
				}
				else
				{
					
					res = 0f;
				}
			}
			else
			{
				res = horizontSpeed;
			}
			return res;
		}
		protected override void PlayFinishAnimation()
		{
			controller.ApplyAnimation(controller.Animations.DangerZoneDead, controller.CrossfadeTimes.DangerZoneDeath);
		}

		protected override void HandeleCollide( PlaceForObstacle obj, ControllerColliderHit hit )
		{
			timer -= (delayTime + 1f);
			direction = -1;
		}
	}
}