namespace Finance.Framework
{
    public interface IEventTypeResolver
    {
        Type Resolve(string eventType);
    }
}