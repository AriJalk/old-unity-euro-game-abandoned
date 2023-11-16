using System;

namespace EDBG.GameLogic.Rules
{
    /// <summary>
    /// Base class for player
    /// </summary>
    public class Player : ICloneable
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Corporation _corporation;

        public Corporation Corporation
        {
            get { return _corporation; }
            set { _corporation = value; }
        }


        private int _discStock;

        public int DiscStock
        {
            get { return _discStock; }
            set
            {
                _discStock = value;
            }
        }


        public Player(string name, int discStock, Corporation corporation)
        {
            _name = name;
            _discStock = discStock;
            _corporation = corporation;
        }


        private Player(Player other)
        {
            _name = other._name;
            _discStock = other._discStock;
            _corporation = other._corporation;
        }

        public virtual object Clone()
        {
            Player clone = (Player)this.MemberwiseClone();
            clone._name = this._name;
            clone._discStock = this._discStock;
            clone._corporation = this._corporation;
            return clone;
        }
    }

}
