
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace EDBG.GameLogic.Rules
{
    /// <summary>
    /// Corporation defines the actions, starting material, and corp specific functionality available to the player in a game
    /// </summary>
    public abstract class Corporation : ICloneable
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }


        public Corporation()
        {

        }

        public object Clone()
        {
            Corporation clone = (Corporation)this.MemberwiseClone();
            clone._name = this._name;
            clone._description = this._description;
            return clone;
        }
    }

}
