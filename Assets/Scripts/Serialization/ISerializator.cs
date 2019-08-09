using LevelGeneration;
using UnityEngine;

namespace Serialization
{
	public interface ISerializator
	{
		IPoolable DeserializeForRuntime();
#if UNITY_EDITOR
		void Serialize( IPoolable poolable );
		IPoolable DeserializeForEditor();
	   
#endif
	}


}
