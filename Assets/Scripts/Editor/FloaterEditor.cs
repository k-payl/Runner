using UnityEngine;
using System.Collections;
using UnityEditor;
using LevelGeneration;

[CustomEditor(typeof(Floater)), System.Serializable]
public class FloaterEditor : Editor
{

	public override void OnInspectorGUI()
	{
		//DrawDefaultInspector();
		(target as Floater).halfAmplitude = EditorGUILayout.FloatField("Derivation", (target as Floater).halfAmplitude);
		(target as Floater).period = EditorGUILayout.FloatField("Period", (target as Floater).period);
		(target as Floater).randomInitPosition = EditorGUILayout.Toggle("Randomize", (target as Floater).randomInitPosition);
		if ( GUI.changed )
		{
			EditorUtility.SetDirty(target as Floater);
		}
	}
}
