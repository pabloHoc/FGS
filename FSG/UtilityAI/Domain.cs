using System;
using System.Collections.Generic;

namespace FSG.UtilityAI
{
	public interface IDomain<Context, Target> where Context : IState
	{
		public ITask<Context, Target> GetRootTask();

		public List<ITask<Context, Target>> GetTasks(List<string> taskNames);
	}
}

