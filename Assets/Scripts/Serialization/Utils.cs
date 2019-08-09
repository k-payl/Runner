using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR 
using UnityEditor; 
#endif
using LevelGeneration;
using System.IO;
using Object = UnityEngine.Object;

namespace Serialization
{
#if UNITY_EDITOR 
    public class Utils
    {
        public static GameObject FindOrCreatePrefab(IPoolable poolable)
        {
            if (poolable != null)
            {
                GameObject go = null;
                switch (PrefabUtility.GetPrefabType(poolable.GetGameObject))
                {
                    case PrefabType.Prefab:
                        go = PrefabUtility.FindPrefabRoot(poolable.GetGameObject) as GameObject;
                        break;
                    case PrefabType.PrefabInstance:
                        go = PrefabUtility.GetPrefabParent(poolable.GetGameObject) as GameObject;
                        break;

                        //скорее всего из Meshes(автоматические префабы)
                    case PrefabType.ModelPrefabInstance:
                        go = TryToFindAlreadyExistsPrefab(poolable.GetGameObject) as GameObject ?? 
                            PrefabUtility.CreatePrefab(
                            "Assets/Prefabs/System/AfterSerializationCreatedPrefabs/FromImportedMeshes/" + poolable.GetGameObject.name + ".prefab",
                            poolable.GetGameObject);
                        break;

                        
                    case PrefabType.MissingPrefabInstance:
                        go = TryToFindAlreadyExistsPrefab(poolable.GetGameObject) as GameObject ??  
                            PrefabUtility.CreatePrefab(
                            "Assets/Prefabs/System/AfterSerializationCreatedPrefabs/Missing/" + poolable.GetGameObject.name + "(missing).prefab",
                            poolable.GetGameObject);
                        break;
                     
                        //скорее всего добалены новые компоненты
                    case PrefabType.DisconnectedPrefabInstance:
                        PrefabUtility.CreatePrefab(
                            "Assets/Prefabs/System/AfterSerializationCreatedPrefabs/WithSomeChanges/" + poolable.GetGameObject.name + "(withChanges).prefab",
                            poolable.GetGameObject);
                        break;

                    default:
                        go = TryToFindAlreadyExistsPrefab(poolable.GetGameObject) as GameObject ??
                             PrefabUtility.CreatePrefab(
                            "Assets/Prefabs/System/AfterSerializationCreatedPrefabs/" + poolable.GetGameObject.name + ".prefab",
                            poolable.GetGameObject);
                        break;
                }
                return go;
                
            }
            else
                throw new ArgumentNullException("poolable");
        }

        private static Object TryToFindAlreadyExistsPrefab(GameObject gameObject)
        {
            string msg = string.Empty;
            MeshFilterDeepComparer comparer = new MeshFilterDeepComparer();
            List<StandartSerializator> alreadyExistsprefabs = Object.FindObjectsOfType<Component>().OfType<StandartSerializator>().ToList();
            foreach(StandartSerializator serializator in alreadyExistsprefabs)
            {                 
                try
                {
                  
                        if (comparer.Equals(serializator.prefab, gameObject))
                        {
                            return PrefabUtility.FindPrefabRoot(serializator.prefab);
                        }
                  
                }
                catch (Exception e)
                {
                    Debug.Log(serializator.GetType()+"; "+serializator+"; "+e.Message);
                }
            }
            msg += "; return null";
       //     Debug.Log(msg);
            return null;
        }
         
    }
#endif
}