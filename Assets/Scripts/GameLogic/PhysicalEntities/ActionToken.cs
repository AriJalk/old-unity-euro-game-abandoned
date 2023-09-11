
public class ActionToken : IGameComponent
{
    public EDBG.Rules.TokenColors color;

    public ActionToken(EDBG.Rules.TokenColors color)
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
