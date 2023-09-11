using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;
using ResourcePool;
using Unity.VisualScripting;
using System.Linq;

namespace EDBG.Rules
{
    public class GameManager : MonoBehaviour
    {
        private GameEngineManager engineManager;

        private Stack<LogicGameState> gameStateStack;


        public Transform gameWorld;

        public CameraController CameraController;

        void Start()
        {
            CameraController.Initialize();
            CreateTestGame();


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

        //TODO: builder class
        void CreateTestGame()
        {
            gameStateStack = new Stack<LogicGameState>();

            MapGrid mapGrid = new MapGrid(4, 4);

            int[] array = new[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

            UtilityFunctions.PrintArray(array);

            UtilityFunctions.ShuffleArray(array);

            UtilityFunctions.PrintArray(array);

            Stack<int> faces = new Stack<int>(array);
            UtilityFunctions.PrintStack(faces);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    MapTile tile = new MapTile(new GamePosition(i, j), faces.Pop());
                    if (i == 0 && j == 0)
                    {
                        tile.ComponentOnTile = new GameStack<Disc>();
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.Black));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.Black));
                    }
                    else if (i == 3 && j == 3)
                    {
                        tile.ComponentOnTile = new GameStack<Disc>();
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.White));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.White));
                    }
                    mapGrid.SetCell(tile);
                }
            }

            gameStateStack.Push(new LogicGameState(mapGrid));
            UtilityFunctions.PrintStack(faces);
        }
    }
}
