using System;

namespace LKBehaviorTree
{
	public class ConditionFunc : Condition 
	{
		public delegate bool Func();
		Func _func;

		public ConditionFunc(Func func)
		{
			this._func = func;
		}

		protected override bool DoCondition()
		{
			if (this._func == null)
				return false;

			return this._func();
		}
	}
}