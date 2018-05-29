using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LSystem2Unity{
	[System.Serializable]
	public class TurtleRule{
        public char Symbol;
		public UnityEvent Action;
		public TurtleRule(){}
		public TurtleRule(char _symbol, UnityEvent _event){
			this.Symbol = _symbol;
			this.Action = _event;
		}
		public bool isEqual(char _symbol) {
            return this.Symbol == _symbol;
        }
	}
}
