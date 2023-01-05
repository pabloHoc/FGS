using System;
using FSG.Core;
using FSG.Entities;

namespace FSG.Conditions
{
    public interface ICondition { }


    public interface ICondition<T> : ICondition
    {
        public bool IsValid(GameState gameState, T entity);
    }
}

