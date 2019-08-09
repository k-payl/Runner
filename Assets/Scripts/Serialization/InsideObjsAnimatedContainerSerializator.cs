using LevelGeneration;
using UnityEngine;

namespace Serialization
{
	public class InsideObjsAnimatedContainerSerializator : StandartSerializator
	{
		
		
		[SerializeField] protected Vector3 collideCenter;
		[SerializeField] protected Vector3 colliderSize;

		public override IPoolable DeserializeForRuntime()
		{
			IPoolable poolable = base.DeserializeForRuntime();
			
			poolable.GetGameObject.GetComponent<BoxCollider>().center = collideCenter;
			poolable.GetGameObject.GetComponent<BoxCollider>().size = colliderSize;
			
			return poolable;
		}

#if UNITY_EDITOR
		public override void Serialize(IPoolable poolable)
		{
			
			collideCenter = poolable.GetGameObject.GetComponent<BoxCollider>().center;
			colliderSize = poolable.GetGameObject.GetComponent<BoxCollider>().size;
			base.Serialize(poolable);
		}

		public override IPoolable DeserializeForEditor()
		{
			IPoolable poolable = base.DeserializeForEditor();
			
			poolable.GetGameObject.GetComponent<BoxCollider>().center = collideCenter;
			poolable.GetGameObject.GetComponent<BoxCollider>().size = colliderSize;
			return poolable;
		}
#endif
	}
}