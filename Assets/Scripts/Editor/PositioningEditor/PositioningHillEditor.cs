using GamePlay;
using LevelGeneration;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PositioningHill)), System.Serializable]
public class PositioningHillEditor : PositioningEditor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
