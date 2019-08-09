using GamePlay;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace LevelGeneration
{
	[CustomEditor(typeof(Track3))]
	public class TrackEditor : Editor
	{
		// Implement this function to make a custom
		// inspector.
		private float lineWidthValue;
		private SpawnPoint spawnPoint;
		private Track3 _track3;
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			//if(track == null)
			//{
			//	track = target as Track;
			//}
			//lineWidthValue = track.LineWidth;
			//track.LineWidth = EditorGUILayout.FloatField("Line Width", lineWidthValue);

			//spawnPoint = track.SpawnPoint;
			//track.SpawnPoint = EditorGUILayout.ObjectField("Spawn Point", spawnPoint, typeof(SpawnPoint),true) as SpawnPoint;
		  
		}

	   
	}
}
