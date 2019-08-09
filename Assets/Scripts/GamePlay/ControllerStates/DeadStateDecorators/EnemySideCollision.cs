using System;
using UnityEngine;


namespace GamePlay
{
	class EnemySideCollision : AbstractDeadStateDecorator
	{
		private readonly TurnDirection collisionSide;
	   
		public EnemySideCollision(Controller controller, TurnDirection collisionSide)
			: base(controller, DeadReason.EnemySide, collisionSide)
		{
			this.collisionSide = collisionSide;
			controllerCollider.center = new Vector3(0f, controller.ControllerColliderDeadHeight, 0f);
		}

		protected override float CalcXSpeed()
		{
			switch(collisionSide)
			{
				case TurnDirection.Left:
					return -base.CalcZSpeed();
				case TurnDirection.Right:
					return base.CalcZSpeed();
				case TurnDirection.None:
					return 0;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		protected override void PlayFinishAnimation()
		{
			controller.ApplyAnimation(controller.Animations.ElectroDead, 0.2f);
		}
	}
}