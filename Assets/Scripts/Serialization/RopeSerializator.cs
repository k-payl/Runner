using LevelGeneration;
using UnityEngine;
using System.Collections;

namespace Serialization
{        
    public class RopeSerializator : StandartSerializator
    {
        [SerializeField] protected float lenght;

        public override IPoolable DeserializeForRuntime()
        {
            Rope rope = base.DeserializeForRuntime() as Rope;
            if (rope != null)
                rope.Lenght = lenght;
            return rope;
        }

#if UNITY_EDITOR
        public override void Serialize(IPoolable poolable)
        {
            base.Serialize(poolable);
            lenght = (poolable as Rope).Lenght;
        }

        public override IPoolable DeserializeForEditor()
        {
            Rope rope = base.DeserializeForEditor() as Rope;
            if ( rope != null )
                rope.Lenght = lenght;
            return rope;
        }
#endif
    }
}