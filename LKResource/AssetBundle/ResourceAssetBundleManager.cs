using UnityEngine;

/// <summary>
/// Resource asset bundle manager.
/// </summary>
public partial class ResourceAssetBundleManager
{
	public static readonly ResourceAssetBundleManager only = new ResourceAssetBundleManager();

	// cached resource asset bundle
	ResourcePool<string, ResourceAssetBundle> pool = new ResourcePool<string, ResourceAssetBundle>();

	ResourceAssetBundleManager()
	{
		this.pool.Infinity = true;
	}

	/// <summary>
	/// Inits all asset bundles.
	/// - Should be called at game start 
	/// </summary>
	public void LoadAssetBundles()
	{
		var manifestName = ResourceAssetBundle.GetPlatformFolderForAssetBundles(Application.platform);
		var resourceAssetBundle = this.GetResourceAssetBundle(manifestName);
		var manifest = resourceAssetBundle.AssetBundle.LoadAsset<AssetBundleManifest>(manifestName);

		// all assetbundle names

		// compare with local assetbundle version

		// download lastest assetbundle & write to local filesystem

		// cached all assetbundle into memory
	}

	/// <summary>
	/// Gets the resource asset bundle.
	/// </summary>
	/// <returns>The resource asset bundle.</returns>
	/// <param name="resourceInfo">Resource info.</param>
	public ResourceAssetBundle GetResourceAssetBundle(string name)
	{
		var resourceAssetBundle = pool.Get(name);
		if (resourceAssetBundle != null)
			return resourceAssetBundle;

		return this.pool.Get(name);
	}
}

partial class ResourceAssetBundleManager
{
}
