using System;
using System.Collections.Generic;

namespace EDBG.GameLogic.Components
{
    public class DiceTray : ICloneable
    {
        public List<Die> Dice { get; private set; }

        public DiceTray()
        {
            Dice = new List<Die>();
        }

        private DiceTray(DiceTray other)
        {
            if (other != null)
            {
                Dice = new List<Die>();
                foreach (Die d in other.Dice)
                {
                    Dice.Add(d.Clone() as Die);
                }
            }
        }

        public void SetDice(int amount)
        {
            Dice.Clear();
            for (int i = 0; i < amount; i++)
            {
                Dice.Add(new Die());
            }
        }

        public void RollAllDice()
        {
            if (Dice.Count > 0)
            {
                foreach (Die d in Dice)
                {
                    d.Roll();
                }
            }
        }

        public void RollDie(Die d)
        {
            if (Dice.Contains(d))
            {
                d.Roll();
            }
        }

        public object Clone()
        {
            return new DiceTray(this);
        }


    }

}
