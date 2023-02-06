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
        private readonly ServiceProvider _serviceProvider;

        private readonly GameState _context;

        private readonly Player _player;

        private readonly TaskDefinition _rootTask;

        private readonly OperationMap _operationMap;

        public Domain(ServiceProvider serviceProvider, Player player)
        {
            _serviceProvider = serviceProvider;
            _context = serviceProvider.GlobalState;
            _player = player;
            _operationMap = new OperationMap(serviceProvider);

            _rootTask = serviceProvider.Definitions.GetAll<TaskDefinition>().Find(task => task.IsRoot);

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
                var definition = _serviceProvider.Definitions.Get<TaskDefinition>(taskName);
                var targets = _serviceProvider.Services.TaskService.GetTargets(_context, _player, definition);

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
            if (definition.Subtasks != null)
            {
                return new HighestScoreTask<GameState, IBaseEntity>(
                    definition.Name,
                    definition.Weight,
                    _serviceProvider.Services.TaskService.GetValidator<GameState, IBaseEntity>(definition),
                    _serviceProvider.Services.TaskService.GetScorers(definition),
                    definition.Subtasks,
                    _context,
                    target
                );
            }
            else
            {
                return new PrimitiveTask<GameState, IBaseEntity>(
                    definition.Name,
                    definition.Weight,
                    _serviceProvider.Services.TaskService.GetValidator<GameState, IBaseEntity>(definition),
                    _serviceProvider.Services.TaskService.GetScorers(definition),
                    _context,
                    target,
                    _operationMap.Get(definition.Name)
                );
            }
        }
    }
}

