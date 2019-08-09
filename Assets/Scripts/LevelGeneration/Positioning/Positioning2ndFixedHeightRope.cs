using UnityEngine;
using System.Collections;
namespace LevelGeneration
{
    public class Positioning2ndFixedHeightRope : PositioningFixedHeightRope
    {

        void Awake()
    {
        Height = HeightInfos.HeightSecondFloorRope;
    }
#if UNITY_EDITOR
        public override void Update()
        {
            if (!Application.isPlaying)
            {
                base.Update();
                BindToFloor();
            }
        }
#endif
    }
}
