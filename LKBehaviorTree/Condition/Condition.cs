using UnityEngine;
using System.Collections;

namespace LKBehaviorTree
{
	public abstract class Condition : TreeNode 
	{
		protected override void DoUpdate(float delta)
		{
			if (this.DoCondition())
				this.Success();
			else
				this.Failure();
		}

		protected virtual bool DoCondition()
		{
			return true;
		}
	}
}
