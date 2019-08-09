using System;
using System.Linq;
using GamePlay;
using Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;

namespace LevelGeneration
{
	
	//[ExecuteInEditMode]
	public class TrackChunkManager : MonoBehaviour
	{
		public float ChunkSize;
		public int NumberOfActiveChunksAhead;
		public int NumberOfActiveChunksBehind;
		public GameObject blockPrefab;// декоративный префаб для упаковки всех объектов в эти блоки
		public List<TrackChunk> trackChuncks; //TODO convert to Queue
		private int currentChunk = -1;
		private static TrackChunkManager instance;
		public static TrackChunkManager GetInstance()
		{
			return instance ?? FindObjectOfType(typeof(TrackChunkManager)) as TrackChunkManager;
		}

		public void OnEnable()
		{
			instance = this;
		}

		public void Start()
		{
			//if (trackChuncks.Count == 0 )
			//{
			//	trackChuncks = GetComponentsInChildren<TrackChunk>().ToList();
			//}
			if (trackChuncks==null) trackChuncks = new List<TrackChunk>();
			

			
		}


		/// <summary>
		/// Правильно инициализировать чанки
		/// </summary>
		/// <param name="position">Начальное положение игрока</param>
		public void Initialize(Vector3 position)
		{
			int startInd = -1;

			//поиск стартового чанка
			//todo убрать и начинать с 0
			for (int i = 0; i < trackChuncks.Count; i++)
				if (trackChuncks[i].Contains(position) && trackChuncks[i] != null)
				{
					startInd = i;
					break;
				}

			//инициализация первых чанков
			if (startInd >= 0) CheckForNewChunks(trackChuncks[startInd]);
		}

		/// <summary>
		/// Правильно вырубить
		/// Использовать при перепрохождении уровня
		/// </summary>
		public void DeInitialize()
		{
			foreach (var chunck in trackChuncks)
			{
				chunck.Deactivate();
			}
		}
		
		

		public void CheckForNewChunks(TrackChunk chunk)
		{
				currentChunk = trackChuncks.IndexOf(chunk);
				if(currentChunk > -1)
				{
					//вниз
					int i = currentChunk;
					while ((i > -1) && (i > (currentChunk - NumberOfActiveChunksBehind)))
					{
						trackChuncks[i].Activate();
						i--;
					}

					if (--i > -1) trackChuncks[i].Deactivate();

					//вверх
					i = currentChunk;
					while ((i < trackChuncks.Count) && (i <= (currentChunk + NumberOfActiveChunksAhead)))
					{
						trackChuncks[i].Activate();
						i++;
					}
				}
				else throw new UnityException("Chunk " + chunk + " is not registred in chunkCollection.index="+currentChunk);
		}

		public void DestroyAllBlocks()
		{
			BlockDecorator[] blocksDecorator = FindObjectsOfType<BlockDecorator>();
			if (blocksDecorator != null)
				foreach (BlockDecorator block in blocksDecorator)
				{
					DestroyImmediate(block.gameObject);
				}
		}

		public void PutAllPoolablesToBlocks()
		{
			if (blockPrefab != null)
			{
				float ZSizeBlock = BlockDecorator.ZSize;
				float trackSizeZ = 0;
				Vector3 tarckStart = Vector3.zero;
				TrackAbstract track = TrackAbstract.GetInstance();
				if(null == track)
					throw new UnityException("The Track is not found in scene");
				else
				{
					trackSizeZ = TrackAbstract.GetInstance().collider.bounds.size.z;
					tarckStart = new Vector3(TrackAbstract.GetInstance().collider.bounds.center.x,0f,TrackAbstract.GetInstance().collider.bounds.min.z);
				}
				List<IPoolable> Poolables;
				MonoBehaviour[] behaviours = FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];
				if ( behaviours != null )
					Poolables = behaviours.OfType<IPoolable>().ToList();
				else
					Poolables = new List<IPoolable>(0);

				for (int i = 0; i*ZSizeBlock < trackSizeZ; i++)
				{
					GameObject newGO = Instantiate(blockPrefab, tarckStart + i*Vector3.forward*ZSizeBlock + Vector3.forward*ZSizeBlock*0.5f, Quaternion.identity) as GameObject;
					newGO.name = (i<10)? "block_0" + i : "block_" + i;
					foreach (IPoolable poolable in Poolables)
					{
						Bounds bound = new Bounds(newGO.transform.position, new Vector3(10000, 10000, ZSizeBlock));
						if (bound.Contains(poolable.GetGameObject.transform.position))
						{
							poolable.GetGameObject.transform.parent = newGO.transform;
						}
					}

				}
				
			}
			else
				Debug.LogWarning("Unable to pack poolables to blocks. Set blockPrefab to TrackChunkManager");
		}
		

#if UNITY_EDITOR

