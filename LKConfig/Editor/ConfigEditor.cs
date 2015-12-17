using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class ConfigEditor 
{
	public const string JSON_STRUCTS_PATH = "Game/Vo";
	public const string CONFIG_PATH = "Scripts/LKCore/LKConfig/ConfigManagerAuto.cs";

	public static void GenConfigManagerAuto()
	{
		var assetPath = Application.dataPath;
		var jsonStructsPath = Path.Combine(assetPath, JSON_STRUCTS_PATH);
		var targetPath = Path.Combine(assetPath, CONFIG_PATH);

		var jsonStructFiles = Directory.GetFiles(jsonStructsPath, "*.*", SearchOption.TopDirectoryOnly)
			.Where(file => file.ToLower().EndsWith(".cs") && Regex.Match(Path.GetFileNameWithoutExtension(file), "^Sg_.*").Success)
			.ToList();

		var codes = "";
		codes += @"using Soul;
using UnityEngine;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Game.Soul.VO;

namespace Soul
{
	public class ConfigDataModelAuto
	{
		protected Dictionary<Type, object> typeToDict = new Dictionary<Type, object>();
";
/*		// fields
		for (int i = 0, max = jsonStructFiles.Count; i < max; i++)
		{
			var jsonStructFile = jsonStructFiles[i];
			var name = Path.GetFileNameWithoutExtension(jsonStructFile);
			var lowerName = name.ToLower();
			var firstUpperName = UtilString.ToCamelCase(lowerName);
			codes += string.Format("\t\tprivate Dictionary<string, {0}> {1};\n", name, lowerName);
			codes += string.Format("\t\tpublic Dictionary<string, {0}> {1}\n", name, firstUpperName);

			codes += "\t\t{\n";
			codes += "\t\t\tget { return this." + lowerName + "; } \n";
//			codes += string.Format("\t\t\tget { return this.{0}; }", lowerName);
			codes += "\t\t}\n\n";
		}
*/
		codes += @"
		public virtual string ReadFile(string fileName, bool ifCreate)
		{
			try 
			{
				string text = (ResourceManager.only.Load<TextAsset>(ResourceConfig.GetResourceInfo(fileName))).text;
				return text;
			}
			catch (Exception e)
			{
				Debug.Log(e.Message);
				return null;
			}
		}
";

		codes += @"
		public virtual void InitConfigData()
		{
			string json = string.Empty;
";
		// load jsons
		for (int i = 0, max = jsonStructFiles.Count; i < max; i++)
		{
			var jsonStructFile = jsonStructFiles[i];
			var name = Path.GetFileNameWithoutExtension(jsonStructFile);
			if (!name.StartsWith("Sg_")) 
				continue;
			var lowerName = name.ToLower();
			lowerName = lowerName;
			var firstUpperName = UtilString.ToCamelCase(lowerName);
			codes += string.Format("\t\t\tjson = ReadFile(\"JsonTable/{0}\", true);\n", lowerName);
			codes += string.Format("\t\t\tDictionary<string, {0}> {1} = JsonConvert.DeserializeObject<Dictionary<string, {2}>>(json);\n", name, lowerName, name);
			codes += string.Format("\t\t\ttypeToDict [typeof({0})] = {1};\n\n", name, lowerName);
		}

		codes += @"
		}
	}
}";


		if (File.Exists(targetPath)) {
			File.Delete(targetPath);
			AssetDatabase.Refresh();
		}

		var f = new StreamWriter(targetPath);
		f.Write(codes);
		f.Close();

		AssetDatabase.Refresh();
	}
}
