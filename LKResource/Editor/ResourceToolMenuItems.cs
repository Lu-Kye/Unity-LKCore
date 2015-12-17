using UnityEditor;
using UnityEngine;

public class ResourceToolMenuItems {
	[MenuItem("Soul/Resource/GenResourceConfig")]
	public static void GenResourceConfig()
	{
		ResourceToolGenConfig.GenResourceConfig();
	}

	[MenuItem("Soul/Resource/GenAssetBundle")]
	public static void GenAssetBundle()
	{
		ResourceToolGenAssetBundle.GenAssetBundle();
	}
}
