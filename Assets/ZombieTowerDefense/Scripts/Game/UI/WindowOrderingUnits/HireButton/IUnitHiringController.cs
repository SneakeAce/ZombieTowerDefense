using System;
using UnityEngine;

public interface IUnitHiringController : IInitialize
{
    IUnitHiringButtonsController UnitHiringButtonsController { get; }
    void SetPositionToMoveProvider(Func<Vector3> getPosition);
    void SetSpawnPositionProvider(Func<Vector3> getPosition);
}
