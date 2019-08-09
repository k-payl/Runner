using UnityEngine;
using LevelGeneration;

namespace GamePlay
{
	internal class RopeRollingState : BaseState
	{
		public Rope rope;
		public float heightArm = 0f;

		private float speed;
		private Vector3 moveVector; 
		private Vector3 handPosition;
		private float dz;

		public RopeRollingState(Controller controller) : base(controller)
		{
			speed = controller.ControllerParams.RopeRollingSpeed;
			dz = 2f;
		}

		
			


		public override Vector3 UpdatePosition()
		{
			handPosition = controller.collider.bounds.center - Vector3.down * heightArm;
			float x = ((track as Track3) != null) ? (track as Track3).CurrentXCoord : controller.transform.position.z;
			Vector3 result = new Vector3(x, rope.CalculateYByZ(handPosition.z + dz), handPosition.z + dz);
			if ( handPosition.z < rope.MinZcoord() )
			{
				
				controller.ApplyAnimation(controller.Animations.JumpUp[0], controller.CrossfadeTimes.JumpingUp);
				moveVector =  new Vector3(0f, 0f, speed*0.8f);
			}
			else if ( handPosition.z > rope.MaxZcoord() )
			{
				Jump(1f);
			}
			else
			{  
				controller.ApplyAnimation(controller.Animations.JumpUp[0], controller.CrossfadeTimes.JumpingUp);
				moveVector = (result - handPosition) * speed;
			}

						
			rope.TrySetKnotPosition(handPosition);

			
			Debug.DrawLine(handPosition, handPosition + moveVector * 0.3f, Color.red, 0.01f);

			return moveVector;
		}

		public override void JumpDown()
		{
			Jump(-4f);
		}
	}

}