using UnityEngine;

namespace Soul 
{
	/// <summary>
	/// Resource info
	/// - Resource key, path .etc
	/// </summary>
	public class ResourceInfo
	{
		private string key;
		public string Key 
		{
			get { return this.key; }
		}

		private string name;
		public string Name
		{
			get { return this.name; }
		}

		private string gameObjectName;
		public string GameObjectName
		{
			get { return this.gameObjectName; }
		}

		private string path;
		public string Path
		{
			get { return this.path; }
		}

		private string assetBundleName;
		public string AssetBundleName
		{
			get { return this.assetBundleName; }
		}

		private int version;
		public int Version
		{
			get { return this.version; }
		}

		public ResourceInfo(string path, string key, string name, string gameObjectName, string assetBundleName, int version)
		{
			this.path = path;
			this.key = key;
			this.name = name;
			this.gameObjectName = gameObjectName;
			this.assetBundleName = assetBundleName;
			this.version = version;
		}
	}
}