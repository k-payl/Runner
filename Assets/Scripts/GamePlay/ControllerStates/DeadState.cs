using System;
using UnityEngine;

namespace GamePlay
{
    public enum DeadReason
    {
        ForwardCollision = 0,
        EnemyForward = 2,
        EnemySide = 3,
        DangerZone = 4,
        BatareyLow = 5,
        HP = 6,
        OnRope = 7,
        None = 8
    }

    internal class DeadState :BaseState
    {
        
	    private DeadReason deadReason;
        private TurnDirection turnDirection = TurnDirection.None;
        public TurnDirection TurnDirection
        {
            set { turnDirection = value; }
        }

        internal DeadReason DeadReason
	    {
	        set
	        {
	            deadReason = value;
                controller.soundEffects.PlayDeadSound(deadReason);
	            switch(deadReason)
	            {
                    case DeadReason.ForwardCollision:
                        controller.DeadState = new ForwardCollisionDeadState(controller);
                        break;
                    case DeadReason.EnemyForward:
                        controller.DeadState = new EnemyForwardCollision(controller);
                        break;
                    case DeadReason.EnemySide:
                        controller.DeadState = new EnemySideCollision(controller, turnDirection);
                        break;
                    case DeadReason.DangerZone:
                        controller.DeadState = new DangerZoneDeadState(controller);
                        break;
                    case DeadReason.BatareyLow:
                        controller.DeadState = new BataryLowDeadState(controller);
                        break;
                    case DeadReason.HP:
                        controller.DeadState = new HPDeadState(controller);
                        break;
                    case DeadReason.OnRope:
                        throw new NotImplementedException("OnRopeDeadReason");
	                default:
                        throw new ArgumentOutOfRangeException();
	            }
               
	        }
	    }
        
	    public DeadState(Controller controller) : base(controller)
	    {
	    }
        public override void Jump()
        {
        }
        public override void Run()
        {
        }
        public override void Turn(TurnDirection direction)
        {
        }
        public override void Attack(AttackType attackType)
        {
        }
        public override void Dead(DeadReason deadReason)
        {
        }
        public override void Stamered()
        {
        }
        protected override void HandeleCollide(LevelGeneration.PlaceForObstacle obj, ControllerColliderHit hit)
        {
        }
	}
}
