using LevelGeneration;
using UnityEngine;
using System;

namespace Serialization
{
    /// <summary>
    /// Полная информация об Float и Roatator 
    /// </summary>
    [Serializable]
    public class FloatRotate
    {
        [SerializeField]private float floatDerivation;
        [SerializeField]private float floatPeriod;
        [SerializeField]private bool floatRandomize;
        [SerializeField]private int rotatorAxis;
        [SerializeField]private float rotatorPeriod;
        [SerializeField]private int rotatorDirection;
        [SerializeField]private bool rotatorRandomize;

        public FloatRotate(Floater floaterPoolable, Rotator rotatorPoolable)
        {
            if (floaterPoolable != null)
            {
                floatDerivation =       floaterPoolable.halfAmplitude;
                floatPeriod =           floaterPoolable.period;
                floatRandomize =        floaterPoolable.randomInitPosition;
            }
            if (rotatorPoolable != null)
            {
                rotatorAxis =           rotatorPoolable.axis;
                rotatorPeriod =         rotatorPoolable.period;
                rotatorDirection =      rotatorPoolable.direction;
                rotatorRandomize =      rotatorPoolable.randomize;
            }
        }

        public void RestoreObject(Floater floaterPoolable, Rotator rotatorPoolable)
        {
            if ( floaterPoolable != null )
            {
                floaterPoolable.halfAmplitude = floatDerivation;
                floaterPoolable.period = floatPeriod;
                floaterPoolable.randomInitPosition = floatRandomize;
            }
            if ( rotatorPoolable != null )
            {
                rotatorPoolable.axis = rotatorAxis;
                rotatorPoolable.period = rotatorPeriod;
                rotatorPoolable.direction = rotatorDirection;
                rotatorPoolable.randomize = rotatorRandomize;
            }
        }
    }


    public class FloatRotateSerializator : StandartSerializator
    {
        [SerializeField] private FloatRotate obj;
        [SerializeField] private FloatRotate c_obj;

        public override IPoolable DeserializeForRuntime()
        {
            IPoolable poolable = base.DeserializeForRuntime();
            Floater floaterPoolable = poolable.GetGameObject.GetComponent<Floater>();
            Rotator rotatorPoolable = poolable.GetGameObject.GetComponent<Rotator>();
            Floater cFloaterPoolable = poolable.GetGameObject.GetComponentInChildren<Floater>();
            Rotator c_rotatorPoolable = poolable.GetGameObject.GetComponentInChildren<Rotator>();

            obj.RestoreObject(floaterPoolable, rotatorPoolable);
            c_obj.RestoreObject(cFloaterPoolable, c_rotatorPoolable);
            return poolable;
        }

#if UNITY_EDITOR
        public override void Serialize( IPoolable poolable )
        {
            base.Serialize(poolable);
            Floater floaterPoolable = poolable.GetGameObject.GetComponent<Floater>();
            Rotator rotatorPoolable = poolable.GetGameObject.GetComponent<Rotator>();
            Floater cFloaterPoolable = poolable.GetGameObject.GetComponentInChildren<Floater>();
            Rotator c_rotatorPoolable = poolable.GetGameObject.GetComponentInChildren<Rotator>();

            obj = new FloatRotate(floaterPoolable, rotatorPoolable);
            c_obj = new FloatRotate(cFloaterPoolable, c_rotatorPoolable);
           
        }

        public override IPoolable DeserializeForEditor()
        {
            IPoolable poolable = base.DeserializeForEditor();
            
            Floater floaterPoolable = poolable.GetGameObject.GetComponent<Floater>();
            Rotator rotatorPoolable = poolable.GetGameObject.GetComponent<Rotator>();
            Floater cFloaterPoolable = poolable.GetGameObject.GetComponentInChildren<Floater>();
            Rotator c_rotatorPoolable = poolable.GetGameObject.GetComponentInChildren<Rotator>();

            obj.RestoreObject(floaterPoolable, rotatorPoolable);
            c_obj.RestoreObject(cFloaterPoolable, c_rotatorPoolable);
             
            return poolable;
        }
#endif
    }
}
