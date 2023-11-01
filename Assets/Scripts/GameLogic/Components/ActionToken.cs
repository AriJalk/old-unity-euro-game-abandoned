namespace EDBG.GameLogic.Components
{
    public class ActionToken : IGameComponent
    {
        public EDBG.GameLogic.Rules.TokenColors color;

        public ActionToken(EDBG.GameLogic.Rules.TokenColors color)
        {
            this.color = color;
        }

        private ActionToken(ActionToken other)
        {
            color = other.color;
        }

        public override object Clone()
        {
            return new ActionToken(this);
        }
    }

}
