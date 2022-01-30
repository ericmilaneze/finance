namespace Finance.Framework
{
    public abstract class AggregateRoot<TId> : Entity<TId>
        where TId : struct
    {
        private readonly List<IEvent> _events = new();

        public IReadOnlyList<IEvent> Events =>
            _events.AsReadOnly();

        protected AggregateRoot(TId id)
            : base(id)
        {
        }

        protected void RaiseEvents(params IEvent[] events)
        {
            foreach (var ev in events?.Where(ev => ev is not null) ?? Enumerable.Empty<IEvent>())
            {
                _events.Add(ev);
                HandleEvent(ev);
                ValidateState();
            }
        }

        protected void RaiseEvent(IEvent @event) =>
            RaiseEvents(@event);

        protected abstract void ValidateState();

        public abstract void HandleEvent(IEvent @event);

        public void HandleEvents(params IEvent[] events)
        {
            foreach (var ev in events?.Where(ev => ev is not null) ?? Enumerable.Empty<IEvent>())
                HandleEvent(ev);
        }

        public void ClearEvents() =>
            _events.Clear();
    }

    public abstract class AggregateRoot : AggregateRoot<Guid>
    {
        protected AggregateRoot(Guid id)
            : base(id)
        {
        }

        protected AggregateRoot()
            : base(Guid.NewGuid())
        {
        }
    }
}