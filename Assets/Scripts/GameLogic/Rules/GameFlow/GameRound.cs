using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.States;
using TMPro;

namespace EDBG.GameLogic.Rules
{
    public enum RoundStates
    {
        ChooseTile,
        ChooseDisc,
        ChooseDemandTile,
        ChooseUpgrade,
        BotTurn,
    }

    public class GameRound
    {
        private RoundStates _roundState;

        public RoundStates RoundState
        {
            get { return _roundState; }
            private set { _roundState = value; }
        }

        private LogicState _logicState;

        public LogicState LogicState
        {
            get { return _logicState; }
            private set { _logicState = value; }
        }

        private MapTile _chosenTile;

        public MapTile ChosenTile
        {
            get { return _chosenTile; }
            private set { _chosenTile = value; }
        }

        private MapTile _chosenBonusTile;

        public MapTile ChosenBonusTile
        {
            get { return _chosenBonusTile; }
            private set { _chosenBonusTile = value; }
        }


        public void StartRound(LogicState logicState)
        {
            _logicState = logicState;
            _roundState = RoundStates.ChooseTile;
        }

        public void SetTile(MapTile tile, Player player)
        {
            if (tile != null)
            {
                if (tile.ComponentOnTile == null)
                {
                    tile.ComponentOnTile = new GameStack<Disc>();
                    GameStack<Disc> stack = (GameStack<Disc>)tile.ComponentOnTile;
                    PlayerStateData data = LogicState.GetPlayerState(player);
                    data.DiscStock--;
                    stack.PushItem(new Disc(player));
                }
            }
        }
    }
}