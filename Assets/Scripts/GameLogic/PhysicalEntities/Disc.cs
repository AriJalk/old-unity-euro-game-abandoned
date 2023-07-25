using EDBG.Rules;
public class Disc
{


    public ComponentColor DiscColor
    {
        get; private set;
    } 

    public Disc(Disc otherDisc)
    {
        DiscColor = otherDisc.DiscColor;
    }

    public Disc(ComponentColor discColor)
    {
        DiscColor = discColor;
    }


}
