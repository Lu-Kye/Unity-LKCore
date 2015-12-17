using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Linq;

public partial class ResourceEditorGenAssetBundle 
{
	public const string ASSET_BUNDLES_FOLDER = "AssetBundles";
	public const string ASSET_BUNDLES_INFO = "AssetBundlesInfo.json";

	/// <summary>
	/// Generate the asset bundles.
	/// </summary>
	public static void GenAssetBundle()
	{
		string outputPath = Path.Combine(ASSET_BUNDLES_FOLDER, ResourceAssetBundle.GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget));
		if (!Directory.Exists(outputPath))
			Directory.CreateDirectory (outputPath);

		BuildPipeline.BuildAssetBundles(
			outputPath, 
			BuildAssetBundleOptions.None, 
			EditorUserBuildSettings.activeBuildTarget
		);

		GenAssetBundlesInfo();
	}
}

partial class ResourceEditorGenAssetBundle 
{
	/// <summary>
	/// Gens the asset bundles info.
	/// </summary>
	static void GenAssetBundlesInfo()
	{
//		var projectPath = Path.Combine(Application.dataPath, "../");
//		var assetBundlesPath = Path.Combine(projectPath, ASSET_BUNDLES_FOLDER);
//		var path = Path.Combine(assetBundlesPath, ResourceAssetBundle.GetPlatformFolderForAssetBundles(EditorUserBuildSettings.activeBuildTarget));
//		
//		// scan all bundles
//		var files = Directory.GetFiles(path)
//			.Where(file => file.ToLower().EndsWith(".unity3d")).ToList();
//		
//		// saved config to json object
//		var json = new JsonData();
//		for (int i = 0, max = files.Count; i < max; i++)
//		{
//			var file = files[i];
//			Hash128 hash;
//			BuildPipeline.GetHashForAssetBundle(file, out hash);
//			json[Path.GetFileName(file)] = hash.ToString();
//		}
//		
//		// write to file
//		var assetBundlesInfoPath = Path.Combine(path, ASSET_BUNDLES_INFO);
//		var sw = new StreamWriter(assetBundlesInfoPath);
//		sw.Write(json.ToJson());
//		sw.Close();

		// refresh
		AssetDatabase.Refresh();
	}
}