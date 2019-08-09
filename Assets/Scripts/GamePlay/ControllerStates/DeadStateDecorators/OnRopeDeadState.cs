using UnityEngine;
using System.Collections;

namespace GamePlay
{
    //TODO implemet all
    class OnRopeDeadState : AbstractDeadStateDecorator
    {
        public OnRopeDeadState(Controller controller) : base(controller, DeadReason.OnRope)
        {
        }
        protected override float CalcZSpeed()
        {
            //TODO: переопределить
            return base.CalcZSpeed();
        }
        protected override void PlayFinishAnimation()
        {
            controller.ApplyAnimation(controller.Animations.GroundHeadUpDeath, .2f);
        }
    }
}
