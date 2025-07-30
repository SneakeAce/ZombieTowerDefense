using System.Collections.Generic;

public interface IInitializer
{
    void Initialize(List<IInitialize> initializeList);
    void Initialize(IInitialize initialize);
}
