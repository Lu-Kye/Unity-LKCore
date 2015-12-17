using UnityEngine;
using System.Collections;

namespace LKBehaviorTree
{
	public abstract class Task : TreeNode
	{
		protected override void DoUpdate(float delta)
		{
			this.DoTask(delta);
			this.Success();
		}

		protected virtual void DoTask(float delta)
		{
			// Implemented by derived class	
		}
	}
}