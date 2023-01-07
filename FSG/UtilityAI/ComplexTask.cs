using System;
using Myra.Graphics2D;
using System.Collections.Generic;

namespace FSG.UtilityAI
{
	public abstract class ComplexTask<Context, Target> : PrimitiveTask<Context, Target>
        where Context : IState
	{
        public List<string> SubTaskNames { get; }

        protected ComplexTask(
            string name,
            double weight,
            Func<Context, Target, bool> validator,
            List<Scorer> scorers,
            List<string> subTaskNames,
            Context context,
            Target target
        ) : base(name, weight, validator, scorers, context, target)
        {
            SubTaskNames = subTaskNames;
		}

        public abstract List<ITask<Context, Target>> GetScoredTasks(List<ITask<Context, Target>> subTasks);
	}
}

