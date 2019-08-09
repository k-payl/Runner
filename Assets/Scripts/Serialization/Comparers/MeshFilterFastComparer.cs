using System.Collections.Generic;
using LevelGeneration;
using UnityEngine;

namespace Serialization
{
	/// <summary>
	/// Использует GetInstanceID, поэтому этим компроатором можно сравнивать только объекты созданные в пределах одной сессии
	/// Например один и тот же объект созданный до и после перезапуска эдитора для этого компратора будет разным. 
	/// Для того чтобы сравнить такие объекты следует использовать MeshFilterDeepComparer
	/// </summary>
	public class MeshFilterFastComparer : IEqualityComparer<IPoolable>
	{

		public bool Equals(IPoolable x, IPoolable y)
		{
			bool fastExit = x.GetGameObject.GetComponents<Component>().Length == y.GetGameObject.GetComponents<Component>().Length;
			if(!fastExit)
				return false;
			MeshFilter xm = x.GetGameObject.GetComponent<MeshFilter>();
			MeshFilter ym = y.GetGameObject.GetComponent<MeshFilter>();
			return xm.sharedMesh.GetHashCode() == ym.sharedMesh.GetHashCode()
				   && x.GetGameObject.renderer.sharedMaterial.GetHashCode() == y.GetGameObject.renderer.sharedMaterial.GetHashCode();
		}

		public int GetHashCode(IPoolable obj)
		{
			MeshFilter objMesh = obj.GetGameObject.GetComponent<MeshFilter>();
			return objMesh.sharedMesh.GetHashCode() ^ obj.GetGameObject.renderer.sharedMaterial.GetHashCode();
		}

	}
}