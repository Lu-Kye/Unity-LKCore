using UnityEngine;

#if UNITY_EDITOR	
using UnityEditor;
#endif

/// <summary>
/// Resources assetbundle instance class.
/// </summary>
public partial class ResourceAssetBundle
{
	#if UNITY_EDITOR
	public static string GetPlatformFolderForAssetBundles(BuildTarget target)
	{
		switch(target)
		{
		case BuildTarget.Android:
			return "Android";
		case BuildTarget.iOS:
			return "iOS";
		case BuildTarget.WebPlayer:
			return "WebPlayer";
		case BuildTarget.StandaloneWindows:
		case BuildTarget.StandaloneWindows64:
			return "Windows";
		case BuildTarget.StandaloneOSXIntel:
		case BuildTarget.StandaloneOSXIntel64:
		case BuildTarget.StandaloneOSXUniversal:
			return "OSX";
			// Add more build targets for your own.
			// If you add more targets, don't forget to add the same platforms to GetPlatformFolderForAssetBundles(RuntimePlatform) function.
		default:
			return "iOS";
		}
	}
	#endif
	
	public static string GetPlatformFolderForAssetBundles(RuntimePlatform platform)
	{
		switch(platform)
		{
		case RuntimePlatform.Android:
			return "Android";
		case RuntimePlatform.IPhonePlayer:
			return "iOS";
		case RuntimePlatform.WindowsWebPlayer:
		case RuntimePlatform.OSXWebPlayer:
			return "WebPlayer";
		case RuntimePlatform.WindowsEditor:
		case RuntimePlatform.WindowsPlayer:
			return "Windows";
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXPlayer:
			return "OSX";
			// Add more build platform for your own.
			// If you add more platforms, don't forget to add the same targets to GetPlatformFolderForAssetBundles(BuildTarget) function.
		default:
			return "iOS";
		}
	}
}

partial class ResourceAssetBundle
{
	// asset bundle name
	private string name;
	public string Name
	{
		get { return this.name; }
	}

	// asset bundle url
	private string url;
	private string Url 
	{
		get 
		{ 
			if (!string.IsNullOrEmpty(this.url))
				return url;

			var prefix = this.GetUrlPrefix();
			prefix += ResourceAssetBundle.GetPlatformFolderForAssetBundles(Application.platform) + "/";
			return this.url = prefix + this.name;
		}
	}

	// loaded asset bundle
	private AssetBundle assetBundle = null;
	public AssetBundle AssetBundle
	{
		get { return this.assetBundle; }
	}

	public ResourceAssetBundle(string name)
	{
		this.name = name;
	}

	/// <summary>
	/// Load assetbundle
	/// </summary>
	/// <param name="version">Version of this assetbundle, if new Version is bigger than 
	/// the local cached one, then will download a new assetbundle and save it.</param>
	public void Load(int version)
	{
		var url = this.Url;
		var www = WWW.LoadFromCacheOrDownload(url, version);
		this.assetBundle = www.assetBundle;
	}

	/// <summary>
	/// Loads assetbundle async.
	/// </summary>
	/// <param name="version">Version.</param>
	public void LoadAsync(int version)
	{

	}

	/// <summary>
	/// Load the specified name asset from the assetbundle.
	/// </summary>
	/// <param name="name">Name.</param>
	public Object LoadAsset(string name)
	{
		if (this.assetBundle == null)
			return null;

		return this.assetBundle.LoadAsset(name);
	}
}

partial class ResourceAssetBundle
{
	string GetUrlPrefix()
	{
#if DEBUG 
		return ResourceAssetBundleConfig.URL_PREFIX_DEBUG;
#else
		return ResourceAssetBundleConfig.URL_PREFIX_RELEASE;
#endif
	}
}

