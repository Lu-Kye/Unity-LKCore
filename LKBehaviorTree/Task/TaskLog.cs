﻿using UnityEngine;
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

		protected override void DoTask(float delta)
		{
			Debug.Log(this.Msg);
		}
	}
}