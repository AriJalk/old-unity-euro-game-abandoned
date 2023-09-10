using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;
using ResourcePool;

namespace EDBG.Rules
{
    public class GameManager : MonoBehaviour
    {
        private Stack<LogicGameState> gameStateStack;

        private readonly Player[] matchPlayers;

        public Transform gameWorld;

        void Start()
        {

            gameStateStack = new Stack<LogicGameState>();
            gameStateStack.Push(new LogicGameState(new MapGrid()));

            //TODO: better object finding
            GameEngineManager.Instance.MapRenderer.RenderMap(gameStateStack.Peek().mapGrid, gameWorld.Find("SquareMapHolder"));
        }

    }
}
