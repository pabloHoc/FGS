using System;
namespace FSG.UtilityAI
{
	public interface ITask<Context, Target> where Context : IState
	{
		public string Name { get; }

		public bool IsValid();

		public double GetScore();
	}
}

