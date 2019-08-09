using UnityEditor;
using GamePlay;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{

	//private bool status = true;
	//private string titile = "Bonus Collection";

	public override void OnInspectorGUI()
	{ //TODO че за херня? не работает
		DrawDefaultInspector();
		//EditorGUILayout.Space();
		//if ((target as GameManager).info.bonuses!=null)
		//{
		//	EditorGUILayout.IntField("score", (target as GameManager).info.bonuses.score);
		//	EditorGUILayout.FloatField("battery Charge", (target as GameManager).info.bonuses.BatareyCharge);
		//	EditorGUILayout.TextField("hasHalfBattery", (target as GameManager).info.bonuses.hasHalfBattery ? "yes" : "no");
		//	EditorGUILayout.TextField("hasMemoryСard", (target as GameManager).info.bonuses.hasMemoryCard ? "yes" : "no");
		//	EditorGUILayout.TextField("hasCoinMultiplier",
		//		(target as GameManager).info.bonuses.hasCoinMultiplier ? "yes" : "no");

		//	EditorUtility.SetDirty(target);
		//}

	}
}		   



