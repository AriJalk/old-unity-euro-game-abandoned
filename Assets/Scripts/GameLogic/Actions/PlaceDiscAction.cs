using EDBG.Director;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EDBG.GameLogic.Actions
{
    public abstract class PlaceDiscAction : ActionBase
    {
        const string name = "Place Disc";
        const string animationName = "PlaceDisc";

        DirectorCore director;
        MapTile tile;
        Player player;


        public override string Name
        {
            get
            {
                return name;
            }
            protected set
            {
                Name = value;
            }
        }

        public PlaceDiscAction(DirectorCore director, MapTile tile, Player player)
        {
            this.director = director;
            this.tile = tile;
            this.player = player;
        }

        public override void ExecuteAction()
        {
            GameStack<Disc> stack = tile.ComponentOnTile as GameStack<Disc>;
            if (stack == null)
                stack = new GameStack<Disc>();
            stack.PushItem(new Disc(player));
            
        }
    }
}
