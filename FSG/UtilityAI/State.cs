using System;
namespace FSG.UtilityAI
{
	public interface IState
	{
		public int getInputValue<T>(T target, string name);
	}
}

