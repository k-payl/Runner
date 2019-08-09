using UnityEngine;
using System.Collections;
using LevelGeneration;
using UnityEditor;

[CustomEditor(typeof(Positioning2ndFixedHeightRope)), System.Serializable]
public class Positioning2ndFixedHeightRopeEditor : PositioningFixedHeightRopeEditor
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}
}
