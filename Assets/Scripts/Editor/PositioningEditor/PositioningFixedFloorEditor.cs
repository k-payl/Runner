using GamePlay;
using LevelGeneration;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PositioningFixedFloor)), System.Serializable]
public class PositioningFixedFloorEditor : PositioningEditor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if ( GUI.changed )
		{
			EditorUtility.SetDirty(target as PositioningFixedFloor);
		}
	}
}
