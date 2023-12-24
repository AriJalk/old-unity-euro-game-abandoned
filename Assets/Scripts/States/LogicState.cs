using System;
using System.Collections.Generic;

using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.Rules;

namespace EDBG.States
{
    public enum RoundStates
    {
        GameStart,
        ChooseTile,
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
            set { _roundState = value; }
        }

        public byte CurrentPlayerIndex {  get; set; }
        public MapGrid MapGrid { get; set; }

        public List<PlayerStateData> PlayerStateList { get; set; }

        public LogicState(MapGrid mapGrid, params Player[] players)
        {
            MapGrid = mapGrid;
            PlayerStateList = new List<PlayerStateData>();
            foreach(Player player in players)
            {
                PlayerStateList.Add(new PlayerStateData(player));
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
                if (other.PlayerStateList != null)
                {
                    PlayerStateList = new List<PlayerStateData>();
                    foreach(PlayerStateData stateData in other.PlayerStateList)
                    {
                        PlayerStateList.Add((PlayerStateData)stateData.Clone());
                    }
                }
                RoundState = other.RoundState;
            }
        }

        public object Clone()
        {
            return new LogicState(this);
        }

        public Player GetCurrentPlayer()
        {
            return PlayerStateList[CurrentPlayerIndex].Player;
        }

        public Player GetOtherPlayer()
        {
            return CurrentPlayerIndex == 0 ? PlayerStateList[1].Player : PlayerStateList[0].Player;
        }

        public PlayerStateData GetPlayerState(Player player)
        {
            foreach(PlayerStateData stateData in PlayerStateList)
            {
                if(stateData.Player == player)
                {
                    return stateData;
                }
            }
            return null;
        }
    }
}
