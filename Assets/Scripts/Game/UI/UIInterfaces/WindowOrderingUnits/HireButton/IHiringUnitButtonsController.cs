using System.Collections.Generic;

public interface IHiringUnitButtonsController : IInitialize
{
    List<IHiringUnitButton> HiringButtons { get; }
}
