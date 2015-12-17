using UnityEngine;
using System.Collections;
using System;

namespace LKBehaviorTree
{
	public class TaskFunc : Task 
	{
		Action<float> _func;

		public TaskFunc(Action<float> func)
		{
			this._func = func;
		}

		protected override void DoTask(float delta)
		{
			if (this._func == null)
			{
				this.Failure();
				return;
			}

			this._func(delta);
		}
	}
}