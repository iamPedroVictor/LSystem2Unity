using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LSystem2Unity.Exemple{
[CustomEditor(typeof(FractalExemple))]
	public class FractalEditor : Editor {

		private FractalExemple fractalExemple;

		/// <summary>
		/// This function is called when the object becomes enabled and active.
		/// </summary>
		void OnEnable()
		{
			fractalExemple = (FractalExemple)target;
		}

		void OnSceneGUI(){
			Handles.color = Color.green;
			for(int i = 0; i < fractalExemple.posList.Count; i++){
				if(i != fractalExemple.posList.Count - 1){
					Handles.DrawLine(fractalExemple.posList[i], fractalExemple.posList[i+1]);
				}			
			}
		}

	}
}