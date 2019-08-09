using GamePlay;
using LevelGeneration;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PositioningByFloor)), System.Serializable]
public class PositioningByFloorEditor : PositioningEditor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        (target as PositioningByFloor).Floor = EditorGUILayout.Popup("Floor",
                                                                       (target as PositioningByFloor).Floor,
                                                                       HeightInfos.FloorsLiteralPresenation);

        if ( GUI.changed )
        {
            EditorUtility.SetDirty(target as PositioningByFloor);
        }
    }
}
