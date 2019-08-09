using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Rotator)), System.Serializable]
public class RotatorEditor : Editor {

	

	public override void OnInspectorGUI()
	{
	   // DrawDefaultInspector();
		(target as Rotator).axis = EditorGUILayout.Popup("Axis",(target as Rotator).axis,(target as Rotator).strDirs);
		(target as Rotator).period = EditorGUILayout.FloatField("Period", (target as Rotator).period);
		(target as Rotator).Direction = EditorGUILayout.Popup("Direction", (target as Rotator).Direction, (target as Rotator).strSides);

		(target as Rotator).randomize = EditorGUILayout.Toggle("Randomize", (target as Rotator).randomize);
		if (GUI.changed)
		{
			EditorUtility.SetDirty((target as Rotator));
		}
	}
}
