using UnityEditor.Experimental.GraphView;
/// <summary>
/// Class for actions which add a bonus
/// </summary>
public abstract class GameActionAdderBase : GameActionBase
{
    public readonly PlayerBase Player;
    public readonly int Bonus;

    protected GameActionAdderBase(string name, string description, PlayerBase player, int bonus) : base(name, description)
    {
        Player = player;
        Bonus = bonus;
    }
}