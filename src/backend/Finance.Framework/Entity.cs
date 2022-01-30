namespace Finance.Framework
{
    public abstract class Entity<TId>
        where TId : struct
    {
        public TId Id { get; }

        protected Entity(TId id)
        {
            Id = id;
        }
    }

    public abstract class Entity : Entity<Guid>
    {
        protected Entity(Guid id)
            : base(id)
        {
        }

        protected Entity()
            : base(Guid.NewGuid())
        {
        }
    }
}