using UnityEngine;
using System.Collections;

namespace Soul
{
	public delegate void ResourceLoaderHandler();

	/// <summary>
	/// Resources loader interface
	/// </summary>
	public interface IResourceLoader 
	{
#region sync
		Object Load(ResourceInfo resourceInfo);
		void UnLoad(ResourceInfo resourceInfo);
#endregion

#region async
		// unimplemented 
		IEnumerator Load(ResourceInfo resourceInfo, ResourceLoaderHandler resourceLoaderHandler);
#endregion
	}
}
