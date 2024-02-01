using EDBG.Commands;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.Core;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;
using System.Collections.Generic;
using UnityEngine.Events;

namespace EDBG.Director
{
    public class TurnManager
    {
        private GameManager gameManager;
        //Holds commands to support undo
        private Stack<CommandBase> commandStack;

        public LogicState LogicState { get; private set; }

        public TurnManager(GameManager gameManager, LogicState state) 
        {
            this.gameManager = gameManager;
            LogicState = state;
            commandStack = new Stack<CommandBase>();
        }

        public void Confirm()
        {
            if(LogicState.RoundState == RoundStates.Confirm)
            {
                LogicState.SwapCurrentPlayer();
                LogicState.RoundState = RoundStates.ChooseTile;
                gameManager.GameMessageEvent?.Invoke($"{LogicState.CurrentPlayer.Name} turn, {LogicState.RoundState}");
            }
        }

        //TODO: further choice logic
        public void SelectTile(MapTile tile)
        {
            if (tile != null)
            {
                PlaceDiscsCommand command = new PlaceDiscsCommand(LogicState, tile, gameManager.EngineManager.VisualManager.ObjectsRenderer);
                commandStack.Push(command);
                command.ExecuteCommand();
                if(command.Result == true)
                {
                    LogicState.RoundState = RoundStates.Confirm;
                    gameManager.GameMessageEvent?.Invoke("Confirm Action");
                }
            }
        }
        


                    /*

                    List<MapTile> tiles = TileRulesLogic.GetTilesWithComponentInAllDirections(
                    StateManager.CurrentState.MapGrid,
                           chooseTile.SelectedTile, StateManager.CurrentState.GetCurrentPlayer(), true, true);
                    if (tiles.Count > chooseTile.SelectedTile.GetComponentOnTile<GameStack<Disc>>().Count)
                    {
                        StateManager.PushCurrentState();
                        int excess = tiles.Count - chooseTile.SelectedTile.GetComponentOnTile<GameStack<Disc>>().Count;
                        while (excess > 0)
                        {

                            chooseTile.UpdateState(StateManager.CurrentState);
                            TileRulesLogic.AddDiscToTile(chooseTile);
                            //RenderDisc
                            EngineManager.VisualManager.ObjectsRenderer.PlaceNewDisc(new Disc(StateManager.CurrentState.GetCurrentPlayer()), chooseTile.SelectedTile, MapHolder, true);
                            excess--;
                        }

                        SwapPlayers();

            */


        //TODO: revert to correct state
        public void UndoState()
        {
            if (commandStack.Count > 0)
            {
                CommandBase command = commandStack.Pop();
                command.UndoCommand();
                gameManager.GameMessageEvent.Invoke($"{LogicState.CurrentPlayer.Name} turn, {LogicState.RoundState}");
            }
        }

    }
}