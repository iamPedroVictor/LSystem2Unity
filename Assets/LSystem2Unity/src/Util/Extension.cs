using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace LSystem2Unity{
	public static class Extension{
		public static void Clear(this StringBuilder sb){
			sb.Remove(0, sb.Length);
			sb.Length = 0;
			sb.Capacity = 0;
		}
	}
}