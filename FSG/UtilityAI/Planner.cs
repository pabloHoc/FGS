using System;
using System.Collections.Generic;

namespace FSG.UtilityAI
{
	public class Planner<Context, Target> where Context : IState
	{
		private readonly IDomain<Context, Target> _domain;

		private Queue<ITask<Context, Target>> _tasksToProcess;

		private List<PrimitiveTask<Context, Target>> _finalTasks;

		public Planner(IDomain<Context, Target> domain)
		{
			_domain = domain;
		}

		public void GeneratePlan()
		{
			ResetPlan();

			while(_tasksToProcess.Count > 0)
			{
				var task = _tasksToProcess.Dequeue();

				if (typeof(PrimitiveTask<Context, Target>) == task.GetType())
				{
					ProcessPrimitiveTask((PrimitiveTask<Context, Target>)task);
				}

                if (typeof(ComplexTask<Context, Target>) == task.GetType())
                {
                    ProcessComplexTask((ComplexTask<Context, Target>)task);
                }
            }
		}

		public void ResetPlan()
		{
			_tasksToProcess = new Queue<ITask<Context, Target>>();
			_tasksToProcess.Enqueue(_domain.GetRootTask());
			
			_finalTasks = new List<PrimitiveTask<Context, Target>>();
		}

		public void ExecutePlan()
		{
			foreach (var task in _finalTasks)
			{
				task.Execute();
			}
		}

		public void ProcessPrimitiveTask(PrimitiveTask<Context, Target> task)
		{
			if (task.IsValid())
			{
				_finalTasks.Add(task);
			}
		}

		public void ProcessComplexTask(ComplexTask<Context, Target> task)
		{
			if (task.IsValid())
			{
				// TODO: maybe we could inject domain into task and let the
				// task to process itself
				var subTasks = _domain.GetTasks(task.SubTaskNames);
				var validTasks = subTasks.FindAll(task => task.IsValid());
				var scoredTasks = task.GetScoredTasks(validTasks);

				foreach (var scoredTask in scoredTasks)
				{
					// TODO: check if we need to reverse queue order
					_tasksToProcess.Enqueue(scoredTask);
				}
			}
		}
	}
}

