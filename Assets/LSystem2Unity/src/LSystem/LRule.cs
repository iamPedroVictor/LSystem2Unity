using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSystem2Unity{
    [System.Serializable]
    public class LRule {

        public char symbol;
        public string replace;
        
        public bool isEqual(char _symbol) {
            return this.symbol == _symbol;
        }
    }
}