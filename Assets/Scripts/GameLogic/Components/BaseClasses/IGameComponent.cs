using EDBG.GameLogic.Rules;
using System;

namespace EDBG.GameLogic.Components
{
    public abstract class IGameComponent : ICloneable
    {
        public abstract Player Owner { get; }
        public abstract object Clone();
    }
}
