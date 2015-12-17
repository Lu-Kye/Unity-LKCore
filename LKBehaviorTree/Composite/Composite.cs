using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LKBehaviorTree
{
	public abstract class Composite : TreeNode
	{
		protected List<TreeNode> _childs = new List<TreeNode>();

		// Current child
		protected int _index = 0;
		protected TreeNode _current 
		{
			get 
			{ 
				if (this._index >= this._childs.Count)
					return null;
				return this._childs[this._index]; 
			}
		}
		protected TreeNodeStatus _currentStatus
		{
			get 
			{
				if (this._current == null)
					return TreeNodeStatus.Error;
				return this._current.Status;
			}
		}

		// Add a child
		public void AddChild(TreeNode child)
		{
			this._childs.Add(child);
		}

		// Whether can continue to execute next child
		protected virtual bool CanContinue()
		{
			return this._index < this._childs.Count - 1;
		}

		// Run current child
		protected virtual void DoCompositeRun()
		{
			this._current.Run();
		}

		// Update current child
		protected virtual void DoCompositeUpdate(float delta)
		{
			this._current.Update(delta);
		}

		// Try change to next child
		protected virtual void DoCompositeNext()
		{
			this._index ++;	
		}

		// Do success at the end
		protected virtual void DoCompositeEnd()
		{
			this.Success();
		}

		// Do reset after end of childs
		protected virtual void DoCompositeReset()
		{
			var childs = this._childs;
			for (int i = 0, max = childs.Count; i < max; i++)
			{
				var child = childs[i];
				if (child.IsEnd)
					child.Reset();
			}
		}

		#region override 
		protected override void DoInit()
		{
			var childs = this._childs;
			for (int i = 0, max = childs.Count; i < max; i++)
			{
				var child = childs[i];
				if (child.IsNone)
					child.Init();
			}
		}

		protected override void DoUpdate(float delta)
		{
			if (this._current == null)
			{
				this.Failure();
				return;
			}

			do 
			{
				// Run
				this.DoCompositeRun();

				// Update
				this.DoCompositeUpdate(delta);

				// Next
				if (this.CanContinue())
					this.DoCompositeNext();
				else
					break;

			} while(true);

			// End
			this.DoCompositeEnd();
		}

		protected override void DoReset()
		{
			this.DoCompositeReset();
		}

		protected override void DoResetInSuccess()
		{
			this._index = 0;
		}
		#endregion
	}
}
