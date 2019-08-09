using UnityEngine;
using System.Collections;

namespace GamePlay
{

    internal class ForwardCollisionDeadState : AbstractDeadStateDecorator
    {

        public ForwardCollisionDeadState(Controller controller) : base(controller, DeadReason.ForwardCollision)
        {
            
            controllerCollider.center = new Vector3(0f, controller.ControllerColliderDeadHeight, 0f);
        }
        protected override void PlayFinishAnimation()
        {
            controller.ApplyAnimation(controller.Animations.GroundHeadUpDeath, 1f);
        }
      
    }
}