using UnityEngine;
using GamePlay;
using System.Collections;

namespace LevelGeneration
{
    [System.Serializable]
    public class PlaceForHill : PlaceForPolable
    {
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            DrawGizmo(Color.cyan);
        }
 #endif

    }
}
