using UnityEngine;
using System.Collections;
using UnityEditor;
using LevelGeneration;

[CustomEditor(typeof(Rope)), System.Serializable]
public class RopeEditor : Editor {

    public override void OnInspectorGUI()
    {
        (target as Rope).Lenght = EditorGUILayout.FloatField("Lenght", (target as Rope).Lenght);
        if ( GUI.changed )
        {
            EditorUtility.SetDirty(target as Rope);
        }
    }

}
