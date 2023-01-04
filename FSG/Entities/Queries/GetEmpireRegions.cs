using System;
using FSG.Core;

namespace FSG.Entities.Queries
{
    public class GetEmpireRegions : IQuery<Region>
    {
        private readonly EntityId<Empire> _empireId;

        public GetEmpireRegions(EntityId<Empire> empireId)
        {
            _empireId = empireId;
        }

        public Predicate<Region> GetPredicate()
        {
            return (region => region.EmpireId.Equals(_empireId));
        }
    }
}

