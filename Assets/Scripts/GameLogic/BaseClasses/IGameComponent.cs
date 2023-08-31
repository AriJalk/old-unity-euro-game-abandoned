using System;

public abstract class IGameComponent : ICloneable
{
    public abstract object Clone();
}