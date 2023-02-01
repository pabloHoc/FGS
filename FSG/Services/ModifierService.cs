using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.Services
{
    public class ModifierService
    {
        private readonly ServiceProvider _serviceProvider;

        public ModifierService(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<Modifier> GetModifiersFor<T>(T entity) where T : IEntity<T>
		{
            return _serviceProvider.GlobalState.World.Modifiers
                .FindAll(m => ((dynamic)m.TargetId).Value == entity.Id.Value);
        }
    }
}

