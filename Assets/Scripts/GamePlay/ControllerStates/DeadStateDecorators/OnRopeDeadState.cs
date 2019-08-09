using UnityEngine;
using System.Collections;

namespace GamePlay
{
	
	class OnRopeDeadState : AbstractDeadStateDecorator
	{
		public OnRopeDeadState(Controller controller) : base(controller, DeadReason.OnRope)
		{
		}
		protected override float CalcZSpeed()
		{
			
			return base.CalcZSpeed();
		}
		protected override void PlayFinishAnimation()
		{
			controller.ApplyAnimation(controller.Animations.GroundHeadUpDeath, .2f);
		}
	}
}
