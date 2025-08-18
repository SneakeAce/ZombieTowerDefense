using UnityEngine;

public interface IUnitBuilder
{
    T Build<T>(IFactoryArguments unitSpawnArgs) where T : Object;
}
