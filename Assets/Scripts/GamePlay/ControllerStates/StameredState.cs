using UnityEngine;

namespace GamePlay
{
	internal class StameredState:BaseState
	{
		private float stameredTime;
		private float maxStameredTime;
		public StameredState(Controller controller) : base(controller)
		{
			maxStameredTime = controller.ControllerParams.StameredTime;
		}
		public override void Jump()
		{
		}

		public override void Turn(TurnDirection direction)
		{
		}


		public override Vector3 UpdatePosition()
		{
			controller.ApplyAnimation(controller.Animations.Stammered,0.3f);
			stameredTime -= Time.deltaTime;
			if(stameredTime<0)
			{
				Run();
				stameredTime = maxStameredTime;
			}
			return base.UpdatePosition();
		}
	}
}
