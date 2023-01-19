using System;
using FSG.Core;

namespace FSG.Entities.Queries
{
    public class GetRegionsWithEmpire : IQuery<Region>
    {
        public Predicate<Region> GetPredicate()
        {
            return (region => region.EmpireId != null);
        }
    }
}

