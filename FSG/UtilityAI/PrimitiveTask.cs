using System;
using System.Collections.Generic;

namespace FSG.UtilityAI
{
    public class PrimitiveTask<Context, Target> : ITask<Context, Target>
        where Context : IState
    {
        public string Name { get; }

        protected Context _context;

        protected Target _target;

        private readonly List<Scorer> _scorers;

        private readonly double _weight; // weight is the max possible value

        private readonly Func<Context, Target, bool> _validator;

        private readonly Operation<Context, Target> _operation;

        public PrimitiveTask(
            string name,
            double weight,
            Func<Context, Target, bool> validator,
            List<Scorer> scorers,
            Context context,
            Target target,
            Operation<Context, Target> operation = null
        )
        {
            Name = name;
            _weight = weight;
            _validator = validator;
            _scorers = scorers;
            _context = context;
            _target = target;
            _operation = operation;
        }

        // There could be different ways to score scorers:
        // AllOrNothing, FixedScore, SumOfChildThreshold
        public double GetScore()
        {
            var inputValue = new InputValue<Context, Target>(_context, _target);
            var compensationFactor = 1.0f - 1.0f / _scorers.Count; // add momentum and bonus
            var result = _weight;

            foreach (var scorer in _scorers)
            {
                var finalScore = scorer.Score(inputValue); // TODO: add target?
                var modification = (1.0f - finalScore) * compensationFactor;
                finalScore += modification * finalScore;
                result *= finalScore;
            }

            return result;
        }

        public bool IsValid()
        {
            return _validator(_context, _target);
        }

        public void Execute()
        {
            _operation.Execute(_context, _target);
        }
    }
}

