using EDBG.Commands;
using EDBG.Director;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EDBG.GameLogic.Actions
{
    public class PlaceDiscLogic : CommandBase
    {
        public const string NAME = "Place Disc";

        public MapTile MapTile { get; private set; }

        public Disc Disc { get; private set; }

        public PlaceDiscLogic(Player activePlayer, MapTile mapTile) : base(activePlayer)
        {
            MapTile = mapTile;
        }

        public override void ExecuteCommand()
        {
            Disc = TileRulesLogic.AddDiscToTile(MapTile, ActivePlayer);
        }

        public override void UndoCommand()
        {
            TileRulesLogic.RemoveTopDiscFromTile(MapTile);
            ActivePlayer.DiscStock++;
        }
    }
}
