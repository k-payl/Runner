using TemporaryActions;
using UnityEngine;

public class SlowMotionEffect : TemporaryAction {

    public AnimationCurve SlowMotionCurve;
    public float slowMotionTime;
  

    protected override void Effect()
    {
        float value = SlowMotionCurve.Evaluate(currentTime/time);
        if (value > 0) Time.timeScale = value;
    }

    protected override void FirstEffect()
    {
    }

    protected override void LastEffect()
    {
        Time.timeScale = 1;
    }
}
