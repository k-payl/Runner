using UnityEngine;
using System.Collections;
namespace LevelGeneration
{
    [ExecuteInEditMode, System.Serializable]
    public class PositioningByFloor : Positioning
    {

        // [HideInInspector]
        public int Floor;
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

        public virtual void BindToFloor()
        {
            float yCoord;
            float epsilon = 0.01f;
            switch ( Floor )
            {
                case 0: yCoord = HeightInfos.HeightGroundFloor + transform.lossyScale.y / 2f + epsilon;
                    break;
                case 1: yCoord = HeightInfos.HeightFirstFloor + transform.lossyScale.y / 2f + epsilon;
                    break;
                case 2: yCoord = HeightInfos.HeightSeconfFloor + transform.lossyScale.y / 2f + epsilon;
                    break;
                default: yCoord = HeightInfos.HeightGroundFloor + transform.lossyScale.y / 2f + epsilon;
                    break;
            }
            transform.position = new Vector3(transform.position.x, yCoord, transform.position.z);

        }
    }
}
