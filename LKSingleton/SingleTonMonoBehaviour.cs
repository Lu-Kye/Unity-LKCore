using UnityEngine;


namespace LKSingleton
{
	/// Singleton MonoBehaviour
	public class SingleTonMonoBehaviour<T> : MonoBehaviour 
		where T : SingleTonMonoBehaviour<T>
	{
		#region static
		protected static GameObject _forever = null;
		
		// If singleton is destroyed means it will nerver be used 
		protected static bool _destroyed = false;

		// Instance
		protected static T _instance;
		public static T Instance 
		{
			get {
				if (_destroyed) 
					return null;
				
				if (_forever == null)
					InitForever();
				
				if (_instance == null) 
					InitInstance();
				
				return _instance;
			}
		}
		#endregion

		// Whether is inited
		protected bool _inited = false;

		// Name for this game object
		protected string _name;

		// Init forever game object
		static void InitForever()
		{
			_forever = UtilGameObject.Create("Forever");
			GameObject.DontDestroyOnLoad(_forever);
		}

		// Init this static member(_instance)
		static void InitInstance()
		{
			var go = new GameObject();
			go.transform.parent = _forever.transform;

			_instance = go.AddComponent<T>();
			_instance.InitName();
			_instance.Init();
			_instance._inited = true;

			// Set name
			if (string.IsNullOrEmpty(_instance._name))
				go.name = _instance.GetType().Name;
			else
				go.name = _instance._name;
		}

		// Init name
		// - Not must implemented
		protected virtual void InitName()
		{
			// Implemented by derived class
		}

		// Init
		protected virtual void Init() 
		{
			// Implemented by derived class
		}

		// Destroy
		protected virtual void OnDestroy() 
		{
			_destroyed = true;
			_instance = null;
		}
	}
}
