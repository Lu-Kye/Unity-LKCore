using UnityEngine;
using System.Collections;

namespace LKBehaviorTree 
{
	public abstract class TreeNode
	{
		// Current status
		TreeNodeStatus _status = TreeNodeStatus.None;

		// Parent & Child node
		public TreeNode Parent { get; set; }
		public TreeNode Child { get; set; }

		public bool IsNone
		{
			get { return this._status == TreeNodeStatus.None; }
		}

		public bool IsInit
		{
			get { return this._status == TreeNodeStatus.Init; }
		}

		public bool IsRunning
		{
			get { return this._status == TreeNodeStatus.Running; }
		}

		public bool IsSuccess
		{
			get { return this._status == TreeNodeStatus.Success; }
		}

		public bool IsFailure 
		{
			get { return this._status == TreeNodeStatus.Failure; }
		}

		// Reset this node
		public virtual void Reset()
		{
			this._status = TreeNodeStatus.Init;
		}

		// Pause this node
		public void Pause()
		{
			this._status = TreeNodeStatus.Pause;
		}

		#region life circle
		// First Init 
		public virtual void Init()
		{
			this._status = TreeNodeStatus.Init;
		}

		// Next Run
		public virtual void Run()
		{
			this._status = TreeNodeStatus.Running;
		}

		// Update every frame
		public void Update()
		{
			if (!this.IsRunning)
				return;

			this.Step();
		}

		// Step
		public virtual void Step()
		{
			// Implemented by derived class
		}

		protected virtual void Success()
		{
			if (!this.IsRunning)
				return;

			this._status = TreeNodeStatus.Success;
		}
		#endregion
	}
}