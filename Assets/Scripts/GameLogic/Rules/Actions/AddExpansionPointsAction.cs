using System.Collections.Generic;

namespace EDBG.GameLogic.Rules
{
    public class AddExpansionPointsAction : CorpAction
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

        public AddExpansionPointsAction(int dieFace,int bonus) 
        {
            DieFaces = new List<int>();
            DieFaces.Add(dieFace);
            Name = $"+{bonus}EP";
            Description = $"Add {bonus} Expansion Points to player to use in current turn";
            Bonus = bonus;
        }
        public AddExpansionPointsAction(List<int> dieFaces, int bonus)
        {
            DieFaces = dieFaces;
            Name = $"+{bonus}EP";
            Description = $"Add {bonus} Expansion Points to player to use in current turn";
            Bonus = bonus;
        }


        public override void ExecuteAction()
        {
            TargetPlayer.ExpansionPoints += Bonus;
            CanExecute = false;
        }

        public override void SetAction(Player player)
        {
            TargetPlayer = player;
            CanExecute = true;
        }
    }
}