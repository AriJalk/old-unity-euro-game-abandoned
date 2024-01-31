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

        Player player;

        public MapTile MapTile { get; private set; }

        public Disc Disc { get; private set; }

        public PlaceDiscLogic(MapTile mapTile, Player player)
        {
            MapTile = mapTile;
            this.player = player;
        }

        public override void ExecuteCommand()
        {
            Disc = TileRulesLogic.AddDiscToTile(MapTile, player);
        }

        public override void UndoCommand()
        {
            TileRulesLogic.RemoveTopDiscFromTile(MapTile);
            player.DiscStock++;
        }
    }
}
