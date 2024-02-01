using EDBG.Commands;
using EDBG.Director;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EDBG.GameLogic.Actions
{
    public class PlaceDiscsLogic : CommandBase
    {
        public const string NAME = "Place Discs";

        public MapTile MapTile { get; private set; }

        public List<Disc> DiscList { get; private set; }

        public PlaceDiscsLogic(LogicState state, MapTile mapTile) : base(state)
        {
            MapTile = mapTile;
            DiscList = new List<Disc>();
        }

        public override void ExecuteCommand()
        {
            if(MapTile.GetOwner() != logicState.GetOtherPlayer())
            {
                Result = true;
                List<MapTile> tiles = TileRulesLogic.GetTilesWithComponentInAllDirections(logicState.MapGrid, MapTile, ActivePlayer, true, true);
                int excess;
                if (tiles.Count == 0 && MapTile.DiscStack.Count == 0)
                    excess = 1;
                else
                    excess = tiles.Count - MapTile.DiscStack.Count;
                while(excess > 0)
                {
                    DiscList.Add(TileRulesLogic.AddDiscToTile(MapTile, ActivePlayer));
                    excess--;
                }
            }
        }

        public override void UndoCommand()
        {
            base.UndoCommand();
            int discCount = DiscList.Count;
            while(discCount > 0)
            {
                TileRulesLogic.RemoveTopDiscFromTile(MapTile);
                ActivePlayer.DiscStock++;
                discCount--;
            }
        }
    }
}
