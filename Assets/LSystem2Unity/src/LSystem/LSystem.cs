using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;
using System;

namespace LSystem2Unity
{
	public class LSystem : MonoBehaviour {

		public string startString;
		private string currentAxiom;
		private int generation;
		public int maxGeneration;
		public Ruleset rules;
		public float timeBetweenGeneration;
		private WaitForSeconds waitGeneration;
        public bool callTurtleForeachGeneration = false;
        private List<TurtleAgent> turtleAgents = new List<TurtleAgent>();
		private StringBuilder stringBuilder;

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// </summary>
		void Awake()
		{
			currentAxiom = startString;
			turtleAgents = GameObject.FindObjectsOfType<TurtleAgent>().ToList();
			stringBuilder = new StringBuilder();
		}

		public void StartGeneration(){
			StartCoroutine(Generation());
		}

		private IEnumerator Generation(){
			generation = 0;
			waitGeneration = new WaitForSeconds(timeBetweenGeneration);
			while(generation < maxGeneration){
				currentAxiom = NextGen(currentAxiom);
				yield return waitGeneration;
				generation++;
				if(callTurtleForeachGeneration) CallTurtleAgent(currentAxiom);
			}
			CallTurtleAgent(currentAxiom);
		}

        private void CallTurtleAgent(string finalAxiom)
        {
			if(turtleAgents.Count == 0) return;
			for(int i = 0; i < turtleAgents.Count; i++){
				turtleAgents[i].InvokeTurtle(finalAxiom);
			}
        }

		public void AddTurtleAgent(TurtleAgent t){
			turtleAgents.Add(t);
		}

        private string NextGen(string axiomBase)
        {
			stringBuilder.Clear();
			for(int i = 0; i < axiomBase.Length; i++){
				stringBuilder.Append(rules.replaceElement(axiomBase[i]));
			}
            return stringBuilder.ToString();
        }
    }
}
