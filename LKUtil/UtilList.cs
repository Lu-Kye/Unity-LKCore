using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UtilList 
{
	public static T Rand<T>(IList<T> list)
	{
		var index = Random.Range(0, list.Count);
		return list[index];
	}
}
