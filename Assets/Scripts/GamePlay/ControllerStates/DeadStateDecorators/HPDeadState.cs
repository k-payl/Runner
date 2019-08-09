using UnityEngine;
using System.Collections;

namespace GamePlay
{
    class HPDeadState : AbstractDeadStateDecorator
    {
        public HPDeadState(Controller controller) : base(controller, DeadReason.HP)
        {
            controllerCollider.center = new Vector3(0f, controller.ControllerColliderDeadHeight, 0f);
        }

        protected override float CalcZSpeed()
        {
            return -base.CalcZSpeed();
        }
        protected override void PlayFinishAnimation()
        {
            controller.ApplyAnimation(controller.Animations.GroundHeadDownDeath, 0.5f);
        }

        protected override void ApplyFallingAnimation()
        {
            controller.ApplyAnimation(controller.Animations.FallingForward, 0.3f);
        }
    }
}