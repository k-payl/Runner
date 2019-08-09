using System;
using System.Diagnostics;
using GamePlay;
using UnityEngine;
using System.Collections.Generic;
using Serialization;
using System.Linq;
using Debug = UnityEngine.Debug;

namespace LevelGeneration
{
	public class TrackChunk : MonoBehaviour
	{
		private Vector3 startCenterPosition;
		private float size;
		public float Size { get { return size; } }
		private bool activated;

		public List<IPoolable> instantiatedPoolables;
		[SerializeField,HideInInspector] private BoxCollider enterTriger;
		[SerializeField]private Bounds bounds;

		public void OnEnable()
		{
			instantiatedPoolables = new List<IPoolable>();
		}
#if UNITY_EDITOR
		public void Init(Vector3 startPosition,IEnumerable<IPoolable> Pooalables, float size)
		{
			activated = false;
			this.startCenterPosition = startPosition;
			this.size = size;
			enterTriger = gameObject.AddComponent<BoxCollider>();
			enterTriger.transform.position = this.startCenterPosition;
			enterTriger.size = new Vector3(TrackAbstract.GetInstance().LineWidth*10f,40f,1f); 
			enterTriger.isTrigger = true;
			
			var trigerScript = gameObject.AddComponent<EnterTrackChunkStartTrigger>();
			trigerScript.chunk = this;
			bounds = new Bounds(startPosition+new Vector3(0f,5f,size/2f),new Vector3(100f,20f,size));
			
			foreach (IPoolable poolable in Pooalables)
			{
				if( bounds.Contains(poolable.GetGameObject.transform.position))
				{
					try
					{
						ISerializator serializator = (ISerializator) gameObject.AddComponent(SerializatorTypes.SerializatorFor(poolable.GetType().Name));
						if ( serializator == null )
						{
							Debug.LogWarning(poolable.GetType().Name+"Serializator not found");
						}
						else
						{
							serializator.Serialize(poolable);
						}
					}
					catch (Exception e)
					{
						Debug.LogException(e);
						Debug.LogError(poolable);
					}
				}
			}
		}

		
#endif
		public bool Contains(Vector3 point)
		{
			return bounds.Contains(point);
		}
		public void Remove()
		{
		   DestroyImmediate(gameObject);
		}

		public void Activate()
		{
			 if(!activated)
			 {
				 ISerializator[] serializators = GetComponents<MonoBehaviour>().OfType<ISerializator>().ToArray();
				 
				 //int i = 0; 
				 //Stopwatch stopwatch = new Stopwatch();
				 //TimeSpan time = new TimeSpan(0,0,0,0,0);
				 foreach (ISerializator serializedGameObject in serializators)
				 {
					 //i++;
					 //stopwatch.Start();
					 IPoolable poolable = serializedGameObject.DeserializeForRuntime();
					 //stopwatch.Stop();
					 //time = time + stopwatch.Elapsed;
					 //UnityEngine.Debug.Log("Time of one search: " + stopwatch.Elapsed+". (sum:"+time+")");
					 //stopwatch.Reset();

					 if (poolable != null)
					 {
						 if (instantiatedPoolables==null) 
							 instantiatedPoolables = new List<IPoolable>();
							 instantiatedPoolables.Add(poolable);
						 poolable.Init();
					 }
					 else
					 {
						 Debug.Log("TrackChunk.ActivateTrackCHunk(): null poolable in serializator " + serializedGameObject.GetType());
					 }
				 }
				 //if (i > 1)
				 //{
				 //	TimeSpan result = TimeSpan.FromTicks(time.Ticks/i);
				 //	Debug.Log("Average:" + result);
				 //}
				 //Debug.Log("TrackChunk "+gameObject.name +" activated");
			 }
			activated = true;
		}

		public void Deactivate()
		{
			if (activated)
			{
				activated = false;
				foreach (var poolavle in instantiatedPoolables)
				{
					poolavle.ResetState();
				}
				instantiatedPoolables.Clear();
			}
		}

	 
		public override string ToString()
		{
			return name;
		}

#if UNITY_EDITOR
	//	private bool foldout = false;
		public void OnGUI()
		{
			//if (serializedPooalables!=null && serializedPooalables.Count > 0)
			//{
			//	foldout = UnityEditor.EditorGUILayout.Foldout(foldout, startPosition.ToString());
			//	if(foldout)
			//	{
			//		foreach(var serializedGameObject in serializedPooalables)
			//		{
			//			UnityEditor.EditorGUILayout.BeginHorizontal();
			//			GUILayout.Label(" ");
			//			serializedGameObject.OnGUI();
			//			UnityEditor.EditorGUILayout.EndHorizontal();
			//		}
			//	}
			//}
			//else
			//{
			//	GUILayout.Label(startPosition.ToString());
			//}
		}

#endif
		public void OnDrawGizmosSelected()
		{
			if (activated)
			{
				Gizmos.color = Color.red;
			}
			else
			{
				Gizmos.color = Color.blue;   
			}
			Gizmos.DrawWireCube(bounds.center, bounds.size);
		}

	}
}