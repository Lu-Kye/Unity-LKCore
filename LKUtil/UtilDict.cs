using System.Collections.Generic;

/// <summary>
/// Dictionary util
/// </summary>
public class UtilDict 
{
	/// <summary>
	/// Gets the values array of the input dictionary.
	/// </summary>
	/// <returns>The values array.</returns>
	/// <param name="dict">Dict.</param>
	/// <typeparam name="Tkey">The 1st type parameter.</typeparam>
	/// <typeparam name="Tval">The 2nd type parameter.</typeparam>
	public static Tval[] GetValuesArray<Tkey, Tval>(Dictionary<Tkey, Tval> dict)
	{
		var values = dict.Values;
		var valuesArray = new Tval[values.Count];
		values.CopyTo(valuesArray, 0);
		return valuesArray;
	}

	/// <summary>
	/// Gets the keys array of the input dictionary.
	/// </summary>
	/// <returns>The keys array.</returns>
	/// <param name="dict">Dict.</param>
	/// <typeparam name="Tkey">The 1st type parameter.</typeparam>
	/// <typeparam name="Tval">The 2nd type parameter.</typeparam>
	public static Tkey[] GetKeysArray<Tkey, Tval>(Dictionary<Tkey, Tval> dict)
	{
		var keys = dict.Keys;
		var keysArray = new Tkey[keys.Count];
		keys.CopyTo(keysArray, 0);
		return keysArray;
	}

	/// <summary>
	/// Gets the first key of the input dictionary.
	/// </summary>
	/// <returns>The first key.</returns>
	/// <param name="dict">Dict.</param>
	/// <typeparam name="Tkey">The 1st type parameter.</typeparam>
	/// <typeparam name="Tval">The 2nd type parameter.</typeparam>
	public static Tkey GetFirstKey<Tkey, Tval>(Dictionary<Tkey, Tval> dict)
	{
		var keys = dict.Keys;
		var keysArray = new Tkey[keys.Count];
		keys.CopyTo(keysArray, 0);
		return keysArray[0];
	}

	public static List<Tkey> GetFirstKeyList<Tkey, Tval>(List<Dictionary<Tkey, Tval>> list)
	{
		List<Tkey> keyList = new List<Tkey> ();
		foreach(var dict in list)
		{
			keyList.Add(GetFirstKey<Tkey,Tval>(dict));
		}
		return keyList;
	}

	/// <summary>
	/// Gets the first pair.
	/// </summary>
	/// <returns>The first pair.</returns>
	/// <param name="dict">Dict.</param>
	/// <typeparam name="Tkey">The 1st type parameter.</typeparam>
	/// <typeparam name="Tval">The 2nd type parameter.</typeparam>
	public static KeyValuePair<Tkey, Tval> GetFirstPair<Tkey, Tval>(Dictionary<Tkey, Tval> dict)
	{
		foreach(var d in dict)
		{
			return d;
		}
		return new KeyValuePair<Tkey, Tval>();
	}
}
