using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;
using ResourcePool;

namespace EDBG.Rules
{
    public class GameManager : MonoBehaviour
    {
        private GameEngineManager engineManager;

        private Stack<LogicGameState> gameStateStack;

        private readonly Player[] matchPlayers;

        public Transform gameWorld;

        public CameraController CameraController;

        void Start()
        {
            CameraController.Initialize();
            gameStateStack = new Stack<LogicGameState>();
            gameStateStack.Push(new LogicGameState(new MapGrid()));

            //TODO: better object finding
            engineManager = GameEngineManager.Instance;
            engineManager.MapRenderer.RenderMap(gameStateStack.Peek().mapGrid, gameWorld.Find("SquareMapHolder"));
            engineManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);

        }

        void Update()
        {
            engineManager.InputHandler.Listen();
        }

        void MoveCamera(Vector2 axis)
        {
            CameraController.MoveCamera(axis.x, axis.y);
        }

        void SelectObject(bool[] mouseButtons, Vector2 position)
        {

        }

        void ZoomCamera(float deltaY)
        {
            CameraController.ZoomCamera(deltaY / 5);
        }
    }
}
