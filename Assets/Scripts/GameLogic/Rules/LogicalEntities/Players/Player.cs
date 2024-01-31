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

        private PlayerColors _playerColor;

        public PlayerColors PlayerColor
        {
            get { return _playerColor; }
            set { _playerColor = value; }
        }


        private Corporation _corporation;

        public Corporation Corporation
        {
            get { return _corporation; }
            set { _corporation = value; }
        }


        private int _initialDiscStock;

        public int InitialDiscStock
        {
            get { return _initialDiscStock; }
            set
            {
                _initialDiscStock = value;
            }
        }

        private int _discStock;

        public int DiscStock
        {
            get { return _discStock; }
            set { _discStock = value; }
        }



        public Player(string name, PlayerColors color, int discStock, Corporation corporation)
        {
            _name = name;
            _playerColor = color;
            _initialDiscStock = discStock;
            _corporation = corporation;
            _discStock = InitialDiscStock;
        }


        private Player(Player other)
        {
            _name = other._name;
            _playerColor = other._playerColor;
            _initialDiscStock = other._initialDiscStock;
            _corporation = other._corporation;
            DiscStock = other._discStock;
        }

        public virtual object Clone()
        {
            return new Player(this);
        }
    }

}
