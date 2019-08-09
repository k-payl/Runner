using UnityEditor;
using UnityEngine;
using System;





[CustomPropertyDrawer(typeof(PopupAttribute))]
public class PopupDrawer : PropertyDrawer
{
	PopupAttribute popupAttribute { get { return ((PopupAttribute)attribute); } }
	int index;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		
		if (Equals(popupAttribute.variableType, typeof(int[])))
		{
			EditorGUI.BeginChangeCheck();
			index = EditorGUI.Popup(position, label.text, property.intValue, popupAttribute.list);
			if (EditorGUI.EndChangeCheck())
			{
				property.intValue = index;
			}
		}
		else if (Equals(popupAttribute.variableType, typeof(float[])))
		{
			EditorGUI.BeginChangeCheck();
			
			for (int i = 0; i < popupAttribute.list.Length; i++)
			{
				if (Math.Abs(property.floatValue - Convert.ToSingle(popupAttribute.list[i])) < float.Epsilon)
				{
					index = i;
				}
			}
			index = EditorGUI.Popup(position, label.text, index, popupAttribute.list);
			if (EditorGUI.EndChangeCheck())
			{
				property.floatValue = Convert.ToSingle(popupAttribute.list[index]);
			}
		}
		else if (Equals(popupAttribute.variableType, typeof(string[])))
		{
			EditorGUI.BeginChangeCheck();
			
			for (int i = 0; i < popupAttribute.list.Length; i++)
			{
				if (property.stringValue == popupAttribute.list[i])
				{
					index = i;
				}
			}
			index = EditorGUI.Popup(position, label.text, index, popupAttribute.list);
			if (EditorGUI.EndChangeCheck())
			{
				property.stringValue = popupAttribute.list[index];
			}
		}
		else
		{
			EditorGUI.LabelField(position, "ERROR READ CONSOLE FOR MORE INFO");
		}
	}
}