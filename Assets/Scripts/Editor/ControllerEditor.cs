using GamePlay;
using Gameplay;
using UnityEditor;

[CustomEditor(typeof(Controller)), System.Serializable]
public class ControllerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUILayout.TextArea((Controller.SavedStates == null) ? "null" : (string) Controller.SavedStates.ToString());
        
    }
}
