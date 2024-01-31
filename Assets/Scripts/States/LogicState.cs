using System;
using System.Collections.Generic;

using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;

namespace EDBG.States
{
    public enum RoundStates
    {
        GameStart,
        ChooseTile,
        ChooseStack,
        ChooseDisc,
        ChooseDemandTile,
        ChooseUpgrade,
        BotTurn,
    }

    public class LogicState : ICloneable
    {
        private RoundStates _roundState;

        public RoundStates RoundState
        {
            get { return _roundState; }
            set 
            { 
                _roundState = value;

            }
        }

        public byte CurrentPlayerIndex {  get; set; }
        public Player CurrentPlayer
        {
            get
            {
                return PlayerList[CurrentPlayerIndex];
            }
        }
        public MapGrid MapGrid { get; set; }

        public List<Player> PlayerList { get; set; }

        public MapTile TargetTile { get; set; }

        public LogicState(MapGrid mapGrid, params Player[] players)
        {
            MapGrid = mapGrid;
            PlayerList = new List<Player>();
            foreach(Player player in players)
            {
                PlayerList.Add(player);
            }
            RoundState = RoundStates.ChooseTile;
        }

        private LogicState(LogicState other)
        {
            if (other != null)
            {
                if (other.MapGrid != null)
                {
                    MapGrid = (MapGrid)other.MapGrid.Clone();
                }
                CurrentPlayerIndex = other.CurrentPlayerIndex;
                if (other.PlayerList != null)
                {
                    PlayerList = new List<Player>();
                    foreach(Player player in other.PlayerList)
                    {
                        PlayerList.Add((Player)player.Clone());
                    }
                }
                RoundState = other.RoundState;
                TargetTile = other.TargetTile;
            }
        }

        public object Clone()
        {
            return new LogicState(this);
        }

        public Player GetCurrentPlayer()
        {
            return PlayerList[CurrentPlayerIndex];
        }

        public Player GetOtherPlayer()
        {
            return CurrentPlayerIndex == 0 ? PlayerList[1] : PlayerList[0];
        }

        public void SwapCurrentPlayer()
        {
            CurrentPlayerIndex = CurrentPlayerIndex == (byte)0 ? (byte)1 : (byte)0;
        }

    }
}
