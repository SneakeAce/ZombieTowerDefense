using System.Collections.Generic;

public class Initializer : IInitializer
{
    public void Initialize(List<IInitialize> initializeList)
    {
        for (int i = 0; i < initializeList.Count; i++)
        { 
            IInitialize initialize = initializeList[i];

            initialize.Initialize();
        }
    }

    public void Initialize(IInitialize initialize)
    {
        initialize.Initialize();
    }
}
