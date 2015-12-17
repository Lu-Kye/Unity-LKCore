using UnityEditor;
using UnityEngine;

public class ResourceEditorMenu 
{
	[MenuItem("LKCore/Resource/GenResourceConfig")]
	public static void GenResourceConfig()
	{
		ResourceEditorGenConfig.GenResourceConfig();
	}

	[MenuItem("LKCore/Resource/GenAssetBundle")]
	public static void GenAssetBundle()
	{
		ResourceEditorGenAssetBundle.GenAssetBundle();
	}
}
