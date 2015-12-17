using UnityEngine;
using System.Collections;

/// <summary>
/// Resources loader
/// - Default resource loader
/// - Load resources by UnityEngine.Resources
/// </summary>
public class ResourceLoader : IResourceLoader
{
	/// <summary>
	/// Load the specified ResourceInfo.
	/// </summary>
	/// <param name="ResourceInfo">Res info.</param>
	public Object Load(ResourceInfo resourceInfo)
	{
		return Resources.Load(resourceInfo.Path);
	}

	/// <summary>
	/// Unload the specified ResourceInfo and object
	/// </summary>
	/// <param name="ResourceInfo">Res info.</param>
	public void UnLoad(ResourceInfo resourceInfo)
	{
		// not implemented
	}

	/// <summary>
	/// Load the specified ResourceInfo and ResourceLoaderHandler.
	/// </summary>
	/// <param name="ResourceInfo">Res info.</param>
	/// <param name="ResourceLoaderHandler">Res loader handler.</param>
	public IEnumerator Load(ResourceInfo resourceInfo, ResourceLoaderHandler resourceLoaderHandler)
	{
		// not implemented
		return null;
	}
}
