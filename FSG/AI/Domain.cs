using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Definitions;
using FSG.Entities;
using FSG.Services;
using FSG.UtilityAI;
using FSG.UtilityAI.ComplexTasks;

namespace FSG.AI
{
    public class Domain : IDomain<GameState, IBaseEntity>
    {
        private readonly List<TaskDefinition> _taskDefinitions;

        private readonly GameState _context;

        private readonly Player _player;

        private readonly TaskService _taskService;

        private readonly TaskDefinition _rootTask;

        public Domain(List<TaskDefinition> taskDefinitions, GameState context, Player player, TaskService taskService)
        {
            _taskDefinitions = taskDefinitions;
            _context = context;
            _player = player;
            _taskService = taskService;

            _rootTask = taskDefinitions.Find(task => task.IsRoot);

            if (_rootTask == null)
            {
                throw new Exception("No root task found in domain");
            }
        }

        public ITask<GameState, IBaseEntity> GetRootTask()
        {
            return GetTask(_player, _rootTask);
        }

        public List<ITask<GameState, IBaseEntity>> GetTasks(List<string> taskNames)
        {
            var tasks = new List<ITask<GameState, IBaseEntity>>();

            foreach (var taskName in taskNames)
            {
                var definition = _taskDefinitions.Find(definition => definition.Name == taskName);
                var targets = _taskService.GetTargets(_context, _player, definition);

                foreach (var target in targets)
                {
                    var task = GetTask(target, definition);
                    tasks.Add(task);
                }
            }

            return tasks;
        }

        private ITask<GameState, IBaseEntity> GetTask(IBaseEntity target, TaskDefinition definition)
        {
            if (definition.SubTasks != null)
            {
                return new HighestScoreTask<GameState, IBaseEntity>(
                    definition.Name,
                    definition.Weight,
                    _taskService.GetValidator<GameState, IBaseEntity>(definition),
                    _taskService.GetScorers(definition),
                    definition.SubTasks,
                    _context,
                    target
                );
            }
            else
            {
                return new PrimitiveTask<GameState, IBaseEntity>(
                    definition.Name,
                    definition.Weight,
                    _taskService.GetValidator<GameState, IBaseEntity>(definition),
                    _taskService.GetScorers(definition),
                    _context,
                    target
                );
            }
        }
    }
}

