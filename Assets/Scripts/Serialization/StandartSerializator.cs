using GamePlay;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;
using LevelGeneration;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Serialization
{
	[Serializable,ExecuteInEditMode]
   public class StandartSerializator : MonoBehaviour, ISerializator
	{
		[SerializeField] public GameObject prefab;
		[SerializeField] protected Vector3 position;
		[SerializeField] protected Quaternion rotation;
		[SerializeField] protected string originalName;
		[SerializeField] protected int line;

		public virtual IPoolable DeserializeForRuntime()
		{
			IPoolable poolable = null;
			
			
			
			
			

				poolable = GameManager.Instance.Pool.GetObject(prefab.GetComponent<AbstractPoolableObject>());
				poolable.IsUsedNow = true;
				poolable.GetGameObject.transform.position = position;
				poolable.GetGameObject.transform.rotation = rotation;
				
			
			return poolable;
		}
#if UNITY_EDITOR
		public virtual void Serialize(IPoolable poolable)
		{
			prefab = Utils.FindOrCreatePrefab(poolable);
			position = poolable.GetGameObject.transform.position;
				rotation = poolable.GetGameObject.transform.localRotation;
				originalName = prefab.name;
				Positioning positioning = poolable.GetGameObject.GetComponent<Positioning>();
				if (positioning != null)
				{
					line = positioning.LineNumber;
				}
		}

		public virtual IPoolable DeserializeForEditor()
		{
			GameObject obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			obj.name = originalName;
			Positioning positioning = obj.GetComponent<Positioning>();
			if ( positioning != null )
			{
				positioning.LineNumber = line;
			}
			return obj.GetComponent<AbstractPoolableObject>();
		}
#endif

	}

}