using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LevelGeneration
{
	public class Pool<T> where T : class, IPoolable, IEquatable<T> 
	{
		private readonly T defaultValue;
		private readonly List<T> insantiatedObjects; //TODO возможно стоит переделать под хэш корзины для быстрого поиска
		public int InstantiatedCount
		{
			get
			{
				if (insantiatedObjects != null) return insantiatedObjects.Count;
				else
					return 0;
			}
		}

		public int TotalGetCount { get; private set; }

		public Pool()
		{
			insantiatedObjects = new List<T>();
		}

		~Pool()
		{
			foreach (T o in insantiatedObjects)
			{
				Object.DestroyImmediate(o.GetGameObject);
			}
			insantiatedObjects.Clear();
		}

		/// <summary>
		/// Правильная очистка пула
		/// </summary>
		public void DestroyAllAndClear()
		{
			foreach (T o in insantiatedObjects)
			{
				Object.Destroy(o.GetGameObject);
			}
			insantiatedObjects.Clear();
		}


		/// <summary>
		/// Создает новый экземпляр пула для объектов реализующих интерфейс T
		/// </summary>
		/// <param name="defaultValue">Значение по умолчанию, которое вернет GetObject()  (в т.ч может вернуть и null)</param>
		public Pool(T defaultValue)
		{
			insantiatedObjects = new List<T>();
			this.defaultValue = defaultValue;
			if(null != this.defaultValue)
			{
				insantiatedObjects.Add(this.defaultValue);
			}
		}

		public void InstantiateAndAdd(T prototype, int countInstnces = 1)
		{
			for (int i = 0; i < countInstnces; i++)
			{
				CreateAndAddObject(prototype);
			}

		}
		
		public T GetDefaultValue()
		{
			if(defaultValue!=null)
				return GetObject(defaultValue);
			else
			{
				throw new ArgumentNullException("defaultValue in this Pool is null");
			}
		}
		public T GetObject(T prototype)
		{
			if(prototype == null)
				throw new ArgumentNullException("prototype");
			TotalGetCount++;
			return FindInInstantiated(prototype);
		}

		private T FindInInstantiated(T prototype)
		{
			
			T founded;
			founded = insantiatedObjects.Find(t => t != null && (!t.IsUsedNow && t.Equals(prototype)));
			
			 //int i = 0;
			 //bool found = false;
			 //while (!found && i < insantiatedObjects.Count)
			 //{
			 //	found = !insantiatedObjects[i].IsUsedNow && insantiatedObjects[i].Equals(prototype);
			 //	Debug.Log("!IsUsedNow=" + !insantiatedObjects[i].IsUsedNow + "t.Equals(prototype)=" + insantiatedObjects[i].Equals(prototype));
			 //	Debug.Log("GetInstanceID:" + insantiatedObjects[i].GetGameObject.GetInstanceID());
			 //	i++;
			 //}
			 //if ( found )
			 //{
			 //	founded = insantiatedObjects[i - 1];
			 //	Debug.Log("Instance is found");
			 //	TotalGetCount++;
			 //}
			 //else
			 //{
			 //	Debug.Log("Instance isn't found by prototype:" + " prototype=" + prototype);
			 //	founded = CreateObject(prototype); 
			 //   }
			if (founded != null)
			{
					TotalGetCount++;
			}
			else
			{
				   founded = CreateAndAddObject(prototype); 
			}
		   
			return founded;
		}

		private T CreateAndAddObject(T prototype)
		{
			if (prototype != null)
			{
				GameObject newObj = Object.Instantiate(prototype.GetGameObject) as GameObject;
				T t = newObj.GetComponent(typeof(T)) as T;
				insantiatedObjects.Add(t);
				t.ResetState();
				return t;
			}
			throw new ArgumentNullException("prototype");
		}

		public string Info()
		{
			string msg = "Objects In Pool:\n";
			foreach (T insantiatedObject in insantiatedObjects)
			{
				msg += (insantiatedObject.GetGameObject.name + '\n');
			}
			msg += '\n';
			return msg;
		}
	}
}