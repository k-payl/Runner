using GamePlay;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

#if UNITY_EDITOR                        
    void OnDrawGizmos()
    {                                  
            var color = Color.green;
            color.a = 0.5f;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, 0.2f);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, 0.2f);
    }
#endif
}
