using UnityEngine;
using System.Collections;

namespace LKBehaviorTree
{
	public abstract class Task : TreeNode
	{
		protected override void DoUpdate(float delta)
		{
			this.DoTask();
			this.Success();
		}

		protected virtual void DoTask()
		{
			// Implemented by derived class	
		}
	}
}