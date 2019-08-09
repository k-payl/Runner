using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using LevelGeneration;
using Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;

[Serializable]
public class PrefabItem :  IEquatable<PrefabItem>
{
	public PrefabItem(GameObject o, int c)
	{
		prefab = o;
		count = c;
	}
	public GameObject prefab;
	public int count;
	public bool Equals(PrefabItem other)
	{
		return (this == other);
	}
}

/// <summary>
/// Хранит в сериализованном виде информацию о том какие префабы и сколько нужно создавать при старте
/// </summary>
[ExecuteInEditMode] 
public class PrefabInitializationManager : MonoBehaviour
{
	[SerializeField]private List<PrefabItem> prefabItems;
	public List<PrefabItem> PrefabItems
	{
		get { return prefabItems; }
	} 
#if UNITY_EDITOR

	
	[ContextMenu("CalculateWithtrackChunkManager")]
	public void CalculateForContextMenu()
	{
		Caluculate(TrackChunkManager.GetInstance());
	}
	//перебор всех позиций и оптимальное заполнение prefabItems
	public void Caluculate(TrackChunkManager trackChunkManager)
	{
		prefabItems = new List<PrefabItem>();
		if (trackChunkManager.trackChuncks!=null)
		{
			int head = Mathf.Max(1, trackChunkManager.NumberOfActiveChunksAhead);
			//Debug.Log("head = "+head);
			for (int j = 0; j < (trackChunkManager.trackChuncks.Count - head -1);j++)
			{
				int tail = trackChunkManager.NumberOfActiveChunksBehind;
				if (j < tail) tail = j;
				
				//формируем List префабов в игровом диапазоне для конкретного положения игрока в чанке j
				List<PrefabItem> PrefabItemsToInclude = new List<PrefabItem>();
				List<StandartSerializator> serializators = new List<StandartSerializator>();
				for (int i = j - tail; i < j + head + 1; i++)
				{
				 
						serializators = serializators.Concat(trackChunkManager.trackChuncks[i].GetComponents<StandartSerializator>()).ToList();
					
				}
				foreach (StandartSerializator s in serializators)
				{
					AddPrefabToItems(PrefabItemsToInclude, s.prefab);
				}


				//Debug
				string s1 = "";
				s1 += "PrefabInitializationManager: Чанки с " + (j - tail) + " до " + (j + head) + "(Центральный=" + j + ") . Количество добавленных префабов: " + PrefabItemsToInclude.Count + "\n";
				s1 += "Все префабы:\n";
				foreach (PrefabItem item in PrefabItemsToInclude)
				{
					s1 += "prefab :" + item.prefab.name + ". count:" + item.count + "\n";
				}
				s1 += "До PrefabItems.Count=" + PrefabItems.Count;


				//для каждого элемента из сформированного List проверяем макисмальено ли его число уже в prefabItems
				foreach (PrefabItem prefabtoInclude in PrefabItemsToInclude)
				{
					//сравниваем с каждым уже содержащимся префабом
					bool needToAdd = true;
					foreach (PrefabItem original in prefabItems)
					{
						//если ссылки префабов совпадают и количество оказалось больше, 
						//то обновляем инфу в prefabItems и выходим
						if (PrefabUtility.FindPrefabRoot(prefabtoInclude.prefab) ==
							PrefabUtility.FindPrefabRoot(original.prefab) && 
							(PrefabUtility.FindPrefabRoot(original.prefab)!=null) &&
							(PrefabUtility.FindPrefabRoot(prefabtoInclude.prefab)!=null))
						{
							needToAdd = false;
							if (prefabtoInclude.count > original.count)
							{
								original.count = prefabtoInclude.count;
							}
						}
					}
					//если в prefabItems еще нет такого префаба, то вставляем с новой инфой о количестве
					if (needToAdd)
						prefabItems.Add(prefabtoInclude);

				}

				s1 += ".\t\tПосле PrefabItems.Count=" + PrefabItems.Count + "\n";
				Debug.Log(s1);

			}
		}
		EditorUtility.SetDirty(this);
	}

	public void Reset()
	{
		prefabItems = null;
	}

	/// <summary>
	/// Если существует уже GameObject то инкрементируем количество
	/// Иначе добовляем этот GameObject в количестве 1
	/// </summary>
	/// <param name="prefabItems">Коллекиция пар GameObject - Count</param>
	/// <param name="objectToInclude">Вставляемый объект</param>
	private void AddPrefabToItems(List<PrefabItem> collection, GameObject objectToInclude)
	{
		bool updated = false;
		foreach (PrefabItem o in collection)
		{
			if (objectToInclude == o.prefab)
			{
				updated = true;
				o.count++;
				break;
			}
		}
		if (!updated)
		{
			PrefabItem other = new PrefabItem(objectToInclude, 1); 
			collection.Add(other);
		}
	}



#endif
	void OnEnable(){if (prefabItems==null) prefabItems = new List<PrefabItem>();}
	void OnDisable(){prefabItems = null;}

}
