using UnityEngine;
using EDBG.GameLogic.Rules;

namespace EDBG.GameLogic.Components
{
    public class Die : IGameComponent
    {
        private int _result;

        public int Result
        {
            get
            { 
                return _result;
            }

            private set
            {
                if (value >= 1 && value <= 6)
                {
                    _result = value;
                }
                else
                {
                    Debug.LogWarning("Die value not in range 1-6");
                }
            }
        }

        private PieceColors _color;

        public PieceColors Color
        {
            get { return _color; }
            set { _color = value; }
        }


        public Die()
        {
            _result = 1;
            _color = PieceColors.White;
        }

        //Copy Constructor
        private Die(Die die)
        {
            _result = die.Result;
            _color = die.Color;
        }

        public Die(int face, PieceColors color)
        {
            _result = face;
            _color = color;
        }

        public int Roll()
        {
            _result = Random.Range(1, 7);
            return _result;
        }

        public void ChangeValue(int value)
        {
            Result = value;
        }

        public override object Clone()
        {
            return new Die(this);
        }
    }
}
