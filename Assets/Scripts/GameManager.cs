using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GamePlay;
using LevelGeneration;
using Serialization;
using UnityEngine;

    /// <summary>
    /// Объект этого класса - единственный, который сохраняется между загрузками сцен
    /// </summary>
    //[ExecuteInEditMode]
    public class GameManager : Singleton<GameManager> //Singletone
    {
        public TrackChunkManager trackChunkManager;
        public Pool<AbstractPoolableObject> Pool;
        public GameInfo info;
        public PrefabInitializationManager PrefabInitializationManager;
        public LevelConfiguration currentLevelConfiguration;
        public string defaultLevel = "LEVEL";

  


        //#region Instance
        //private static GameManager instance;
        //public static GameManager Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = FindObjectOfType(typeof(GameManager)) as GameManager;
        //        if (instance == null)
        //        {
        //            Debug.Log("Creating new GameManager");
        //            instance = new GameObject("GameManager").AddComponent<GameManager>();
        //            DontDestroyOnLoad(instance.gameObject);
        //        }
        //        return instance;
        //    }
        //}
        //#endregion

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
                //Debug.Log("Awake in " + gameObject.name);
                DontDestroyOnLoad(gameObject);

                info = new GameInfo();
                info.Load();

                Pool = new Pool<AbstractPoolableObject>();
                //умная реинициализация пула
                //if (PrefabInitializationManager != null)
                //{
                //    foreach (PrefabItem prefabItem in PrefabInitializationManager.PrefabItems)
                //    {
                //        if (prefabItem.prefab.GetComponent<AbstractPoolableObject>() != null)
                //        {
                //            Pool.InstantiateAndAdd(prefabItem.prefab.GetComponent<AbstractPoolableObject>(),
                //                prefabItem.count);
                //        }
                //    }

                //}
            }

        }
        

        private void FixedUpdate()
        {
            //TODO
            //if (Controller.GetInstance() != null)
            //{
            //    bonuses.BatareyCharge -= currentLevelSettings.batteryConsumption;
            //}

        }

        void OnLevelWasLoaded()
        {

            if (IsCanUse)
            {
                if (Application.loadedLevelName != defaultLevel)
                {
                    //Debug.Log("menu->game in " + gameObject.name);
                    //ihntGUI.Instance.ShowIngameGUI();

                    trackChunkManager = TrackChunkManager.GetInstance();
                    currentLevelConfiguration = LevelConfiguration.Instance;
                    PrefabInitializationManager = FindObjectOfType<PrefabInitializationManager>();



                    //умная реинициализация пула
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
                        //ставим игрока
                        //настраиваем трек

                        TrackAbstract.GetInstance().CalculateTrackState(currentLevelConfiguration.DefaultPosition);
                        Controller.GetInstance().MoveToPosition(currentLevelConfiguration.DefaultPosition);
                        trackChunkManager.Initialize(currentLevelConfiguration.DefaultPosition);

                        //говорим что бонусы изменились(т.к начало уровня)
                        CollectionInitializedEvent(info.bonuses);
                    }
                }
                else
                {
                    //Debug.Log("game->menu");
                    Pool = new Pool<AbstractPoolableObject>();
                    trackChunkManager = null;
                    currentLevelConfiguration = null;
                    PrefabInitializationManager = null;


                }
            }
        }

        /// <summary>
        /// Сообщиить что левел закончен
        /// </summary>
        /// <param name="success">true - если игрок добежал до канца уровня</param>
        public void LevelFinished(bool success)
        {
            info.LevelFinished(success);
        }

        

        void OnApplicationQuit()
        {
            info.Save();
        }

    }
