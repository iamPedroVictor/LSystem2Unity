using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSystem2Unity{
	[CreateAssetMenu(menuName = "LSystem/Ruleset")]
    public class Ruleset : ScriptableObject
    {
        public List<LRule> rules = new List<LRule>();
        public void addRule(char symbol, string replace)
        {
            LRule rule = new LRule();
            rule.symbol = symbol;
            rule.replace = replace;
            rules.Add(rule);
        }
        public void removeRule(char symbol)
        {
            for (int i = 0; i < rules.Count; i++)
            {
                if (rules[i].isEqual(symbol)) { 
                    rules.RemoveAt(i); 
                }
            }
        }
        public string replaceElement(char symbol)
        {
            for(int i = 0; i < rules.Count; i++)
            {
            if (rules[i].isEqual(symbol)) { 
                return rules[i].replace; 
                }
            }
            return symbol.ToString();
        }
    }
}
