using UnityEngine;
using System.Collections;

public class DigraphBase
{
	public IDigraphData Data;

	public DigraphBase(IDigraphData data)
	{
		this.Data = data;
	}
}
