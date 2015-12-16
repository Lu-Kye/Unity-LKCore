using UnityEngine;
using System.Collections;

namespace LKBehaviorTree 
{
	public enum TreeNodeStatus
	{
		Error,

		None,
		Init,
		Running,
		Pause,
		Success,
		Failure
	}
}
