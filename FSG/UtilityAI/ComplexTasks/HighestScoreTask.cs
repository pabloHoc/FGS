using System;
using System.Collections.Generic;

namespace FSG.UtilityAI.ComplexTasks
{
    public class HighestScoreTask<Context, Target> : ComplexTask<Context, Target>
        where Context : IState
    {
        public HighestScoreTask(
            string name,
            double weight,
            Func<Context, Target, bool> validator,
            List<Scorer> scorers,
            List<string> subTaskNames,
            Context context,
            Target target)
            : base(name, weight, validator, scorers, subTaskNames, context, target)
        {
        }

        public override List<ITask<Context, Target>> GetScoredTasks(List<ITask<Context, Target>> subTasks)
        {
            double highestScore = 0.0f;
            var highestScoreTask = subTasks[0];

            foreach (var task in subTasks)
            {
                var taskScore = task.GetScore();
                if (taskScore > highestScore)
                {
                    highestScore = taskScore;
                    highestScoreTask = task;
                }
            }

            return new List<ITask<Context, Target>> { { highestScoreTask } };
        }
    }
}

