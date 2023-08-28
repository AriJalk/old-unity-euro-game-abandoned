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
}
