using UnityEngine;
using System.Collections;
using LevelGeneration;
using UnityEditor;

[CustomEditor(typeof(Positioning1stFixedHeightRope)), System.Serializable]
public class Positioning1stFixedHeightRopeEditor : PositioningFixedHeightRopeEditor
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}
}
