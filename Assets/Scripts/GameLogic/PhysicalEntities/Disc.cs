using EDBG.Rules;
public class Disc : GameComponent
{
    public Colors DiscColor
    {
        get; private set;
    }

    public Disc(Colors discColor)
    {
        DiscColor = discColor;
    }

    protected Disc(Disc other)
    {
        DiscColor = other.DiscColor;
    }

    public override object Clone()
    {
        return new Disc(this);
    }
}
