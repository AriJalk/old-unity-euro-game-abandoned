using EDBG.GameLogic.Rules;
using System;
using System.Data.SqlTypes;

namespace EDBG.GameLogic.Components
{
    public abstract class IGameComponent : ICloneable, INullable
    {
        public abstract Player Owner { get; }
        public abstract bool IsNull { get; }

        public abstract object Clone();
    }
}
