using System;

public abstract class GameComponent : ICloneable
{
    public abstract object Clone();
}