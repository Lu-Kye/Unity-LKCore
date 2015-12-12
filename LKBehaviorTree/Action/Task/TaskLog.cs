using UnityEngine;
using System.Collections;

namespace LKBehaviorTree
{
	public class TaskLog : Task 
	{
		public string Msg;

		public TaskLog(string msg)
		{
			this.Msg = msg;
		}

		public override void Step()
		{
			Debug.Log(this.Msg);
			this.Success();
		}
	}
}