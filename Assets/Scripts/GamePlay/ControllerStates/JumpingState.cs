using Sound;
using UnityEngine;
using Random = UnityEngine.Random;
using LevelGeneration;

namespace GamePlay
{

	//TODO: Во время прыжка случайный клип анимации может поменяться
	class JumpingState: BaseState
	{
		private int currentUpAnim;
		private int currentDownAnim;
		public JumpingState(Controller controller) : base(controller)
		{
			currentUpAnim = Random.Range(0, controller.Animations.JumpUp.Length);
			currentDownAnim = Random.Range(0, controller.Animations.JumpDown.Length);
		}
		public override void Jump()
		{
		   // Debug.Log("trying jump");
			Controller.SavedStates.PutState(controller.JumpingState);
		}

		public override void JumpDown()
		{
			Jump(-4f);
		}

		public override Vector3 UpdatePosition()
		{
			result.x = 0f;
			result.z =controller.ControllerParams.RunSpeed + controller.ControllerParams.JumpingAcceleration;
			verticalSpeed -= controller.ControllerParams.Gravity * Time.deltaTime;
			result.y = verticalSpeed;
			if(verticalSpeed < 0)
			{
				controller.ApplyAnimation(controller.Animations.JumpDown[currentDownAnim], controller.CrossfadeTimes.JumpingDown);
			}
			else
				controller.ApplyAnimation(controller.Animations.JumpUp[currentUpAnim], controller.CrossfadeTimes.JumpingUp);

			if ( verticalSpeed < 0 && IsGrounded())
			{
				currentUpAnim = Random.Range(0, controller.Animations.JumpUp.Length);
				currentDownAnim = Random.Range(0, controller.Animations.JumpDown.Length);
				verticalSpeed = 0;
				wasInAirWhenAttacked = false;
				ApplyLastState(Controller.SavedStates.GetLastState());
			}

			return result;
		}

		public override void Attack(AttackType attackType)
		{
			if (!wasInAirWhenAttacked)
			{
				wasInAirWhenAttacked = true;
				base.Attack(attackType);
			}
			else
			{
				switch ( attackType )
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
		}
		protected override void HandeleCollide( PlaceForDangerZone obj )
		{
			controller.ApplyAnimation(controller.Animations.Run, controller.CrossfadeTimes.Run);
			Dead(DeadReason.DangerZone);
		}
		/// <summary>Делает нужные действия при переходе в отложенное в очередь состояние. 
		/// Принимает RuningState, либо TurningState, либо  JumpingState, либо SwordAttackState, либо ShieldAttackState</summary>
		protected override void ApplyLastState( BaseState state )
		{
			if ( state is RuningState )
			{
				controller.soundEffects.PlayClip(PlayerClip.JumpRunClip);
				Run();
			}
			if ( state is TurningState )
			{
				controller.TurningState = state as TurningState;
				base.Turn((state as TurningState).direction);
			}
			if ( state is JumpingState )
			{
				//it's important that "base." !
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
	}
}
