using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using LSystem2Unity;

namespace LSystem2Unity.ToolEditor {
	[CustomEditor(typeof(Ruleset))]
	public class RulesetEditor : Editor {

		private ReorderableList list;

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		void OnEnable()
		{
			list = new ReorderableList(serializedObject, serializedObject.FindProperty("rules"),
			false,true,true,true);

			list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
				var element = list.serializedProperty.GetArrayElementAtIndex(index);
				rect.y += 2;
				EditorGUI.PropertyField(
					new Rect(rect.x, rect.y, 30, EditorGUIUtility.singleLineHeight),element.FindPropertyRelative("symbol"), GUIContent.none);
				EditorGUI.PropertyField(
					new Rect(rect.x + 60, rect.y, rect.width - 60 - 30 , EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("replace"), GUIContent.none);
				
			};

			list.drawHeaderCallback = (Rect rect) => {
				EditorGUI.LabelField(rect, "Rules (char -> string)");
			};

		}

		public override void OnInspectorGUI(){
			serializedObject.Update();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		}

	}
}