using UnityEngine;

namespace GamePlay
{

	public class FinishState : BaseState
	{
		public float timeOfBreaking = 2f;
		private float startTime;
		private float animationSavedSpeed;
		private float savedZSpeed;
		private Vector3 res;


		public FinishState(Controller controller) : base(controller)
		{
			startTime = Time.time;
			animationSavedSpeed = controller.ControllerParams.RunAnimationSpeed;
			res = base.UpdatePosition();
			savedZSpeed = res.z;
			
			
			
		}

		public override Vector3 UpdatePosition()
		{
			
			
			res = Vector3.zero;

			

			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			
			return res;
		}

		public override void Attack(AttackType attackType)
		{
		}

		public override void Jump()
		{
		}

		public override void JumpDown()
		{
		}

		public override void Turn(TurnDirection direction)
		{
		}
	}
}
