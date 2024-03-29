﻿using Sound;
using UnityEngine;

namespace GamePlay
{
	class RuningState : BaseState
	{

		public RuningState(Controller controller) : base(controller)
		{
		}

		public override Vector3 UpdatePosition()
		{
			result = base.UpdatePosition();
			if (IsGrounded())
			{
				controller.ApplyAnimation(controller.Animations.Run, controller.CrossfadeTimes.Run);
			}
			else
				controller.SetState(controller.JumpingState);
					

			return result;
		}

	}
}
