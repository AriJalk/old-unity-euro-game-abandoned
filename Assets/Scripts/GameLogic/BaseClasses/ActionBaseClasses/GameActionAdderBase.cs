/// <summary>
/// Class for actions which add a bonus
/// </summary>
public abstract class GameActionAdderBase : GameActionBase
{
    public PlayerBase Player { get; protected set; }
    public int Bonus { get; protected set; }

    public GameActionAdderBase(PlayerBase player, int bonus)
    {
        Player = player;
        Bonus = bonus;
    }
}