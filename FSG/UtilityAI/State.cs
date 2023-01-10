using System;
namespace FSG.UtilityAI
{
	public interface IState
	{
		public int GetInputValue<T>(T target, string name);
	}
}

