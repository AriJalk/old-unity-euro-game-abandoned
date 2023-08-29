using EDBG.Rules;

public class ComponentOnTile
{
    public GameComponent Component { get; set; }

    public Ownership Ownership { get; set; }

    public ComponentOnTile(GameComponent component, Ownership ownership)
    {
        Component = component;
        Ownership = ownership;
    }
}