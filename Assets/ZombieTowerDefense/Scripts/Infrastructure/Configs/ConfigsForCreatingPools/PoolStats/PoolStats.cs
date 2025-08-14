public abstract class PoolStats
{
    public abstract int PoolSize { get; }
    public abstract bool CanExpand { get; }
}
