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
		
		
		private float lineWidthValue;
		private SpawnPoint spawnPoint;
		private Track3 _track3;
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			
			
			
			
			
			

			
			
		  
		}

	   
	}
}
