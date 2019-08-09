using System.Runtime.Remoting;
using Sound;
using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
    public class CrazyBattery : TimePeriodBonus
    {
        public float coeff;
        public float animationCoeff;

        public override void Affect()
        {
        }

        protected override void LastEffect()
        {
            if (coeff < 0.01f) coeff = 0.01f;
            if (animationCoeff < 0.001f) animationCoeff = 0.01f;
            controller.ControllerParams.RunSpeed /= coeff;
            controller.ControllerParams.RunAnimationSpeed /= animationCoeff;
        }

        protected override void FirstEffect()
        {
            controller.ControllerParams.RunSpeed *= coeff;
            controller.ControllerParams.RunAnimationSpeed *= animationCoeff;
        }
    }

}
