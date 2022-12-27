namespace FSG.Entities
{
    public class Army : IEntity<Army>
    {
        public string Type { get => "ARMY"; }

        public EntityId<Army> Id { get; init; }

        public int Size { get; init; }

        public int Attack { get; init; }

        public int Defense { get; init; }

        public EntityId<Empire> EmpireId { get; init; }

        public EntityId<Region> RegionId { get; init; }
    }
}