using System;
using UnityEngine;

public interface IUnitHiringController : IInitialize
{
    IUnitHiringButtonsController UnitHiringButtonsController { get; }
    void SetSpawnPositionProvider(Func<Vector3> getPosition);
}
