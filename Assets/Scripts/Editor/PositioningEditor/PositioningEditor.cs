using GamePlay;
using LevelGeneration;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Positioning)), System.Serializable]
    public class PositioningEditor : Editor
    {
    
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            (target as Positioning).LineNumber = EditorGUILayout.Popup("Line",
                                                                           (target as Positioning).LineNumber,
                                                                           Track3.LinesLiteralPresenation);

            if ( GUI.changed )
            {
                EditorUtility.SetDirty(target as Positioning);
            }
        }

    
       
    }
