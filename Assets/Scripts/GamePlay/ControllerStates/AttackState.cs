using System;
using LevelGeneration;
using Sound;
using UnityEngine;

namespace GamePlay
{
    public enum AttackType
    {
        //меч
        Sword = 0,
        //щит
        Shield = 1
    }

  internal class AttackState : BaseState
    {

        public float startTime;

        protected readonly float maxTime;
        protected float currentTime;
        protected float horizontalAcceleration;
        
        protected static bool AttachToRopeAfterAttack = false;
        protected static Rope savedRope;

          protected void HideWeapon()
          {
              if (controller.ShieldIn != null) controller.ShieldIn.enabled = false;
              if (controller.ShieldOut != null) controller.ShieldOut.enabled = false;
              if (controller.WrenchInHand != null) controller.WrenchInHand.enabled = false;
              if (controller.WrenchOnBelt != null) controller.WrenchOnBelt.enabled = true; 
          }

        protected AttackState(Controller controller,float maxTime) : base(controller)
        {
            this.maxTime = maxTime;
            currentTime = 0f;
        }

        public override Vector3 UpdatePosition()
        {
            verticalSpeed = 0;
            result = Vector3.zero;
            //result = base.UpdatePosition();
            currentTime = Time.time - startTime;
            horizontalAcceleration=controller.ControllerParams.AttackAcceleration.Evaluate(currentTime/maxTime);             

            //Debug.Log(string.Format("currentTime={0}, maxTime={1}", currentTime, maxTime));
            if(currentTime > maxTime)
            {
                horizontalAcceleration = 0f;
                currentTime = 0f;
                HideWeapon();
                result = new Vector3(result.x, result.y, controller.ControllerParams.RunSpeed);


                
                if (AttachToRopeAfterAttack && (savedRope.MaxZcoord() > controllerTransform.position.z))
                {
                    HideWeapon();
                    AttachToRopeAfterAttack = false;
                    controller.SetState(controller.RopeRollingState);
                    controller.RopeRollingState.rope = savedRope;           
                }
                else if (!IsGrounded())
                {
                    controller.SetState(controller.JumpingState);
                }
                else
                {
                    ApplyLastState(Controller.SavedStates.GetLastState());
                }
                //crossfade starts here
                controller.ApplyAnimation(controller.Animations.Run, controller.CrossfadeTimes.AttackRun);
            }
            else
            {
                //Debug.Log("controller.ControllerParams.RunSpeed="+controller.ControllerParams.RunSpeed+ " ,horizontalAcceleration=" + horizontalAcceleration);
                result.z = controller.ControllerParams.RunSpeed + horizontalAcceleration;
                PlayAnimation();
            }
            if ( !IsGrounded() ) wasInAirWhenAttacked = true;
            if(savedRope !=null)  Debug.Log("AttachToRopeAfterAttack=" + AttachToRopeAfterAttack + ", (savedRope.MaxZcoord() < controllerTransform.position.z)" + (savedRope.MaxZcoord() < controllerTransform.position.z));
            return result;
        }

        public virtual void PlayAnimation() { }

        public override void Jump()
        {
            Controller.SavedStates.PutState(controller.JumpingState);
            //verticalSpeed = controller.ControllerParams.JumpVerticalSpeed;
            //controller.SetState(controller.JumpingState);
        }
        public override void Jump( float startSpeedY )
        {
            if (!wasInAirWhenAttacked)
            {
                //TODO do with startSpeedY
                Controller.SavedStates.PutState(controller.JumpingState);
            }
        }


      protected override void HandeleCollide(DestroyableObstcle obj)
        {
        }

        public override void Attack( AttackType type )
        {
            switch ( type )
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
        public override void Turn( TurnDirection direction )
        {  
           TurningState turning = new TurningState(controller);
            turning.direction = direction;
            Controller.SavedStates.PutState(turning);
        }

        protected override void HandeleCollide( EnemyAbstract obj, ControllerColliderHit hit ) { Debug.Log("HandleCollide in AttackState"); }
        protected override void HandeleCollide( Rope obj )
        {
            AttachToRopeAfterAttack = true;
            savedRope = obj;
        }


        //protected void ReciveDamage(int damageValue)
        //{
        //    hitPoints -= damageValue;
        //    if(hitPoints<=0)
        //        Die();
        //}


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