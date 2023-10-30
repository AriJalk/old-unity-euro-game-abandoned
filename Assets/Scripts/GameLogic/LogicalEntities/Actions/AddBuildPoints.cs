public class AddBuildPoints : GameActionAdderBase
{
    public AddBuildPoints(PlayerBase player, int bonus) : base(player, bonus)
    {
        Name = $"+{bonus}BP";
        Description = "Add disrupt points to use in the current turn";
    }

    public override void ExecuteAction()
    {
        Player.BuildPoints += Bonus;
    }

    public override void UpdateCanExecute()
    {
        throw new System.NotImplementedException();
    }


}