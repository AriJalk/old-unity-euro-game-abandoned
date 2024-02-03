using EDBG.Commands;
using EDBG.Director;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//TODO: move to command
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
                List<MapTile> tiles = TileRulesLogic.GetTilesWithComponentInAllDirections(logicState.MapGrid, MapTile, ActivePlayer, true, true);
                int excess;
                if(logicState.CurrentPlayer.DiscStock > 0)
                {
                    // Place 1 disc if no legal neighbors
                    if (tiles.Count == 0 && MapTile.DiscStack.Count == 0)
                        excess = 1;
                    else
                    {
                        excess = tiles.Count - MapTile.DiscStack.Count;
                        //Make sure no more than available discs could be placed
                        if (excess > logicState.CurrentPlayer.DiscStock)
                            excess = logicState.CurrentPlayer.DiscStock;
                    }
                    if (excess > 0)
                    {
                        Result = true;
                    }
                    while (excess > 0)
                    {
                        DiscList.Add(TileRulesLogic.AddDiscToTile(MapTile, ActivePlayer));
                        excess--;
                    }
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
