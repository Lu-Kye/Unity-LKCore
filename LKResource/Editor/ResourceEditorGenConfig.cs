using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

public partial class ResourceEditorGenConfig 
{
	// resources version
	public static int Version 
	{
		get 
		{
			return 0;
		}
	}

	// need generate resources extensions
	public static string[] EXTENSIONS = new string[]{"prefab", "mat", "shader", "json", "png"};

	// dictionary name
	public const string DIC_FIELDNAME = "dictResourceInfo";

	// config file path
	public const string CONFIG_PATH = "Scripts/LKCore/LKResource/ResourceConfig.cs";

	/// <summary>
	/// Generate resources config
	/// </summary>
	public static void GenResourceConfig()
	{
		EditorUtility.DisplayProgressBar("Generating resource config", "", 0f);

		// version 
		var version = Version;

		// path
		var assetPath = Application.dataPath;
		var resourcesPath = Path.Combine(assetPath, "Resources");

//		Debug.Log(assetPath);
//		Debug.Log(resourcesPath);

		var extensions = EXTENSIONS;

		// resources infos
		var str1 = new StringBuilder();
		for (var i = 0; i < extensions.Length; i++) 
		{
			// scan files & save file infos
			var files = GetFiles(resourcesPath, extensions[i]);

			// scan file infos & get codes string
			str1.Append(GetResourceConfigString(resourcesPath, files, version).ToString());
			if (i < extensions.Length - 1)
				str1.AppendLine();
		}

		EditorUtility.DisplayProgressBar("Generating resource config", "", 0.5f);

		// dic resources info
		var str2 = new StringBuilder();
		for (var i = 0; i < extensions.Length; i++) 
		{
			// scan files & save file infos
			var files = GetFiles(resourcesPath, extensions[i]);
			
			// scan file infos & get codes string
			str2.Append(GetResourceConfigDicString(resourcesPath, files).ToString());
			if (i < extensions.Length - 1)
				str2.AppendLine();
		}

		// write codes string into file
		var filePath = Path.Combine(assetPath, CONFIG_PATH);
		WriteResourceConfig(filePath, str1.ToString(), str2.ToString());

		EditorUtility.ClearProgressBar();

		// refresh asset
		AssetDatabase.Refresh();
	}
}

public partial class ResourceEditorGenConfig
{
	static List<string> GetFiles(string resourcesPath, string extension)
	{
		return Directory.GetFiles(resourcesPath, "*." + extension, SearchOption.AllDirectories)
			.ToList();
	}

	static StringBuilder GetResourceConfigString(string resourcesPath, List<string> files, int version)
	{
		var str = new StringBuilder();
		var fileKeys = new Dictionary<string, int>();

		for (int i = 0, max = files.Count; i < max; i++)
		{
			var file = files[i];
			var fileInfo = new ResourceEditorFileInfo(file);
			var fileKey = fileInfo.Name;
			var fileKeyIndex = -1;

			var tmpFileKey = fileKey;
			while (fileKeys.ContainsKey(tmpFileKey))
			{
				fileKeyIndex ++;
				tmpFileKey = fileKey + fileKeyIndex.ToString();
			}
			fileKey = tmpFileKey;

			fileKeys[fileKey] = fileKeyIndex;
			fileInfo.Key = fileKey;

			// assetbundle name
			var loadPath = fileInfo.GetLoadPath(resourcesPath);
			var assetImportPath = Path.Combine("Assets/Resources", loadPath + "." + fileInfo.Extension);
			var assetImporter = AssetImporter.GetAtPath(assetImportPath);

			str.Append(string.Format("\t\tpublic static ResourceInfo {0} = new ResourceInfo(", fileInfo.Key));
			str.Append(string.Format("\"{0}\", ", loadPath));
			str.Append(string.Format("\"{0}\", ", fileInfo.Key));
			str.Append(string.Format("\"{0}\", ", fileInfo.Name));
			str.Append(string.Format("\"{0}\", ", fileInfo.GameObjectName));
			if (assetImporter != null)
				str.Append(string.Format("\"{0}\", ", assetImporter.assetBundleName));
			str.Append(string.Format("{0}", version));
			str.AppendLine(");");
		}
		return str;
	}

	static StringBuilder GetResourceConfigDicString(string resourcesPath, List<string> files)
	{
		var str = new StringBuilder();
		var fileKeys = new Dictionary<string, int>();
		
		for (int i = 0, max = files.Count; i < max; i++)
		{
			var file = files[i];
			var fileInfo = new ResourceEditorFileInfo(file);
			var fileKey = fileInfo.Name;
			var fileKeyIndex = -1;
			
			var tmpFileKey = fileKey;
			while (fileKeys.ContainsKey(tmpFileKey))
			{
				fileKeyIndex ++;
				tmpFileKey = fileKey + fileKeyIndex.ToString();
			}
			fileKey = tmpFileKey;
			
			fileKeys[fileKey] = fileKeyIndex;
			fileInfo.Key = fileKey;

			str.AppendLine(string.Format("\t\t\t{0}[\"{1}\"] = {2};", DIC_FIELDNAME, fileInfo.GetLoadPath(resourcesPath), fileInfo.Key));
		}
		return str;
	}

	static void WriteResourceConfig(string filePath, string str1, string str2)
	{
		// remove file first
		if (File.Exists(filePath)) {
			File.Delete(filePath);
			AssetDatabase.Refresh();
		}

		// code string
		var codes =	"";
		codes += @"using System.Collections.Generic;

public class ResourceConfig
{
";
		codes += str1;

		codes += @"
	private static Dictionary<string, ResourceInfo> dictResourceInfo = new Dictionary<string, ResourceInfo>();
	static ResourceConfig()
	{
";
		codes += str2;
		codes += @"
	}

	/// <summary>
	/// Gets the ResourceInfo by resource path
	/// </summary>
	/// <returns>The res info.</returns>
	public static ResourceInfo GetResourceInfo(string path)
	{
";
		codes += string.Format("\t\t\tif ({0}.ContainsKey(path)) ", DIC_FIELDNAME);
		codes += string.Format("return {0}[path];", DIC_FIELDNAME);
		codes += @"
		return null;
	}
}
";

		var file = new StreamWriter(filePath);
		file.Write(codes);
		file.Close();
	}
}