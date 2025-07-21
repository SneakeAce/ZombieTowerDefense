using System.Collections.Generic;

public interface IUnitHiringButtonsController : IInitialize
{
    List<IUnitHiringButton> HiringButtons { get; }
}
