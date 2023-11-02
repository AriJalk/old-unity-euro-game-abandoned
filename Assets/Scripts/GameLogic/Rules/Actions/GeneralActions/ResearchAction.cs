
namespace EDBG.GameLogic.Rules
{
    public class ResearchAction : GameActionAdder
    {
        const string ActionDescription = "Remove X Discs from a Player's Disc stock";

        public ResearchAction(int bonus) : base("Research", ActionDescription, bonus) { }

        public override void ExecuteAction()
        {
            player.DiscStock -= Bonus;
            CanExecute = false;
        }

        public override void SetAction(Player player)
        {
            base.SetAction(player);
            if (player.DiscStock > 0)
                CanExecute = true;
        }
    }
}
