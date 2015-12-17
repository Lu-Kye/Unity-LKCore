/// <summary>
/// ************************************************************
/// ------------------This file is auto generated --------------
/// ------------------Plz dont edit!----------------------------
/// ************************************************************
/// </summary>
using Soul;
using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class ConfigDataModelAuto
{
	protected Dictionary<Type, object> typeToDict = new Dictionary<Type, object>();

	public virtual string ReadFile(string fileName, bool ifCreate)
	{
		try 
		{
//				string text = (ResourceManager.only.Load<TextAsset>(ResourceConfig.GetResourceInfo(fileName))).text;
//				return text;
			return "";
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
			return null;
		}
	}

	public List<T> GetElementList<T>(Predicate<T> where = null)
	{
		List<T> list = new List<T> ();
		Type t = typeof(T);
		object dict;
		if (typeToDict.TryGetValue(t, out dict))
		{
			Dictionary<string,T> table = dict as Dictionary<string,T>;
			if (where == null)
			{
				T[] elemts = new T[table.Values.Count];
				table.Values.CopyTo(elemts, 0);
				list = new List<T>(elemts);
			}
			else
			{
				list = new List<T>();
				foreach(var kv in table)
				{
					if (where(kv.Value))
						list.Add(kv.Value);
				}
			}
		}
		return list;
	}


	public virtual void InitConfigData()
	{
	}
}
