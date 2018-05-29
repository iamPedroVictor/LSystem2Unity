using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LSystem2Unity{
    public class TurtleAgent : MonoBehaviour
    {
		public List<TurtleRule> rules = new List<TurtleRule>();
		public bool callTurtleInit;
		public UnityEvent startEvent;

		public void InvokeTurtle(string axiom){
			if(callTurtleInit){
				startEvent.Invoke();
			}
			for(int i = 0; i < axiom.Length; i++){
				InvokeRule(axiom[i]);
			}
		}

		private void InvokeRule(char Symbol){
			for(int i = 0; i < rules.Count; i++){
				if(rules[i].isEqual(Symbol)){
					rules[i].Action.Invoke();
				}
			}
		}

    }
}