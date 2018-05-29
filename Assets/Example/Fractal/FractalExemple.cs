using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSystem2Unity;

namespace LSystem2Unity.Exemple{
	public class FractalExemple : MonoBehaviour {
		public LSystem lsystem;
		public float angle;
		public float distanceToMove;
		
		[HideInInspector]
		public List<Vector3> posList = new List<Vector3>();
		[HideInInspector]
		public List<Quaternion> rotList = new List<Quaternion>();

		private Vector3 originalPos;
		private Quaternion originalRot;
		
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
			originalPos = transform.position;
			originalRot = transform.rotation;
		}

		/// <summary>
		/// Start is called on the frame when a script is enabled just before
		/// any of the Update methods is called the first time.
		/// </summary>
		void Start()
		{
			lsystem.StartGeneration();
		}

		public void AddPoint(){
			posList.Add(transform.position);
		}

		public void Rotate(int direction)
		{
			transform.Rotate(0,0, (angle * direction));
		}

		public void MoveForward(){
			transform.Translate(0, distanceToMove, 0);
			AddPoint();
		}

		public void CleanList(){
			transform.position = originalPos;
			transform.rotation = originalRot;
			posList.Clear();
			rotList.Clear();
		}
	}
}