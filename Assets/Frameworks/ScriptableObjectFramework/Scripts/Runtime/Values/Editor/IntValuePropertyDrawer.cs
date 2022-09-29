using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CrossComm.Framework.ScriptableObjects
{
	[CustomPropertyDrawer(typeof(IntValueSO))]
	public class IntValuePropertyDrawer : PropertyDrawer
	{
		private bool _showSo;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var buttonRect = new Rect(position.x - 20, position.y, 20, 20);

			if (GUI.Button(buttonRect, "S"))
			{
				_showSo = !_showSo;
				SetProperty(property, _showSo);
			}

			EditorGUI.PropertyField(position, property, GUIContent.none);
			EditorGUI.EndProperty();
		}

		private void SetProperty(SerializedProperty i_property, bool i_showSo)
		{
			//var propRelative = property.FindPropertyRelative("UseConstant");
			//propRelative.boolValue = value;
			//property.serializedObject.ApplyModifiedProperties();
		}
	}
}