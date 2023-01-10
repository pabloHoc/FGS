using FSG.AI;
using FSG.Entities;
using FSG.UtilityAI;

namespace FSG.Core
{
    public class GameState : IState
    {
        private readonly InputValueMap _inputValueMap;

        public EntityRepository Entities { get; } = new EntityRepository();

        public int Turn { get; private set; } = 0;

        public GameState()
        {
            _inputValueMap = new InputValueMap(this);
        }

        public int GetInputValue<T>(T target, string name) 
        {
            return _inputValueMap.Get(name).GetValue((IBaseEntity)target);
        }

        public void nextTurn()
        {
            Turn++;
        }
    }
}