using System.Collections.Generic;

public interface IHiringUnitButtonsController
{
    List<IHiringUnitButton> HiringButtons { get; }

    void Initialize();
}
