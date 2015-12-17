using UnityEngine;
using System.Collections;

namespace LKBehaviorTree
{
	public class CompositeSelector : Composite
	{
		protected override bool CanContinue()
		{
			return base.CanContinue() && !this._current.IsSuccess;
		}

		protected override void DoCompositeEnd()
		{
			if (this._current.IsSuccess)
				this.Success();
			else
				this.Failure();
		}

		protected override void DoSuccess()
		{
			this.Child = this._current.Child;
		}

		protected override void DoFailure()
		{
			this.Child = null;
		}

		/// <summary>
		/// Add a condition.
		/// </summary>
		/// <param name="condition">Condition.</param>
		public void AddCondition(Condition condition)
		{
			this.AddChild(condition);
		}

		/// <summary>
		/// Add a condition.
		/// </summary>
		/// <param name="condition">Condition.</param>
		/// <param name="child">If this condition success will execute the child.</param>
		public void AddCondition(Condition condition, TreeNode child)
		{
			condition.Child = child;
			this.AddChild(condition);
		}

		/// <summary>
		/// Add a condition which is always return true
		/// </summary>
		/// <param name="child">This child maybe the last task which likes "switch-case's default"</param>
		public void AddCondition(TreeNode child)
		{
			var condition = new ConditionFunc(() => { return true; });
			this.AddCondition(condition, child);
		}

		/// <summary>
		/// Add a condition.
		/// </summary>
		/// <param name="condition">Condition.</param>
		/// <param name="tree">If this condition success will execute the root of this tree.</param>
		public void AddCondition(Condition condition, BehaviorTree tree)
		{
			condition.Child = tree.Root;
			this.AddChild(condition);
		}

		/// <summary>
		/// Add a condition which is always return true
		/// </summary>
		/// <param name="tree">This tree maybe the last task which likes "switch-case's default"</param>
		public void AddCondition(BehaviorTree tree)
		{
			var condition = new ConditionFunc(() => { return true; });
			this.AddCondition(condition, tree);
		}
	}
}