using UnityEngine;
using System.Collections;
using LevelGeneration;
using UnityEditor;

[CustomEditor(typeof(PositioningFixedHeightRope)), System.Serializable]
public class PositioningFixedHeightRopeEditor : PositioningEditor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}
}
