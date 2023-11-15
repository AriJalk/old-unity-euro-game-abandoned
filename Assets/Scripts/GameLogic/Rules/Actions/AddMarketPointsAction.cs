using System.Collections.Generic;

namespace EDBG.GameLogic.Rules
{
    public class AddMarketPointsAction : CorpAction
    {
        private int _bonus;

        public int Bonus
        {
            get { return _bonus; }
            private set { _bonus = value; }
        }

        private Player _targetPlayer;

        public Player TargetPlayer
        {
            get { return _targetPlayer; }
            private set { _targetPlayer = value; }
        }
        public AddMarketPointsAction(int dieFace, int bonus)
        {
            DieFaces = new List<int>();
            DieFaces.Add(dieFace);
            Name = $"+{bonus}MP";
            Description = $"Add {bonus} Market Points to player to use in current turn";
            Bonus = bonus;
        }

        public AddMarketPointsAction(List<int> dieFaces, int bonus)
        {
            DieFaces = dieFaces;
            Name = $"+{bonus}MP";
            Description = $"Add {bonus} Market Points to player to use in current turn";
            Bonus = bonus;
        }

        public override void ExecuteAction()
        {
            TargetPlayer.MarketPoints += Bonus;
            CanExecute = false;
        }

        public override void SetAction(Player player)
        {
            TargetPlayer = player;
            CanExecute = true;
        }
    }
}