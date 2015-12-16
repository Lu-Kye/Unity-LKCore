using UnityEngine;
using System.Collections;

namespace LKBehaviorTree 
{
	public abstract class TreeNode
	{
		// Current status
		TreeNodeStatus _status = TreeNodeStatus.None;
		public TreeNodeStatus Status
		{
			get { return this._status; }
		}

		// Parent & Child node
		protected TreeNode _parent;
		protected TreeNode _child;
		public virtual TreeNode Child 
		{ 
			get { return this._child; } 
			set
			{
				this._child = value;
				value._parent = this;
			}
		}

		public virtual bool IsNone
		{
			get { return this._status == TreeNodeStatus.None; }
			set { this._status = TreeNodeStatus.None; }
		}

		public virtual bool IsInit
		{
			get { return this._status == TreeNodeStatus.Init; }
			set { this._status = TreeNodeStatus.Init; }
		}

		public virtual bool IsRunning
		{
			get { return this._status == TreeNodeStatus.Running; }
			set { this._status = TreeNodeStatus.Running; }
		}

		public virtual bool IsPause
		{
			get { return this._status == TreeNodeStatus.Pause; }
			set { this._status = TreeNodeStatus.Pause; }
		}

		public virtual bool IsSuccess
		{
			get { return this._status == TreeNodeStatus.Success; }
			set { this._status = TreeNodeStatus.Success; }
		}

		public virtual bool IsFailure 
		{
			get { return this._status == TreeNodeStatus.Failure; }
			set { this._status = TreeNodeStatus.Failure; }
		}

		public virtual bool IsEnd
		{
			get { return this.IsSuccess || this.IsFailure; }
		}

		// Reset this node
		public void Reset()
		{
			this.DoReset();

			if (this.IsSuccess)
				this.DoResetInSuccess();
			else if (this.IsFailure)
				this.DoResetInFailure();

			this.IsInit = true;
		}
		protected virtual void DoReset()
		{
			// Implemented by derived class
		}
		protected virtual void DoResetInSuccess()
		{
			// Implemented by derived class
		}
		protected virtual void DoResetInFailure()
		{
			// Implemented by derived class
		}

		// Pause this node
		public void Pause()
		{
			this.IsPause = true;
			this.DoPause();
		}
		protected virtual void DoPause()
		{
			// Implemented by derived class
		}

		// First Init 
		public void Init()
		{
			this.IsInit = true;
			this.DoInit();
		}
		protected virtual void DoInit()
		{
			// Implemented by derived class
		}

		// Next Run
		public void Run()
		{
			this.IsRunning = true;
			this.DoRun();
		}
		protected virtual void DoRun()
		{
			// Implemented by derived class
		}

		// Update every frame
		public void Update(float delta)
		{
			if (!this.IsRunning)
				return;

			this.DoUpdate(delta);
		}
		protected virtual void DoUpdate(float delta)
		{
			// Implemented by derived class
		}

		// Success
		protected void Success()
		{
			if (!this.IsRunning)
				return;

			this.IsSuccess = true;
			this.DoSuccess();
		}
		protected virtual void DoSuccess()
		{
			// Implemented by derived class
		}

		// Failure
		protected void Failure()
		{
			if (!this.IsRunning)
				return;

			this.IsFailure = true;
			this.DoFailure();
		}
		protected virtual void DoFailure()
		{
			// Implemented by derived class
		}
	}
}