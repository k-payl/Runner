using LevelGeneration;
using UnityEngine;
using System.Collections;

namespace Serialization
{		
	public class ScalaableSerializator : StandartSerializator
	{
		[SerializeField] protected Vector3 scale;

		public override IPoolable DeserializeForRuntime()
		{
			IPoolable poolable = base.DeserializeForRuntime();
			poolable.GetGameObject.transform.localScale = scale;
			return poolable;
		}

#if UNITY_EDITOR
		public override void Serialize(IPoolable poolable)
		{
			base.Serialize(poolable);
			scale = poolable.GetGameObject.transform.lossyScale;
		}

		public override IPoolable DeserializeForEditor()
		{
			IPoolable poolable = base.DeserializeForEditor();
			poolable.GetGameObject.transform.localScale = scale;
			return poolable;
		}
#endif
	}
}