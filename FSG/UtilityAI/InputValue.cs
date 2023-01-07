using System;

namespace FSG.UtilityAI
{
	public class InputValue<Context, Target> where Context : IState
	{
		private readonly Context _context;

		private readonly Target _target;

		public InputValue(Context context, Target target)
		{
			_context = context;
			_target = target;
		}

		public int GetFrom(IInputParameter inputParam)
		{
			return _context.getInputValue(_target, inputParam.Name);
		}
	}
}

