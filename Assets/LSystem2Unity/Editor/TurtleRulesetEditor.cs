using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using LSystem2Unity;
using UnityEngine.Events;
using System;
using System.Linq;


namespace LSystem2Unity.ToolEditor{
	[CustomEditor(typeof(TurtleAgent))]
	public class TurtleRulesetEditor : Editor {
		private ReorderableList list;
		private TurtleAgent turtleRuleset;

		private const string headerTemplate = "{0} {1}";
		private const string callTurtleInitLabel = "Call event before Turtle process";

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		void OnEnable()
		{

			turtleRuleset = (TurtleAgent)target;
			list = CreateList(serializedObject, serializedObject.FindProperty("rules"));
			
		}

		public override void OnInspectorGUI(){

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(callTurtleInitLabel);
			turtleRuleset.callTurtleInit = EditorGUILayout.Toggle(turtleRuleset.callTurtleInit);
			EditorGUILayout.EndHorizontal();

			if(turtleRuleset.callTurtleInit){
				EditorGUILayout.BeginVertical();
				var initEvent = serializedObject.FindProperty("startTurtle");
				EditorGUILayout.PropertyField(initEvent);
				EditorGUILayout.EndVertical();
			}

			serializedObject.Update();
			list.DoLayoutList();
			serializedObject.ApplyModifiedProperties();
		}

		private ReorderableList CreateList(SerializedObject obj, SerializedProperty prop){
			ReorderableList list = new ReorderableList (obj, prop, false, true, true, true);

			list.drawHeaderCallback = rect => {
                        EditorGUI.LabelField (rect, "Turtle Rules");
                };
 
            List<float> heights = new List<float> (prop.arraySize);
 
			list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
				var element = list.serializedProperty.GetArrayElementAtIndex(index);
				SerializedProperty s = element.FindPropertyRelative("Symbol");
				SerializedProperty c = element.FindPropertyRelative("Action");
				var elementSymbol = turtleRuleset.rules[index].Symbol;

				bool foldout = isActive;
				
				float height = EditorGUIUtility.singleLineHeight * 1.25f;
				//if(foldout){height = EditorGUIUtility.singleLineHeight * 5;}

				try{
					heights[index] = height;
				} catch(ArgumentOutOfRangeException e){
					Debug.LogWarning(e.Message);
				} finally{
					float[] floats = heights.ToArray();
					Array.Resize(ref floats, prop.arraySize);
					heights = floats.ToList();
				}
				

				float margin = height / 100;
				//rect.y += margin;
				rect.height = (height / 5) * 4;
				rect.width = rect.width /2 - margin/2;

				if(foldout){
					if(s != null && c != null){
						EditorGUILayout.PropertyField(s);
						EditorGUILayout.PropertyField(c);
					}
				}
				
				
				rect.x = 20;
				EditorGUI.LabelField(rect, string.Format(headerTemplate, s.displayName, elementSymbol.ToString()));

			};
			
			list.elementHeightCallback = (index) => {
				Repaint ();
				float height = 0;

				try {
					height = heights [index];
				} catch (ArgumentOutOfRangeException e) {
					Debug.LogWarning (e.Message);
				} finally {
					float[] floats = heights.ToArray ();
					Array.Resize (ref floats, prop.arraySize);
					heights = floats.ToList ();
				}

				return height;
			};

			list.onAddDropdownCallback = (rect, li) => {
				var menu = new GenericMenu ();
				menu.AddItem (new GUIContent ("Add Element"), false, () => {
					serializedObject.Update ();
					li.serializedProperty.arraySize++;
					serializedObject.ApplyModifiedProperties ();
				});

				menu.ShowAsContext ();

				float[] floats = heights.ToArray ();
				Array.Resize (ref floats, prop.arraySize);
				heights = floats.ToList ();
			};
 
			return list;

		}

	}
}