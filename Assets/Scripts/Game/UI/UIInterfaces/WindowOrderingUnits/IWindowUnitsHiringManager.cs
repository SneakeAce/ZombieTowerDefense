using System;
using UnityEngine;

public interface IWindowUnitsHiringManager : IUIManager
{
    public Canvas WindowOrderingCanvas { get; }

    event Action<WindowUnitsHiringView> BindWindowDone;
}
