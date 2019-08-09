using UnityEngine;
using System.Collections;

namespace GamePlay
{
    internal class EnemyForwardCollision : AbstractDeadStateDecorator
    {
        public EnemyForwardCollision(Controller controller) : base(controller, DeadReason.EnemyForward)
        {
            controllerCollider.center = new Vector3(0f, controller.ControllerColliderDeadHeight, 0f);
        }
        protected override void PlayFinishAnimation()
        {
            controller.ApplyAnimation(controller.Animations.ElectroDead, .2f);
        }
    }
}
