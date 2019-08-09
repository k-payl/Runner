using GamePlay;
using LevelGeneration;
using Serialization;
using UnityEngine;
using UnityEditor;

public class PrepareLevel : EditorWindow
{
	[SerializeField] private bool IsSerialized;

	[MenuItem ("Window/Save level")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		GetWindow<PrepareLevel>(false, "Save level");
	}

	private Vector2 scroll = Vector2.zero;

	void OnEnable()
	{
		IsSerialized = LevelConfiguration.Instance.IsSerializedLevel;
	}

	void OnGUI ()
	{
		scroll = GUILayout.BeginScrollView(scroll);
	 
		GUILayout.BeginHorizontal();
		GUILayout.Toggle(IsSerialized, "Level is saved");  
		
			if (IsSerialized)
			{
				EditorGUILayout.BeginVertical();
				if (GUILayout.Button("Restore level"))
				{
					IsSerialized = false;
					TrackChunkManager.GetInstance().DeserializeFromChunks();
					TrackChunkManager.GetInstance().PutAllPoolablesToBlocks();
					if (GameManager.Instance.PrefabInitializationManager) 
					   GameManager.Instance.PrefabInitializationManager.Reset();
					LevelConfiguration.Instance.IsSerializedLevel = false;
				}

				EditorGUILayout.EndVertical();
			}
			else
			{
				if (GUILayout.Button("Save level")) 
				{
					IsSerialized = true;
					TrackChunkManager.GetInstance().GenerateChunks();
					TrackChunkManager.GetInstance().DestroyAllBlocks();
					PrefabInitializationManager prefabManager = FindObjectOfType<PrefabInitializationManager>();
					if (prefabManager)
						prefabManager.Caluculate(TrackChunkManager.GetInstance());
					LevelConfiguration.Instance.IsSerializedLevel = true;

				}
			}
		GUILayout.EndHorizontal();
		GUILayout.EndScrollView();									

	}

	void GenerateBlocks()
	{
		
	}
	
	
}


