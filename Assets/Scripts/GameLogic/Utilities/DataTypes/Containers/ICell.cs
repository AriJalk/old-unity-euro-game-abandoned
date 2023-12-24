using System;
using Unity.VisualScripting;

namespace EDBG.Utilities.DataTypes
{
    public interface ICell : ICloneable
    {
        public GamePosition GamePosition { get; set; }

    }
}