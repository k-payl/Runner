using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GamePlay;
using LevelGeneration;
using Serialization;
using UnityEngine;

	
	
	
	
	public class GameManager : Singleton<GameManager> 
	{
		public TrackChunkManager trackChunkManager;
		public Pool<AbstractPoolableObject> Pool;
		public GameInfo info;
		public PrefabInitializationManager PrefabInitializationManager;
		public LevelConfiguration currentLevelConfiguration;
		public string defaultLevel = "LEVEL";

  


		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		

		#region Events
		public event BonusHandler BonusCollected;
		public event BonusHandler BonusMissed;
		public event BonusCollectionHandler CollectionInitialized;

		public void BonusCollectedEvent(Bonus bonus, BonusCollection collection)
		{
			if (BonusCollected != null) BonusCollected(bonus, collection);
		}

		public void BonusMissedEvent(Bonus bonus, BonusCollection collection)
		{
			if (BonusMissed != null) BonusMissed(bonus, collection);
		}

		public void CollectionInitializedEvent(BonusCollection collection)
		{
			if (CollectionInitialized != null) CollectionInitialized(collection);
		}
		#endregion


		protected  override void Awake()
		{
			base.Awake();
			if (IsCanUse)
			{
				
				DontDestroyOnLoad(gameObject);

				info = new GameInfo();
				info.Load();

				Pool = new Pool<AbstractPoolableObject>();
				
				
				
				
				
				
				
				
				
				
				

				
			}

		}
		

		private void FixedUpdate()
		{
			
			
			
			
			

		}

		void OnLevelWasLoaded()
		{

			if (IsCanUse)
			{
				if (Application.loadedLevelName != defaultLevel)
				{
					
					

					trackChunkManager = TrackChunkManager.GetInstance();
					currentLevelConfiguration = LevelConfiguration.Instance;
					PrefabInitializationManager = FindObjectOfType<PrefabInitializationManager>();



					
					if (PrefabInitializationManager != null)
					{
						foreach (PrefabItem prefabItem in PrefabInitializationManager.PrefabItems)
						{
							if (prefabItem.prefab.GetComponent<AbstractPoolableObject>() != null)
							{
								Pool.InstantiateAndAdd(
									prefabItem.prefab.GetComponent<AbstractPoolableObject>(),
									prefabItem.count);
							}
						}

					}


					if (Controller.GetInstance() != null)
					{
						
						

						TrackAbstract.GetInstance().CalculateTrackState(currentLevelConfiguration.DefaultPosition);
						Controller.GetInstance().MoveToPosition(currentLevelConfiguration.DefaultPosition);
						trackChunkManager.Initialize(currentLevelConfiguration.DefaultPosition);

						
						CollectionInitializedEvent(info.bonuses);
					}
				}
				else
				{
					
					Pool = new Pool<AbstractPoolableObject>();
					trackChunkManager = null;
					currentLevelConfiguration = null;
					PrefabInitializationManager = null;


				}
			}
		}

		
		
		
		
		public void LevelFinished(bool success)
		{
			info.LevelFinished(success);
		}

		

		void OnApplicationQuit()
		{
			info.Save();
		}

	}
