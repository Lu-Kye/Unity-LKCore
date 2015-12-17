using UnityEngine;
using System.Text;

public class UtilVector3
{
	/// <summary>
	/// Tos the string.
	/// - Example :
	/// - vect   (1.0, 1.0, 1.0)
	/// - return "1.0,1.0,1.0"
	/// </summary>
	/// <returns>The string.</returns>
	/// <param name="vect">Vect.</param>
	public static string ToString(Vector3 vect)
	{
		var sb = new StringBuilder();
		sb.Append(vect.x);
		sb.Append(',');
		sb.Append(vect.y);
		sb.Append(',');
		sb.Append(vect.z);
		return sb.ToString();
	}
}
