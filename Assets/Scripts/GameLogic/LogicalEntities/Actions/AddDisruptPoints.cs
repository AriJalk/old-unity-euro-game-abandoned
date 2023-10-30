public class AddDisruptPoints : GameActionAdderBase
{
    public AddDisruptPoints(PlayerBase player, int bonus) : base(player, bonus)
    {
        Name = $"+{bonus}DP";
        Description = "Add disrupt points to use in the current turn";
    }

    public override void ExecuteAction()
    {
        Player.DisruptPoints += Bonus;
    }

    public override void UpdateCanExecute()
    {
        throw new System.NotImplementedException();
    }
}