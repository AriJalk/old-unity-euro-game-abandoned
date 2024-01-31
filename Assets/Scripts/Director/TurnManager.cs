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

        //TODO: further choice logic
        public void SelectTile(MapTile tile)
        {
            if(tile != null && LogicState.CurrentPlayer.DiscStock > 0)
            {
                PlaceDiscCommand command = new PlaceDiscCommand(tile, LogicState.CurrentPlayer, gameManager.EngineManager.VisualManager.ObjectsRenderer);
                commandStack.Push(command);
                command.ExecuteCommand();
                LogicState.SwapCurrentPlayer();
                gameManager.GameMessageEvent.Invoke($"{LogicState.CurrentPlayer.Name} turn, select tile");
            }
        }


        public void UndoState()
        {
            if (commandStack.Count > 0)
            {
                CommandBase command = commandStack.Pop();
                command.UndoCommand();
                //TODO: dont swap if dont need to
                LogicState.SwapCurrentPlayer();
            }
        }

    }
}