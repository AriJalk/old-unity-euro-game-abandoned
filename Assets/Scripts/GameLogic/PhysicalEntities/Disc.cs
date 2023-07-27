using EDBG.Rules;
public class Disc
{


    public Colors DiscColor
    {
        get; private set;
    } 

    public Disc(Disc otherDisc)
    {
        DiscColor = otherDisc.DiscColor;
    }

    public Disc(Colors discColor)
    {
        DiscColor = discColor;
    }


}
