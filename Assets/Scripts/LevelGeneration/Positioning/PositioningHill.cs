using UnityEngine;
using System.Collections;

namespace LevelGeneration
{
    [ExecuteInEditMode]
    public class PositioningHill : Positioning
    {   
#if UNITY_EDITOR
        public override void Update()
        {
            if (!Application.isPlaying)
            {
                base.Update();
                transform.position = new Vector3(transform.position.x,
                    transform.lossyScale.z*Mathf.Sin(Mathf.Deg2Rad*transform.eulerAngles.x)/2f, transform.position.z);
            }
        }
#endif
    }
}