		[ContextMenu("GenerateChunks")]
		public void GenerateChunks()
		{
			Debug.Log("GenereateChunks()");
			Vector3 trackSrart = Vector3.zero;
			int chunkCount = 0;
			
			TrackAbstract track = TrackAbstract.GetInstance();
			if (null == track)
				throw new UnityException("The Track is not found in scene");
			else
			{
				float fullSize = track.GetComponent<Collider>().bounds.size.z;
				trackSrart = new Vector3(track.collider.bounds.center.x, track.collider.bounds.max.y,
											  track.collider.bounds.min.z);
				chunkCount = Mathf.CeilToInt(fullSize / ChunkSize);
			}
			Debug.Log("chunkCount:"+chunkCount);
			trackChuncks = new List<TrackChunk>(chunkCount);
			List<IPoolable> Poolables;
			MonoBehaviour[] behaviours = FindObjectsOfType(typeof(MonoBehaviour)) as MonoBehaviour[];
			if (behaviours != null)
				Poolables = behaviours.OfType<IPoolable>().ToList();
			else
				Poolables = new List<IPoolable>(0);

			for (int j = 0; j < chunkCount; j++)
			{
				Vector3 startPoint = trackSrart + new Vector3(0, 0, ChunkSize * j);
				GameObject newObjChunk = new GameObject { name = "chunk" + j };
				UnityEditor.GameObjectUtility.SetStaticEditorFlags(newObjChunk, UnityEditor.StaticEditorFlags.BatchingStatic);
				newObjChunk.transform.parent = gameObject.transform;
				TrackChunk newChunk = newObjChunk.AddComponent<TrackChunk>();
				//------main in this script--------
				newChunk.Init(startPoint, Poolables, ChunkSize);
				//---------------
				newObjChunk.layer = newObjChunk.transform.parent.gameObject.layer;
				trackChuncks.Add(newChunk);
				Debug.Log("trackChunks.Count: "+trackChuncks.Count);
			}
			try
			{
				foreach (IPoolable poolable in Poolables)
				{
					if (poolable != null)
						if (poolable.GetGameObject != null) DestroyImmediate(poolable.GetGameObject);
				}
			}
			catch (Exception)
			{

				Debug.Log("MissingReferenceException: The object of type 'Obstacle' has been destroyed but you are still trying to access it.");
			}
			
			EditorUtility.SetDirty(this);
		}

		[ContextMenu("DeserializeInChunks")]
		public void DeserializeFromChunks()
		{
			trackChuncks = (FindObjectsOfType(typeof(TrackChunk)) as TrackChunk[]).ToList();
			foreach (TrackChunk trackChunck in trackChuncks)
			{
				List<ISerializator> serializators = trackChunck.GetComponents<MonoBehaviour>().OfType<ISerializator>().ToList();
				foreach (ISerializator serializator in serializators)
					serializator.DeserializeForEditor();
				DestroyImmediate(trackChunck.gameObject);
			}
			trackChuncks.Clear();
		}
#endif
		public void OnDisable()
		{
			instance = null;
		}

		
	}
}