using System.Collections.Generic;

using UnityEngine;

using EDBG.Engine.Core;
using EDBG.Engine.Visual;

using EDBG.Utilities.DataTypes;

using EDBG.GameLogic.Rules;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Components;
using EDBG.GameLogic.GameStates;

namespace EDBG.GameLogic.Core
{
    /// <summary>
    /// Central class for handling the logic side of the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        private GameEngineManager engineManager;
        private Stack<GameLogicState> gameLogicStateStack;
        private IGameState currentGameState;

        public Transform GameWorld;
        public CameraController CameraController;
        public Camera DiceCamera;
        public GameUI GameUI;
        public DiceTrayObject DiceTrayObject;

        private GameLogicState currentGameLogicState
        {
            get
            {
                return gameLogicStateStack.Peek();
            }
        }

        void Start()
        {

            engineManager = GameEngineManager.Instance;
            CameraController.Initialize();
            LoadPrefabs();
            CreateTestGame();

            //MoveDiscAction moveDisc = new MoveDiscAction();
            //moveDisc.SetAction((MapTile)currentGameLogicState.MapGrid.GetCell(0, 0), (MapTile)currentGameLogicState.MapGrid.GetCell(1, 1), 3, currentGameLogicState);
            //moveDisc.ExecuteAction();

            //Draw map at head of stack

            engineManager.MapRenderer.RenderMap(currentGameLogicState.MapGrid, GameWorld.Find("SquareMapHolder"));
            engineManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);
            engineManager.ScreenManager.ScreenChanged += ScreenChanged;
            currentGameState = new ChooseDie();
            
        }

        /// <summary>
        /// Called when the screen size changed due to orientation change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenChanged(object sender, ScreenChangedEventArgs e)
        {
            CameraController.UpdateAspectRatio(e.NewWidth, e.NewHeight);
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
            Ray ray = DiceCamera.ScreenPointToRay(position);
            LayerMask layerMask;
            switch (currentGameState.Name)
            {
                case "ChooseDie":
                    layerMask = LayerMask.GetMask("Die");
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.transform.GetComponent<DieObject>() is DieObject die)
                        {
                            currentGameState.Update(die);
                            die.transform.localScale = die.transform.localScale * 2f;
                        }
                    }
                    break;
            }
        }

        void ZoomCamera(float deltaY)
        {
            CameraController.ZoomCamera(deltaY / 5);
        }

        //TODO: builder class
        void CreateTestGame()
        {
            gameLogicStateStack = new Stack<GameLogicState>();

            MapGrid mapGrid = new MapGrid(4, 4);

            int[] array = new[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6 };

            EDBG.Utilities.UtilityFunctions.ShuffleArray(array);

            Stack<int> faces = new Stack<int>(array);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    MapTile tile = new MapTile(new GamePosition(i, j), faces.Pop());
                    tile.ComponentOnTile = new GameStack<Disc>();
                    if (i == 0 && j == 0)
                    {
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.Black));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.Black));
                    }
                    else if (i == 3 && j == 3)
                    {
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.White));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(DiscColors.White));
                    }
                    mapGrid.SetCell(tile);
                }
            }

            GameLogicState newState = new GameLogicState(mapGrid);

            newState.PlayerList = new List<Player>() {
                new Player("Player", 10, new BeginnerCorporation(Ownership.Player)),
                new Player("Bot", 10, new BeginnerCorporation(Ownership.Bot)),
            };
            newState.DiceTray = new DiceTray();
            newState.DiceTray.SetDice(5);
            newState.DiceTray.RollAllDice();
            gameLogicStateStack.Push(newState);

            //TODO: index
            GameUI.Initialize(newState.PlayerList[0], newState.PlayerList[1]);
            DiceTrayObject.SetDice(newState.DiceTray, engineManager.PrefabManager);
        }

        private void LoadPrefabs()
        {
            //TODO: replace with proper asset loading
            GameObject squarePrefab = Resources.Load<GameObject>("Prefabs/3D/SquareTilePrefab");
            if (squarePrefab == null)
            {
                Debug.Log("Square Tile Prefab is not found in Resources/Prefabs/3D/SquareTilePrefab.");
                return;
            }
            engineManager.PrefabManager.RegisterPrefab<SquareTileObject>(squarePrefab);

            GameObject discPrefab = Resources.Load<GameObject>("Prefabs/3D/DiscPrefab");
            if (discPrefab == null)
            {
                Debug.Log("Square Tile Prefab is not found in Resources/Prefabs/3D/DiscPrefab.");
                return;
            }
            engineManager.PrefabManager.RegisterPrefab<DiscObject>(discPrefab);

            GameObject diePrefab = Resources.Load<GameObject>("Prefabs/3D/DiePrefab");
            if(diePrefab == null)
            {
                Debug.Log("Die Prefab is not found in Resources/Prefabs/3D/DiePrefab");
                return;
            }
            engineManager.PrefabManager.RegisterPrefab<DieObject>(diePrefab);
        }
    }
}
