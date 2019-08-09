using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PlayingAnimationDetector : MonoBehaviour
{

    public GameObject target;

    void OnTriggerEnter()
    {
        if (target.animation)
        {
            target.animation.Play();
            collider.enabled = false;
        }
    }
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "anim_trigger.png");
    }
#endif
}
