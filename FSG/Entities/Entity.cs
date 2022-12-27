namespace FSG.Entities
{
    public interface IBaseEntity { }

    public interface IEntity<T> : IBaseEntity where T : IEntity<T>
    {
        public string Type { get; }

        public EntityId<T> Id { get; }
    }

    public interface IEntityWithTurns<T> : IEntity<T> where T : IEntity<T>
    {
        public int RemainingTurns { get; set; }
    }
}