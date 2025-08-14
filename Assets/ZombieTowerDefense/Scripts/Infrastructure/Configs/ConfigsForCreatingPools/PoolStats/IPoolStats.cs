public interface IPoolStats
{
    int PoolSize { get; }
    bool CanExpand { get; }
}
