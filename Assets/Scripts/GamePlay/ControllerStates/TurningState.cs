using System;
using LevelGeneration;
using Sound;
using UnityEngine;

namespace GamePlay
{
	public enum TurnDirection
	{
		Left = 0,
		Right = 1,
		None = 2
	}
	internal class TurningState : BaseState  , IEquatable<TurningState>
	{
		public TurnDirection direction;

		private bool wasInDangerZone = false;
		private int count;

		public TurningState(Controller controller) : base(controller)
		{
		  
		}

		public override Vector3 UpdatePosition()
		{
		  
			
			
			result = Vector3.zero;
			result.z = controller.ControllerParams.RunSpeed;
			
			if ( IsGrounded() )
				verticalSpeed = 0;
			else
				verticalSpeed -= controller.ControllerParams.Gravity * Time.deltaTime;
			result.y = verticalSpeed;


			float trackXCoord = ((track as Track3) != null) ? (track as Track3).CurrentXCoord : controller.transform.position.z;
			if (trackXCoord*(direction == TurnDirection.Left ? 1f : -1f) >=
				controllerTransform.position.x*(direction == TurnDirection.Left ? 1f : -1f))
			{
				
				if (wasInDangerZone)
				{
					controller.ApplyAnimation(controller.Animations.Run, controller.CrossfadeTimes.Run);
					Dead(DeadReason.DangerZone);
				}
				else
				{
					ApplyLastState(Controller.SavedStates.GetLastState());
				}
			}
			else
			{
				float diff = trackXCoord - controllerTransform.position.x;
				result.x = Mathf.Sign(diff)*
						   controller.ControllerParams.TurningCurve.Evaluate(1f -
																			 Mathf.Sign(diff)*(diff/track.LineWidth))*
						   controller.ControllerParams.TurningSpeed;



				
				
				
				if ((count%2) == 0)
				{
					controller.ApplyAnimation(
						((direction == TurnDirection.Left)
							? controller.Animations.TurnToLeft
							: controller.Animations.TurnToRight), controller.CrossfadeTimes.Turning);
					
				}
				else
				{
					controller.ApplyAnimation(
						((direction == TurnDirection.Left)
							? controller.Animations.TurnToLeft1
							: controller.Animations.TurnToRight1), controller.CrossfadeTimes.Turning);
					
				}

			}
			return result;
		}

		public override void Jump()
		{
			JumpingState newJumping = new JumpingState(Controller.GetInstance());
			Controller.SavedStates.PutState(newJumping);
		}
		public override void Attack(AttackType type)
		{
			switch (type)
			{
					case AttackType.Sword:
				{
					SwordAttackState sword = new SwordAttackState(controller, controller.ControllerParams.SwordAttackTime);
					Controller.SavedStates.PutState(sword);
					break;
				}
				case AttackType.Shield:
				{
					ShieldAttackState shield = new ShieldAttackState(controller, controller.ControllerParams.ShieldAttackTime);
					Controller.SavedStates.PutState(shield);
					break;
				}
			}
			
		}

		public override void Turn(TurnDirection direction)
		{

				if (IsTurnAllow(direction))
				{
					track.Turn(direction);
					controller.TurningState.direction = direction;
					controller.SetState(controller.TurningState);
					count++;
					controller.soundEffects.PlayClip(PlayerClip.TurnSound);
					if (controller.footFlares != null) 
						controller.footFlares.StartAndStopLastCrossfadable(controller.timeFlareAction, controller.timeFlaresCrossfade);

				}
				else
				{
				   
					controller.DisallowedTurnState.Direction = direction;
					controller.SetState(controller.DisallowedTurnState);
					controller.soundEffects.PlayClip(PlayerClip.DissalowedTurnSound);
				}
		}

		protected override void HandeleCollide(EnemyAbstract obj, ControllerColliderHit hit)
		{
			float trackXCoord = ((track as Track3) != null) ? (track as Track3).CurrentXCoord : controller.transform.position.z;
			float diff = (trackXCoord - controllerTransform.position.x);
			TurnDirection direction = diff < 0 ? TurnDirection.Left : TurnDirection.Right;
			controller.DeadState.TurnDirection = direction;
			Dead(DeadReason.EnemySide);
		}
		protected override void HandeleCollide( PlaceForDangerZone obj )
		{
			wasInDangerZone = true;
		}

		public override void Run()
		{
			controller.ApplyAnimation(controller.Animations.Run, controller.CrossfadeTimes.TurnRun);
			base.Run();
		}

		protected override void ApplyLastState( BaseState state )
		{
			if ( state is RuningState )
			{
				Run();
			}
			if ( state is TurningState )
			{
				controller.TurningState = state as TurningState;
				base.Turn((state as TurningState).direction);
			}
			if ( state is JumpingState )
			{
				base.Jump();
			}
			if ( state is SwordAttackState )
			{
				controller.SwordAttackState = state as SwordAttackState;
				base.Attack(AttackType.Sword);
			}
			if ( state is ShieldAttackState )
			{
				controller.ShieldAttackState = state as ShieldAttackState;
				base.Attack(AttackType.Shield);
			}
		}


		public bool Equals(TurningState other)
		{
			return (this.direction == other.direction);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return false;
		}
	}
	
}
