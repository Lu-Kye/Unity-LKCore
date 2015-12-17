using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;

public class ConfigManagerAuto
{
	protected Dictionary<Type, object> dictObject = new Dictionary<Type, object>();

	protected virtual string ReadFile(string fileName)
	{
		return (ResourceManager.Instance.Load<TextAsset>(ResourceConfig.GetResourceInfo(fileName))).text;
	}

	public List<T> GetElementList<T>(Predicate<T> where = null)
	{
		List<T> list = new List<T> ();
		Type t = typeof(T);
		object dict;
		if (dictObject.TryGetValue(t, out dict))
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

	public virtual void Init()
	{
		string json = string.Empty;
		json = ReadFile("Jsons/vo_traffic_edge");
		Dictionary<string, Vo_Traffic_Edge> vo_traffic_edge = JsonConvert.DeserializeObject<Dictionary<string, Vo_Traffic_Edge>>(json);
		dictObject[typeof(Vo_Traffic_Edge)] = vo_traffic_edge;

		json = ReadFile("Jsons/vo_traffic_node");
		Dictionary<string, Vo_Traffic_Node> vo_traffic_node = JsonConvert.DeserializeObject<Dictionary<string, Vo_Traffic_Node>>(json);
		dictObject[typeof(Vo_Traffic_Node)] = vo_traffic_node;

		json = ReadFile("Jsons/vo_traffic_role");
		Dictionary<string, Vo_Traffic_Role> vo_traffic_role = JsonConvert.DeserializeObject<Dictionary<string, Vo_Traffic_Role>>(json);
		dictObject[typeof(Vo_Traffic_Role)] = vo_traffic_role;

		json = ReadFile("Jsons/vo_traffic_role_spawn");
		Dictionary<string, Vo_Traffic_Role_Spawn> vo_traffic_role_spawn = JsonConvert.DeserializeObject<Dictionary<string, Vo_Traffic_Role_Spawn>>(json);
		dictObject[typeof(Vo_Traffic_Role_Spawn)] = vo_traffic_role_spawn;

	}
}