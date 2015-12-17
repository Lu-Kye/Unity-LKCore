using System.IO;
using System.Text;

public class ResourceToolFileInfo 
{
	private string file;
	public string File
	{
		get { return this.file; }
	}

	//	private string dir;

	private string key;
	public string Key
	{
		get { return (this.Extension + "_" + this.key).ToUpper(); }
		set { this.key = value; }
	}

	private string name;
	public string Name 
	{
		get 
		{
			if (!string.IsNullOrEmpty(this.name))
				return this.name;

			var name = Path.GetFileNameWithoutExtension(this.file);
			StringBuilder sb = new StringBuilder();
			foreach (char c in name) {
				if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_') {
					if (c == '.')
						sb.Append('_');
					else
						sb.Append(c);
				}
			}
			return this.name = sb.ToString().ToUpper(); 
		}
	}

	public string GameObjectName
	{
		get
		{
			return Path.GetFileNameWithoutExtension(this.file);
		}
	}

	private string extension;
	public string Extension
	{
		get 
		{ 
			if (!string.IsNullOrEmpty(this.extension))
				return this.extension;
			return this.extension = Path.GetExtension(this.file).Replace(".", ""); 
		}
	}

	public ResourceToolFileInfo(string file)
	{
		this.file = file;

		//		var dirs = Path.GetDirectoryName(file).Split(Path.DirectorySeparatorChar);
		//		this.dir = dirs[dirs.Length - 1];
	}

	/// <summary>
	/// Get load path 
	/// </summary>
	/// <param name="resourcesPath"></param>
	/// <returns></returns>
	public string GetLoadPath(string resourcesPath)
	{
		var loadPath = this.File.Replace(resourcesPath + Path.DirectorySeparatorChar.ToString(), "");
		loadPath = loadPath.Replace("." + this.Extension, "");
		return loadPath.Replace(Path.DirectorySeparatorChar, '/');
	}
}
