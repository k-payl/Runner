using System.Linq;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

namespace GamePlay
{
	[System.Serializable]
	public class GameInfo
	{
		public BonusCollection bonuses;
		public int completedLevels;
		public dfLanguageCode lang;

		public GameInfo()
		{
			bonuses = new BonusCollection();
			completedLevels = 0;
			lang = dfLanguageCode.EN;
		}

		public void Reset()
		{
			bonuses = new BonusCollection();
			completedLevels = 0;
			lang = dfLanguageCode.EN;
			Debug.Log("info reseted");
			GameManager.Instance.CollectionInitializedEvent(bonuses);
		}
		  
		public void LevelFinished(bool success)
		{
			bonuses.LevelFinished(success);
		}

		
		
		
		public void Save()
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
			bf.Serialize(file, this);
			Debug.Log("Saved to file \'" + Application.persistentDataPath + "/playerInfo.dat\'");
			file.Close();
		}

		public void Load()
		{
			
			if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
			{
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
				try
				{
					GameInfo i = (GameInfo) bf.Deserialize(file);
					Debug.Log("Loaded \'" + Application.persistentDataPath + "/playerInfo.dat\'");
					

					i.bonuses.Load();
					bonuses = i.bonuses;
					
					GameManager.Instance.CollectionInitializedEvent(bonuses);

					completedLevels = i.completedLevels;
					lang = i.lang;
				}
				catch (Exception)
				{
					Debug.Log("Created new info. (Catched exception in Deserialize())");
					Reset();
				}

				file.Close();
			}
			else
			{
				Debug.Log("File \'" + Application.persistentDataPath + "/playerInfo.dat\' isn't exist");
				Reset();
			}
		}
	}
}

