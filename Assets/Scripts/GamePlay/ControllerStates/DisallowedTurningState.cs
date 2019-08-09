using UnityEngine;

namespace GamePlay
{
	internal class DisallowedTurningState : BaseState
	{
		private TurnDirection direction;
		public TurnDirection Direction
		{
			set { direction = value; }
		}
		private readonly float maxTime;
		private float currentTime;

		public DisallowedTurningState(Controller controller)
			: base(controller)
		{
			maxTime = Mathf.Min(controller.Animations.LeftDiasllowedTurn.length, controller.Animations.RightDiasllowedTurn.length);
			
		}
		public override void Jump()
		{
			Controller.SavedStates.PutState(controller.JumpingState);
		}

		public override void Turn(TurnDirection turnDirection)
		{
			if ( turnDirection != direction )
			{
				TurningState turning = new TurningState(controller);
				turning.direction = turnDirection;
				Controller.SavedStates.PutState(turning);
			}
		}

		public override Vector3 UpdatePosition()
		{
			result = base.UpdatePosition();
			currentTime += Time.deltaTime;
			if(currentTime >= maxTime)
			{
				currentTime = 0f;
				ApplyLastState(Controller.SavedStates.GetLastState());
			}
			else
			{
				controller.ApplyAnimation(direction==TurnDirection.Left ?
													controller.Animations.LeftDiasllowedTurn : 
													controller.Animations.RightDiasllowedTurn,
													controller.CrossfadeTimes.Turning);
			}
			return result;
		}

		public override void Attack(AttackType type)
		{
			switch ( type )
			{
				case AttackType.Shield: Controller.SavedStates.PutState(Controller.GetInstance().ShieldAttackState); break;
				case AttackType.Sword: Controller.SavedStates.PutState(Controller.GetInstance().SwordAttackState); break;
			}
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
	}
}