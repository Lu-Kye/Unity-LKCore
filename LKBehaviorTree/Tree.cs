using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LKBehaviorTree 
{
	public class Tree : MonoBehaviour 
	{
		// Tree current status
		TreeStatus _status = TreeStatus.None;

		// Current tree node
		TreeNode _node;

		// Root tree node
		TreeNode _root;

		// Whether this tree is loop
		public bool Loop = false;

		public bool IsNone
		{
			get { return this._status == TreeStatus.None; }
		}

		public bool IsInit
		{
			get { return this._status == TreeStatus.Init; }
		}

		public bool IsRunning
		{
			get { return this._status == TreeStatus.Running; }
		}

		public bool IsPause
		{
			get { return this._status == TreeStatus.Pause; }
		}

		public bool IsEnd
		{
			get { return this._status == TreeStatus.End; }
		}

		// Do init before start this tree
		public void Init()
		{
			this._status = TreeStatus.Init;
			this._node = this._root;
		}

		// Do run if you want to start this tree
		public void Run()
		{
			if (this._status != TreeStatus.Init)
				return;

			this._status = TreeStatus.Running;
		}

		// Update every frame
		void Update()
		{
			if (!this.IsRunning)
				return;	

			// Update node
			{
				if (this._node == null)
				{
					if (!this.Loop)
					{
						this._status = TreeStatus.End;
						return;
					}

					this._node = this._root;
				}

				if (this._node.IsNone)
					this._node.Init();

				if (this._node.IsInit)
					this._node.Run();

				if (this._node.IsRunning)
					this._node.Update();

				if (this._node.IsSuccess)
				{
					this._node.Reset();
					this._node = this._node.Child;
				}
			}
		}

		// Do pause if you want to pause this tree
		public void Pause()
		{
			this._status = TreeStatus.Pause;

			if (this._node != null)
				this._node.Pause();
		}

		#region for task
		// Push a node(task) in this tree
		public void PushNode(TreeNode node)
		{
			if (this._root == null)
				this._root = node;

			if (this._node != null)
			{
				this._node.Child = node;
				node.Parent = this._node;
			}

			this._node = node;
		}
		#endregion
	}
}