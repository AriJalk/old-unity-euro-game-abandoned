using EDBG.Commands;
using EDBG.GameLogic.Core;
using EDBG.GameLogic.MapSystem;
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
            if (tile != null && LogicState.CurrentPlayer.DiscStock > 0)
            {
                PlaceDiscCommand command = new PlaceDiscCommand(LogicState.CurrentPlayer, tile, gameManager.EngineManager.VisualManager.ObjectsRenderer);
                commandStack.Push(command);
                command.ExecuteCommand();
                gameManager.GameMessageEvent?.Invoke($"Confirm Action");
                LogicState.RoundState = RoundStates.Confirm;
            }

            if (tile != null)
            {
                
            }
        }



        //TODO: revert to correct state
        public void UndoState()
        {
            if (commandStack.Count > 0)
            {
                CommandBase command = commandStack.Pop();
                command.UndoCommand();
                //TODO: dont swap if dont need to
                LogicState.SetCurrentPlayer(command.ActivePlayer);
                gameManager.GameMessageEvent.Invoke($"{LogicState.CurrentPlayer.Name} turn, {LogicState.RoundState}");
            }
        }

    }
}