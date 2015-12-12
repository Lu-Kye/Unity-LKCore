using UnityEngine;

namespace LKAction 
{
	/// <summary>
	/// Action test.
	/// </summary>
    public class ActionTest : MonoBehaviour
    {
		void Start()
		{
			ActionManager.Instance.Add(this.gameObject,
              	ActionMove.Create(this.gameObject,
			    	5.0f,
			        this.gameObject.transform.localPosition,
			        new Vector3(10f, 10f, 0f)
			    )
			);
		}
    }
}
