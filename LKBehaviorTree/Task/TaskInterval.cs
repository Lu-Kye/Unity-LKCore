using UnityEngine;
using System.Collections;

namespace LKBehaviorTree
{
	public abstract class TaskInterval : Task 
	{
		// Current update time
		float _updateTime = -1f;

		// Whether update first 
		public bool UpdateAtFirst = false;

		// Interval
		public float UpdateInterval = 0f;

		protected override void DoUpdate(float delta)
		{
			if (this._updateTime == -1f)
			{
				if (this.UpdateAtFirst)
					this._updateTime = float.MinValue;
				else
					this._updateTime = Time.realtimeSinceStartup;
			}

			if (Time.realtimeSinceStartup - this._updateTime >= this.UpdateInterval)
			{
				this._updateTime = Time.realtimeSinceStartup;

				this.DoTask(delta);
				this.Success();

				return;
			}

			this.Failure();
		}
	}
}