using UnityEngine;

/// <summary>
/// Util for game object
/// </summary>
public class UtilGameObject
{
	/// <summary>
	/// Find the specified gameobject by name
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="isRoot">If set to <c>true</c> is root.</param>
	public static GameObject Find(string name, bool isRoot = false)
	{
		var go = GameObject.Find(name);
		if (go == null)
			return null;

		if (!isRoot) 
			return go;

		return go.transform.parent == null ? go : null;
	}

	/// <summary>
	/// Finds the type of the specify name gameObject.
	/// </summary>
	/// <returns>The with type.</returns>
	/// <param name="name">Name.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T FindWithType<T>(string name, out GameObject root)
		where T : Component
	{
		T result;
		var gos = GameObject.FindObjectsOfType<GameObject>();
		for (int i = 0, max = gos.Length; i < max; i++)
		{
			var go = gos[i];
			if (go.name != name)
				continue;

			if ((result = go.GetComponent<T>()) != null)
			{
				root = go;
				return result;
			}

			if ((result = go.GetComponentInChildren<T>()) != null)
			{
				root = go;
				return result;
			}
		}
		root = null;
		return null;
	}

	/// <summary>
	/// Finds the specified gameobject in parent.
	/// </summary>
	/// <returns>The in parent.</returns>
	/// <param name="name">Name.</param>
	/// <param name="parent">Parent.</param>
	/// <param name="recursive">Recursive.</param>
	public static GameObject FindInParent(string name, GameObject parent, bool recursive = false)
	{
		if (parent == null) {
			return null;
		}
		
		GameObject child = null;
		Transform childTransform;
		
		for (int i = 0, max = parent.transform.childCount; i < max; i ++) {
			childTransform = parent.transform.GetChild(i);
			
			if (childTransform.gameObject.name == name) {
				child = childTransform.gameObject;
				break;
			}
		}

		if (child == null && recursive)
		{
			for (int i = 0, max = parent.transform.childCount; i < max; i ++) {
				child = FindInParent(name, parent.transform.GetChild(i).gameObject, recursive);
				if (child != null)
					return child;
			}
		}
		
		return child;
	}
	
	/// <summary>
	/// Create the specified name.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="isMulti">If set to <c>true</c> is multi create even if has same name gameobject.</param>
	public static GameObject Create(string name, bool isMulti = false)
	{
		if (isMulti) {
			var go = UtilGameObject.Find(name);
			if (go != null) {
				return go;
			}
		}
		
		return new GameObject(name);
	}
	
	/// <summary>
	/// Creates gameobject the in parent.
	/// </summary>
	/// <returns>The in parent.</returns>
	/// <param name="name">Name.</param>
	/// <param name="parent">Parent.</param>
	/// <param name="isMulti">If set to <c>true</c> is multi create even if has same name gameobject.</param>
	public static GameObject CreateInParent(string name, GameObject parent, bool isMulti = false)
	{
		if (parent == null) {
			return null;
		}
		
		GameObject go;
		
		if (!isMulti) {
			go = UtilGameObject.FindInParent(name, parent);
			if (go != null) {
				return go;
			}
		}
		
		go = UtilGameObject.Create(name);
		go.transform.parent = parent.transform;
		return go;
	}
	
	/// <summary>
	/// Creates gameobject by prefab in the parent
	/// </summary>
	/// <returns>The by G.</returns>
	/// <param name="prefab">Prefab.</param>
	/// <param name="parent">Parent.</param>
	public static GameObject CreateByGO(GameObject prefab, GameObject parent = null)
	{
		if (prefab == null) {
			return null;
		}
		
		var go = GameObject.Instantiate(prefab);
		go.name = go.name.Replace("(Clone)", "");

		// Set parent
		if (parent != null) 
			go.transform.SetParent(parent.transform);

		// Reset transform
		go.transform.localPosition = prefab.transform.localPosition;
		go.transform.localRotation = prefab.transform.localRotation;
		go.transform.localScale = prefab.transform.localScale;

		return go;
	}

	/// <summary>
	/// Deletes the childs.
	/// </summary>
	/// <param name="go">Go.</param>
	public static void DeleteChilds(GameObject go, bool immediate = false)
	{
		for (int i = go.transform.childCount - 1; i >= 0; i--) {
			var child = go.transform.GetChild(i);
			if (child != null)
			{
				if (immediate)
					GameObject.DestroyImmediate(child.gameObject);
				else
					GameObject.Destroy(child.gameObject);
			}
		}
	}

	/// <summary>
	/// Active the childs.
	/// </summary>
	/// <param name="go">GameObject.</param>
	/// <param name="active">If set to <c>true</c> active.</param>
	public static void ActiveChilds(GameObject go, bool active = true)
	{
		for (int i = 0, max = go.transform.childCount; i < max; i ++) {
			go.transform.GetChild(i).gameObject.SetActive(active);
		}
	}
	}
