using GamePlay;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Serialization;

namespace LevelGeneration
{
    [RequireComponent(typeof (BoxCollider)), System.Serializable]
    abstract public class PlaceForPolable : AbstractPoolableObject
    {
        public Vector3 Size
        {
            get { return collider.bounds.size; }
        }


        protected void OnEnable(){}
#if UNITY_EDITOR
        private const float gizmoLineLength = 0.8f; 
#endif

        
    }
}