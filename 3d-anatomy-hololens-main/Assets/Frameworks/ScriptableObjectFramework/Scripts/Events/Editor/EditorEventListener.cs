using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace CrossComm.Framework.ScriptableObjects
{
	[CustomEditor(typeof(EventListener))]
	public class EditorEventListener : Editor
	{
		private ReorderableList list;
		private void OnEnable()
		{
			list = new ReorderableList(serializedObject, serializedObject.FindProperty("_eventChannels"), true, true, true, true);
			list.drawElementCallback = DrawListItems; // Delegate to draw the elements on the list
			list.drawHeaderCallback = DrawHeader;
			list.elementHeightCallback = ElementHeightCallback;
		}

		// Draws the elements on the list
		private void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
		{
			SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index); // The element in the list

			//Create a property field and label field for each property. 
			SerializedProperty eventChannelProperty = element.FindPropertyRelative("channel");

			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, rect.width, EditorGUIUtility.singleLineHeight),
				eventChannelProperty,
				GUIContent.none
			);

			if (eventChannelProperty.objectReferenceValue != null)
			{
				EditorGUI.LabelField
				(
					new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
					eventChannelProperty.objectReferenceValue.name
				);

				SerializedProperty unityEventProperty = element.FindPropertyRelative("OnEventRaised");

				EditorGUI.PropertyField(
					new Rect(rect.x, rect.y + (EditorGUIUtility.singleLineHeight * 2), rect.width, EditorGUI.GetPropertyHeight(unityEventProperty, true)),
					unityEventProperty,
					GUIContent.none,
					true
				);
			}
			else
			{
				EditorGUI.LabelField
				(
					new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
					"Add an Event Channel Here"
				);
			}
		}

		private float ElementHeightCallback(int index)
		{
			SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index); // The element in the list
			SerializedProperty eventChannelProperty = element.FindPropertyRelative("channel");
			SerializedProperty unityEventProperty = element.FindPropertyRelative("OnEventRaised");

			if (eventChannelProperty.objectReferenceValue != null)
			{
				return EditorGUI.GetPropertyHeight(eventChannelProperty, true) + EditorGUI.GetPropertyHeight(unityEventProperty, true) + 25;
			}
			else
			{
				return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(eventChannelProperty, true);
			}

		}

		private void DrawHeader(Rect rect)
		{

		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		}
	}
}