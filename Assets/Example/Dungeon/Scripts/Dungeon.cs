using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSystem2Unity;

namespace LSystem2Unity.Exemple{

	[System.Serializable]
	public enum BlockType{
		Corredor = 0,
		T = 1,
		Quarto = 2
	}
	[System.Serializable]
	public struct Point{
		public Vector3 pos;
		public Quaternion rot;
	}
	public class Dungeon : MonoBehaviour {
		
		public GameObject CorredorPrefab, QuartoPrefab, TPrefab;
		public Point savePoint;
		public float angle = 90;
		public float distance = 1;

		public LSystem lsystem;

		private GameObject block;
		private GameObject DungeonGameObject;

		private const float checkRadius = 0.2f;

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
			DungeonGameObject = new GameObject();
			DungeonGameObject.name = "Dungeon";
		}

		/// <summary>
		/// Start is called on the frame when a script is enabled just before
		/// any of the Update methods is called the first time.
		/// </summary>
		void Start()
		{
			if(lsystem){
				lsystem.StartGeneration();
			}
		}

		public void InstantiateBlock(int blockType){
			BlockType type = (BlockType)blockType;

			bool canInstantiate = isPosEmpty();
			switch (type){
				case BlockType.Corredor:
					block = Instantiate(CorredorPrefab, this.transform.position, this.transform.rotation);
					Move();
					break;
				case BlockType.Quarto: 
					block = Instantiate(QuartoPrefab, this.transform.position, this.transform.rotation);
					Move();
					break;
				case BlockType.T: 
					block = Instantiate(TPrefab, this.transform.position, this.transform.rotation);
					Move();
					break;
			}
			block.transform.SetParent(DungeonGameObject.transform);
			if(!canInstantiate){
				GameObject.Destroy(block);
			}
			
		}


		private bool isPosEmpty(){
			var listPosition = Physics2D.OverlapCircleAll(transform.position, checkRadius);
			return listPosition.Length == 0;
		}

		public void SavePoint(){
			savePoint.pos = this.transform.localPosition;
			savePoint.rot = this.transform.rotation;
		}

		public Point GetPoint(){
			return savePoint;
		}

		public void Move(){
			transform.Translate(0,distance,0);
		}

		public void MoveToPoint(){
			transform.position = savePoint.pos;
			transform.rotation = savePoint.rot;
		}

		public void Rotate(int direction){
			transform.Rotate(0,0,(angle * direction));
		}

	}
}