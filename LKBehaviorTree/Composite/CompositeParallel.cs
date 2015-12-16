using UnityEngine;
using System.Collections;

namespace LKBehaviorTree
{
	// Execute childs in parallel 
	public class CompositeParallel : Composite
	{
		public bool Loop = true;

		protected override void DoCompositeReset()
		{
			if (this.Loop)
			{
				var childs = this._childs;
				for (int i = 0, max = childs.Count; i < max; i++)
				{
					var child = childs[i];
					if (child.IsEnd)
						child.Reset();
				}
			}
		}
	}
}
