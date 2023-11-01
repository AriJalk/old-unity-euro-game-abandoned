using System;

namespace EDBG.GameLogic.Components
{
    public abstract class IGameComponent : ICloneable
    {
        public abstract object Clone();
    }
}
