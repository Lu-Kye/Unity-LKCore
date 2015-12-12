using UnityEngine;
using System.Collections;

namespace LKBehaviorTree 
{
	public enum TreeNodeStatus
	{
		None,
		Init,
		Running,
		Pause,
		Success,
		Failure
	}
}
