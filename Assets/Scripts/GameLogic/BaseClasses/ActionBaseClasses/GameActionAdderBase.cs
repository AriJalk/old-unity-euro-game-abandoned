/// <summary>
/// Class for actions which add a bonus
/// </summary>
public abstract class GameActionAdderBase : GameActionBase
{
    public PlayerBase Player { get; private set; }
    public readonly int Bonus;

    protected GameActionAdderBase(string name, string description, int bonus) : base(name, description)
    {
        Bonus = bonus;
    }

    public void SetAction(PlayerBase player)
    {
        Player = player;
    }
}