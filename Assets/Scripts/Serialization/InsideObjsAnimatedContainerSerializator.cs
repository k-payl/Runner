using LevelGeneration;
using UnityEngine;

namespace Serialization
{
	public class InsideObjsAnimatedContainerSerializator : StandartSerializator
	{
		//[SerializeField] protected Vector3 localPosition;
		//Todo WTF?
		[SerializeField] protected Vector3 collideCenter;
		[SerializeField] protected Vector3 colliderSize;

		public override IPoolable DeserializeForRuntime()
		{
			IPoolable poolable = base.DeserializeForRuntime();
			//if (poolable.GetGameObject.GetComponentInChildren<Transform>() != null) poolable.GetGameObject.GetComponentInChildren<Transform>().localPosition = localPosition;
			poolable.GetGameObject.GetComponent<BoxCollider>().center = collideCenter;
			poolable.GetGameObject.GetComponent<BoxCollider>().size = colliderSize;
			
			return poolable;
		}

#if UNITY_EDITOR
		public override void Serialize(IPoolable poolable)
		{
			//if (poolable.GetGameObject.GetComponentInChildren<Transform>() != null) localPosition = poolable.GetGameObject.GetComponentInChildren<Transform>().localPosition;
			collideCenter = poolable.GetGameObject.GetComponent<BoxCollider>().center;
			colliderSize = poolable.GetGameObject.GetComponent<BoxCollider>().size;
			base.Serialize(poolable);
		}

		public override IPoolable DeserializeForEditor()
		{
			IPoolable poolable = base.DeserializeForEditor();
			//if (poolable.GetGameObject.GetComponentInChildren<Transform>() != null) poolable.GetGameObject.GetComponentInChildren<Transform>().localPosition = localPosition;
			poolable.GetGameObject.GetComponent<BoxCollider>().center = collideCenter;
			poolable.GetGameObject.GetComponent<BoxCollider>().size = colliderSize;
			return poolable;
		}
#endif
	}
}