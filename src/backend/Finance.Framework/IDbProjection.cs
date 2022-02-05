namespace Finance.Framework
{
    public interface IDbProjection<TId>
        where TId : struct
    {
        Task ProjectEventsAsync(EventRecord<TId>[] eventRecords, CancellationToken cancellationToken = default);
    }

    public interface IDbProjection : IDbProjection<Guid>
    {
    }
}