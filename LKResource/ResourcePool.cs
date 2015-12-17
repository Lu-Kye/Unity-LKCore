using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Resource cache pool
/// </summary>
public class ResourcePool<T1, T2>
	where T2 : class
{
	private bool infinity = false;
	public bool Infinity
	{
		set { this.infinity = value; }
	}

	private int maxLength = 200;
	public int MaxLength 
	{
		set { this.maxLength = value; }
	}

	private Dictionary<T1, T2> dict = new Dictionary<T1, T2>();

	/// <summary>
	/// Contains the specified key.
	/// </summary>
	/// <param name="key">Key.</param>
	public bool Contains(T1 key)
	{
		return this.dict.ContainsKey(key);
	}
	
	/// <summary>
	/// Get the specified key value.
	/// </summary>
	/// <param name="key">Key.</param>
	public T2 Get(T1 key)
	{
		if (!this.Contains(key))
			return null;

		return this.dict[key];
	}

	/// <summary>
	/// Add the specified key, val and force.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="val">Value.</param>
	/// <param name="force">If set to <c>true</c> force replace the old one.</param>
	public void Add(T1 key, T2 val, bool force = false)
	{
		if (this.Contains(key) && force)
		{
			this.dict[key] = val;
			return;
		}

		if (!this.infinity && this.dict.Count > this.maxLength) 
		{
			var removeKey = UtilDict.GetFirstKey<T1, T2>(dict);
			this.Remove(removeKey);
		}

		this.dict[key] = val;
	}

	/// <summary>
	/// Remove the specified key value.
	/// </summary>
	/// <param name="key">Key.</param>
	public T2 Remove(T1 key)
	{
		if (!this.Contains(key))
			return null;

		var val = this.dict[key];
		this.dict.Remove(key);
		return val;
	}

	/// <summary>
	/// Foreach this pool
	/// </summary>
	/// <param name="action">Action.</param>
	public void Iter(UnityAction<T1, T2> action)
	{
		var keys = UtilDict.GetKeysArray<T1, T2>(this.dict);
		for (int i = 0, max = keys.Length; i < max; i++)
		{
			var key = keys[i];
			if (action != null)
				action(key, this.dict[key]);
		}
	}

	/// <summary>
	/// Clear all resources in pool
	/// </summary>
	public void Clear()
	{
		this.dict.Clear();
	}
}
