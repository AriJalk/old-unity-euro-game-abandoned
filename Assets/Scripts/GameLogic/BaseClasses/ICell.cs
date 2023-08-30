using System;

public interface ICell : ICloneable
    {
        public GamePosition GamePosition { get; set; }
    }
