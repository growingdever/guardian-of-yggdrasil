using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Util {

	static public int UpdateValueByTable(int[] table, int level) {
		return table [level < table.Length ? level : table.Length - 1];
	}

	static public float UpdateValueByTable(float[] table, int level) {
		return table [level < table.Length ? level : table.Length - 1];
	}

}
