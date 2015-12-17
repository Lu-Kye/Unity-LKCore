using UnityEngine;
using LKSingleton;

/// <summary>
/// Resources manager
/// - Load resources under /Resources/ (prefab, material, texture .etc)
/// - Return exact object type
/// </summary>
public partial class ResourceManager : SingleTonMonoBehaviour<ResourceManager>
{
	// loader
	IResourceLoader loader;
	IResourceLoader loaderWWW;

	// pool
	ResourcePool<ResourceInfo, Object> pool = new ResourcePool<ResourceInfo, Object>();
	public ResourceManager()
	{
		this.loader = new ResourceLoader();
		this.loaderWWW = new ResourceLoaderWWW();
	}

	/// <summary>
	/// Load resource
	/// </summary>
	/// <returns>Object</returns>
	public T Load<T>(ResourceInfo resourceInfo)
		where T : Object
	{
		if (resourceInfo == null)
			return null;

		// load from local
		if (this.pool.Contains(resourceInfo)) 
		{
			var cache = this.pool.Get(resourceInfo);
			try 
			{
				return (T)cache;
			} 
			catch (System.Exception e)
			{
				Debug.LogError(e.Message);
				return null;
			}
		}
		
		var obj = this.GetLoader(resourceInfo).Load(resourceInfo);
		try 
		{
			return (T)obj;
		} 
		catch (System.Exception e)
		{
			Debug.LogError(e.Message);
			return null;
		}
	}

	/// <summary>
	/// Loads at path.
	/// </summary>
	/// <returns>The at path.</returns>
	/// <param name="path">Path.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T LoadAtPath<T>(string path)
		where T : Object
	{
		return this.Load<T>(ResourceConfig.GetResourceInfo(path));
	}

	/// <summary>
	/// Unload the specified ResourceInfo's resource.
	/// </summary>
	/// <param name="ResourceInfo">Res info.</param>
	public void Unload(ResourceInfo resourceInfo)
	{
		if (!this.pool.Contains(resourceInfo)) {
			return;
		}

		var obj = this.pool.Remove(resourceInfo);
		this.GetLoader(resourceInfo).UnLoad(resourceInfo);
		Resources.UnloadAsset(obj);
	}

	/// <summary>
	/// Unloads all cached.
	/// </summary>
	public void UnloadAll()
	{
		this.pool.Iter((resourceInfo, obj) => {
			this.Unload(resourceInfo);
		});
	}
}

public partial class ResourceManager
{
	IResourceLoader GetLoader(ResourceInfo resourceInfo)
	{
		// TODO test
//			return this.loaderWWW;

//#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID) 
//			if (!string.IsNullOrEmpty(resourceInfo.AssetBundleName))
//				return this.loaderWWW;
//#endif

		return this.loader;
	}
}
