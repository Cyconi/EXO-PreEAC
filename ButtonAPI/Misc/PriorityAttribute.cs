using System;

namespace xButtonAPI.Misc
{
    internal class PriorityAttribute : Attribute
	{
		public readonly int priority;

		public PriorityAttribute(int priority)
		{
			this.priority = priority;
		}
	}
}
