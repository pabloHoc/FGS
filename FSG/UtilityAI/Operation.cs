using System;
using FSG.Core;

namespace FSG.UtilityAI
{
	public abstract class Operation<Context, Target> where Context : IState
    {
		protected ServiceProvider _serviceProvider;

        public Operation(ServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public abstract void Execute(Context context, Target target);
	}
}

