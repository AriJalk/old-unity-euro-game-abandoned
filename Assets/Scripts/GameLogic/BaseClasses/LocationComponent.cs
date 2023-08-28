using EDBG.Rules;

public class LocationComponent
{
    public GameComponent Component { get; set; }

    public Ownership Ownership { get; set; }

    public LocationComponent(GameComponent component, Ownership ownership)
    {
        Component = component;
        Ownership = ownership;
    }
}