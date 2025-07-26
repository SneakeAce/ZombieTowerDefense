using System;

[Flags]
public enum SceneId
{
    MainMenu = 1 << 1,
    FirstLevel = 1 << 2,
}
