using EDBG.Director;
using UnityEngine;

namespace EDBG.GameLogic.Actions
{
    public abstract class ActionBase
    {

        public virtual string Name { get; protected set; }

        public abstract void ExecuteAction();
    }
}
