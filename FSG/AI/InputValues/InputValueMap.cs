using System;
using System.Collections.Generic;
using FSG.Core;
using FSG.Entities;

namespace FSG.AI
{
	public interface IInputValue {
        public int GetValue(IBaseEntity entity);
    }

	public class InputValueMap
	{
		private readonly GameState _context;

		private readonly Dictionary<string, IInputValue> _inputMap;
			

		public InputValueMap(GameState context)
		{
			_context = context;
			_inputMap = new Dictionary<string, IInputValue>
			{
				{ "Grassland", new GrasslandNumberInput(context) }
			};
        }

		public IInputValue Get(string name)
        {
			return _inputMap[name];
		}

	}
}

