using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LKBehaviorTree 
{
	public class BehaviorTree 
	{
		// Tree current status
		TreeStatus _status = TreeStatus.None;

		// Current tree node
		TreeNode _node;

		// Root tree node
		TreeNode _root;
		public TreeNode Root
		{
			get { return this._root; }
		}

		// Last node
		TreeNode _last;
		public TreeNode Last
		{
			get { return this._last; }
		}

		// Whether this tree is loop
		public bool Loop = true;

		public virtual bool IsNone
		{
			get { return this._status == TreeStatus.None; }
			set { this._status = TreeStatus.None; }
		}

		public virtual bool IsRunning
		{
			get { return this._status == TreeStatus.Running; }
			set { this._status = TreeStatus.Running; }
		}

		public virtual bool IsPause
		{
			get { return this._status == TreeStatus.Pause; }
			set { this._status = TreeStatus.Pause; }
		}

		public virtual bool IsEnd
		{
			get { return this._status == TreeStatus.End; }
			set { this._status = TreeStatus.End; }
		}

		protected virtual void Init()
		{
			// Implemented by derived class
		}

		// Do run if you want to start this tree
		public void Run()
		{
			if (this.IsRunning)
				return;
			
			// Try init
			if (this.IsNone)
				this.Init();

			// Set running
			this.IsRunning = true;
			
			// Reset first node
			this._node = this._root;
		}

		// Do pause if you want to pause this tree
		public void Pause()
		{
			if (!this.IsRunning)
				return;

			this.IsPause = true;

			if (this._node != null)
				this._node.Pause();
		}

		// Update every frame
		public void Update()
		{
			if (!this.IsRunning)
				return;	

			// Update node
			{
				if (this._node == null)
				{
					if (!this.Loop)
					{
						this.IsEnd = true;
						return;
					}

					this._node = this._root;

					// Check, if error return 
					if (this._node == null)
						return;
				}

				if (this._node.IsNone)
					this._node.Init();

				if (this._node.IsInit)
					this._node.Run();

				if (this._node.IsRunning)
					this._node.Update(Time.deltaTime);

				if (this._node.IsEnd)
				{
					this._node.Reset();
					this._node = this._node.Child;
				}
			}
		}

		#region push
		public T PushNode<T>()
			where T : TreeNode, new()
		{
			return this.PushNode(new T()) as T;
		}

		// Push a node(task) in this tree
		public TreeNode PushNode(TreeNode node)
		{
			if (this._root == null)
				this._root = node;

			if (this._node != null)
				this._node.Child = node;

			this._node = node;
			this._last = node;

			return this._node;
		}

		// Push a tree into this tree
		public TreeNode PushTree(BehaviorTree tree)
		{
			if (tree._root == null)
				return null;

			var node = tree._root;
			while (node != null)
			{
				this.PushNode(node);
				node = node.Child;
			}

			return this._node;
		}
		#endregion
	}
}