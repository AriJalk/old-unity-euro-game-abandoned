using EDBG.Commands;
using EDBG.GameLogic.Core;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Rules;
using EDBG.States;
using System.Collections.Generic;
using UnityEngine;

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
            if (LogicState.RoundState == RoundStates.Confirm)
            {
                LogicState.SwapCurrentPlayer();
                LogicState.RoundState = RoundStates.ChooseTile;
                gameManager.StatusMessageEvent?.Invoke($"{LogicState.CurrentPlayer.Name} turn, {LogicState.RoundState}");
            }
        }

        //TODO: further choice logic
        public void SelectTile(MapTile tile)
        {
            // Empty or player owned tile
            if (tile != null && tile.GetOwner() != LogicState.GetOtherPlayer())
            {
                PlaceDiscsCommand command = new PlaceDiscsCommand(LogicState, tile, gameManager.EngineManager.VisualManager.ObjectsRenderer);
                command.ExecuteCommand();
                if (command.Result == true)
                {
                    commandStack.Push(command);
                    LogicState.RoundState = RoundStates.Confirm;
                    gameManager.StockChangedEvent?.Invoke($"{LogicState.CurrentPlayerIndex + 1}-{LogicState.CurrentPlayer.DiscStock}");
                    gameManager.StatusMessageEvent?.Invoke("Confirm Action");
                }
                else
                {

                }
            }
            // Opponent controlled tile
            else
            {
                ChooseOpponentStackCommand command = new ChooseOpponentStackCommand(LogicState, tile, gameManager.EngineManager.VisualManager.ObjectsRenderer);
                if (command.Result == true)
                {
                    LogicState.RoundState = RoundStates.ChooseCaptureStack;
                    commandStack.Push(command);
                    command.ExecuteCommand();
                    gameManager.StatusMessageEvent?.Invoke($"{LogicState.CurrentPlayer.Name}, choose capture stack");
                }
            }
        }

        public void SelectStack(MapTile tile)
        {
            ChooseOpponentStackCommand command = commandStack.Peek() as ChooseOpponentStackCommand;
            if (command != null)
            {
                command.UndoCommand();
                commandStack.Pop();
                if (command.LegalCaptureStackTiles.Contains(tile))
                {
                    CaptureStackCommand captureCommand = new CaptureStackCommand(
                        LogicState, command.OpponentStack, tile,
                        gameManager.EngineManager.VisualManager.ObjectsRenderer);
                    captureCommand.ExecuteCommand();
                    if (captureCommand.Result == true)
                    {
                        commandStack.Push(captureCommand);
                        LogicState.SwapCurrentPlayer();
                    }

                }
                // Switch target
                else
                {
                    SelectTile(tile);
                }
            }
        }

        public void UndoState()
        {
            if (commandStack.Count > 0)
            {
                CommandBase command = commandStack.Pop();
                command.UndoCommand();
                gameManager.StatusMessageEvent.Invoke($"{LogicState.CurrentPlayer.Name} turn, {LogicState.RoundState}");
                gameManager.StockChangedEvent?.Invoke($"{LogicState.CurrentPlayerIndex + 1}-{LogicState.CurrentPlayer.DiscStock}");
            }
        }

    }
}