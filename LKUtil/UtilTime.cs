using UnityEngine;
using System.Collections;
using System;

public class UtilTime
{
	/// <summary>
	/// Unixtime to string "MM:SS"
	/// </summary>
	/// <returns>The str.</returns>
	public static string Tommss(long unix, char separater = ':')
	{
		var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
		dateTime = dateTime.AddSeconds(unix);
		return dateTime.ToString("mm" + separater + "ss");
	}
}
