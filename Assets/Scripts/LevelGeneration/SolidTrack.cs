using UnityEngine;
using System.Collections;

namespace GamePlay
{
    public class SolidTrack : TrackAbstract
    {

        public float width;
        
        private BoxCollider _collider;
        private Transform _transform;

        protected override void OnEnable()
        {
            _collider = collider as BoxCollider;
            _transform = transform;
        }

        public override float LineWidth
        {
            get { return MaxX() - MinX(); }
            set{}
        }

        /// <summary>
        /// </summary>
        /// <returns>Возвращает максимальную точку x в мировых координатах</returns>
        public override float MaxX()
        {
            return _transform.TransformPoint(_collider.center.x + width/2, _collider.center.y, _collider.center.z).x;
        }

        /// <summary>
        /// </summary>
        /// <returns>Возвращает минимальную точку x в мировых координатах</returns>
        public override float MinX()
        {
            return _transform.TransformPoint(_collider.center.x - width/2, _collider.center.y, _collider.center.z).x;
        }

        public override void CalculateTrackState(Vector3 pointAtTrack)
        {
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 center = _transform.TransformPoint(_collider.center);
            Vector3 size = new Vector3(width, 0.01f, _collider.bounds.size.z);
            Gizmos.DrawWireCube(center, size);
        }
#endif
    }
}