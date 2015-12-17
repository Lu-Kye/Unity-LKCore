using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class ConfigEditor 
{
	public const string JSON_STRUCTS_PATH = "Scripts/Game/Vo";
	public const string CONFIG_PATH = "Scripts/LKCore/LKConfig/ConfigManagerAuto.cs";

	public static void GenConfigManagerAuto()
	{
		// Pathes
		var assetPath = Application.dataPath;
		var jsonStructsPath = Path.Combine(assetPath, JSON_STRUCTS_PATH);
		var targetPath = Path.Combine(assetPath, CONFIG_PATH);

		var codes = "";
		codes += @"using UnityEngine;
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
";

		codes += @"
	public virtual void InitConfigData()
	{
		string json = string.Empty;
";
		// Get files
		var jsonStructFiles = Directory.GetFiles(jsonStructsPath, "*.*", SearchOption.TopDirectoryOnly)
			.Where(file => file.ToLower().EndsWith(".cs") && Regex.Match(Path.GetFileNameWithoutExtension(file), "^Vo_.*").Success)
			.ToList();

		// Load jsons
		for (int i = 0, max = jsonStructFiles.Count; i < max; i++)
		{
			var jsonStructFile = jsonStructFiles[i];
			var name = Path.GetFileNameWithoutExtension(jsonStructFile);
			if (!name.StartsWith("Vo_")) 
				continue;
			var lowerName = name.ToLower();
			lowerName = lowerName;
			var firstUpperName = UtilString.ToCamelCase(lowerName);
			codes += string.Format("\t\tjson = ReadFile(\"Jsons/{0}\");\n", lowerName);
			codes += string.Format("\t\tDictionary<string, {0}> {1} = JsonConvert.DeserializeObject<Dictionary<string, {2}>>(json);\n", name, lowerName, name);
			codes += string.Format("\t\tdictObject[typeof({0})] = {1};\n", name, lowerName);

			if (i != max - 1)
				codes += "\n";
		}

		codes += @"
	}
}";
		// Delete first
		if (File.Exists(targetPath)) 
		{
			File.Delete(targetPath);
			AssetDatabase.Refresh();
		}

		// Write
		var f = new StreamWriter(targetPath);
		f.Write(codes);
		f.Close();

		AssetDatabase.Refresh();
	}
}
