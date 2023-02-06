using System;
using System.Collections.Generic;
using FSG.UtilityAI;
using FSG.Scopes;

namespace FSG.Definitions
{
	public class TaskDefinition : IDefinition
	{
        public DefinitionType DefinitionType => DefinitionType.Task;

        public string Name { get; init; }

		public string TaskType { get; init; }

		public List<string> Subtasks { get; init; }

		public double Weight { get; init; }

		public FSG.Conditions.Conditions Conditions { get; init; }

		public List<string> Scorers { get; init; }

		public bool IsRoot { get; init; }

		public Scope Target { get; init; }
    }
}

