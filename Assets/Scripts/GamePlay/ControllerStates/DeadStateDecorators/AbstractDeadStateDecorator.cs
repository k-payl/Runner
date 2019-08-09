using UnityEngine;
using System.Collections;

namespace GamePlay
{
	internal class AbstractDeadStateDecorator : DeadState
	{
		protected float horizontSpeed;
		protected byte bounceCount = 0;
		private bool AnimationIsPlayed = false;
		private float stopWatch; 
	  
		public AbstractDeadStateDecorator(Controller controller, DeadReason reason, TurnDirection direction = TurnDirection.None) : base(controller)
		{
			
			horizontSpeed = -controller.ControllerParams.RunSpeed;
			  if (controller.ShieldIn != null) controller.ShieldIn.enabled = false;
			  if (controller.ShieldOut != null) controller.ShieldOut.enabled = false;
			  if (controller.WrenchInHand != null) controller.WrenchInHand.enabled = false;
			  if (controller.WrenchOnBelt != null) controller.WrenchOnBelt.enabled = true;
			if (bounceCount == 0)
				ApplyFallingAnimation();
			stopWatch = 2;
			ihntGUI.Instance.HideIngameGUI();
			ihntGUI.Instance.ShowGameOverGUI("levelNN");
			GameManager.Instance.LevelFinished(false);
		}
		public override Vector3 UpdatePosition()
		{
		  
		  
			result = Vector3.zero;

			
			if (bounceCount > 0 && !AnimationIsPlayed)
			{
				PlayFinishAnimation();
				AnimationIsPlayed = true;
			}

			
			if (AnimationIsPlayed && stopWatch > 0)
			{
				stopWatch -= Time.deltaTime;
			}

			if (stopWatch <= 0)
			{
				
				
				
			}

		   

			result = new Vector3(CalcXSpeed(), CalcYSpeed(), CalcZSpeed());
			return result;
		}

		protected virtual void ApplyFallingAnimation()
		{
			controller.ApplyAnimation(controller.Animations.FallingBack, 0.3f); 
		}


		protected virtual float CalcXSpeed()
		{
			return 0;
		}
		protected virtual float CalcYSpeed()
		{
			if (IsGrounded())
			{
				if(bounceCount <= controller.ControllerParams.BounceCount)
				{
					verticalSpeed = Mathf.Abs(verticalSpeed)*controller.ControllerParams.BounceCoef;
					bounceCount++;
				}
				else
				{
					verticalSpeed = -controller.ControllerParams.Gravity*Time.deltaTime;
				}
			}
			else
			{
				verticalSpeed -= controller.ControllerParams.Gravity * Time.deltaTime;
				
			}
		   
			return verticalSpeed;
		}
		protected virtual float CalcZSpeed()
		{
			if(IsGrounded())
			{
				if(Mathf.Abs(horizontSpeed) > 0.1f)
					horizontSpeed *= controller.ControllerParams.HorizontBounceFrictionCoef;
				else
					horizontSpeed = 0f;
			}
			else
				horizontSpeed = -controller.ControllerParams.RunSpeed/3;
			return horizontSpeed;
		}

		protected virtual void PlayFinishAnimation()
		{
			
		}

	}
}