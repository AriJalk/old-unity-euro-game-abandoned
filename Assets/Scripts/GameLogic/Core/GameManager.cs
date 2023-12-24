using UnityEngine;
using EDBG.Engine.Core;
using EDBG.Engine.Visual;
using EDBG.GameLogic.Rules;
using EDBG.UserInterface;
using EDBG.States;
using EDBG.GameLogic.MapSystem;
using UnityEngine.UIElements;
using System.Collections.Generic;
using EDBG.Utilities.DataTypes;

namespace EDBG.GameLogic.Core
{
    /// <summary>
    /// Central class for handling the logic side of the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public StateManager StateManager { get; private set; }

        private GameEngineManager engineManager;

        public Transform GameWorld;
        public CameraController MapCamera;
        public Camera TokenCamera;
        public GameUI GameUI;
        public DiceTrayObject DiceTrayObject;
        public Transform MapHolder;


        void Start()
        {
            StateManager = new StateManager();
            engineManager = GameEngineManager.Instance;
            MapCamera.Initialize();
            LoadPrefabs();
            engineManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);
            engineManager.ScreenManager.ScreenChanged += ScreenChanged;

            //Set new game
            HumanPlayer human = new HumanPlayer("HumanPlayer", PlayerColors.Black, 10, new BeginnerCorporation(Ownership.HumanPlayer));
            BotPlayer bot = new BotPlayer("BotPlayer", PlayerColors.White, 10, new BeginnerCorporation(Ownership.BotPlayer));
            StateManager.PushState(GameBuilder.BuildInitialState(4, 4, human, bot));

            //Render map
            RenderGameState();

            GameUI.Initialize(this);

            /* Test tile search
            TileRulesLogic.GetCellsInDirectLine(StateManager.CurrentState.GameLogicState.MapGrid, StateManager.CurrentState.GameLogicState.MapGrid.GetCell(1, 1), false);
            */

            // Test bfs
            bool[,] distanceMap = TileRulesLogic.GetCellsInDistance(StateManager.CurrentState.GameLogicState.MapGrid, StateManager.CurrentState.GameLogicState.MapGrid.GetCell(1, 1), 2);
            Debug.Log(distanceMap);
        }


        void Update()
        {
            engineManager.InputHandler.Listen();
            if (Input.GetKeyDown(KeyCode.U))
            {
                UndoState();
            }
        }

        private void OnDestroy()
        {
            engineManager.InputEvents.UnsubscribeFromAllEvents(MoveCamera, SelectObject, ZoomCamera);
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
            if (diePrefab == null)
            {
                Debug.Log("Die Prefab is not found in Resources/Prefabs/3D/DiePrefab");
                return;
            }
            engineManager.PrefabManager.RegisterPrefab<DieObject>(diePrefab);
        }

        private void ActionSelected(UIAction action)
        {
            if (action.name == "ConfirmAction")
            {
                Debug.Log("Confirm");
            }
            else if (action.name == "UndoAction")
            {
                Debug.Log("Undo");
                UndoState();
            }
            else
            {

            }
        }
        private void MoveCamera(Vector2 axis)
        {
            MapCamera.MoveCamera(axis.x, axis.y);
        }

        /// <summary>
        /// Called when the screen size changed due to orientation change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScreenChanged(object sender, ScreenChangedEventArgs e)
        {
            MapCamera.UpdateAspectRatio(e.NewWidth, e.NewHeight);
        }

        //TODO: move to UI
        private void SelectObject(bool[] mouseButtons, Vector2 position)
        {

            /*
            Transform tile = cameraRaycaster.Raycast(position, LayerMask.GetMask("Disc"));
            if (tile != null)
            {
                Debug.Log(tile.parent.parent.parent.name);
            }
            */
            switch (StateManager.CurrentState.GameLogicState.RoundState)
            {
                case RoundStates.GameStart:
                    break;
                case RoundStates.ChooseTile:
                    if (mouseButtons[0] == true)
                        ChooseTile(position);
                    else if (mouseButtons[1] == true)
                    {

                    }
                    break;
            }
        }

        private void ChooseTile(Vector2 position)
        {
            CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();

            Transform tileTransform = cameraRaycaster.Raycast(position, LayerMask.GetMask("Tile"));
            if (tileTransform != null)
            {
                SquareTileObject tile = tileTransform.GetComponent<SquareTileObject>();
                ChooseTile chooseTile = new ChooseTile(tile.TileData, StateManager.CurrentState.GameLogicState);
                Player owner = chooseTile.SelectedTile.GetOwner();
                // Empty Tile
                if (owner == null)
                {
                    StateManager.PushCurrentState();
                    TileRulesLogic.AddDiscToTile(chooseTile);
                    StateManager.CurrentState.GameLogicState.MapGrid.SetCell(chooseTile.SelectedTile);
                    RenderGameState();
                    SwapPlayers();
                }

                // Player owned tile
                else if (owner == StateManager.CurrentState.GameLogicState.GetCurrentPlayer())
                {
                    if (TileRulesLogic.IsNextFloorPossible(chooseTile.LogicState.MapGrid, chooseTile.SelectedTile, chooseTile.LogicState.GetCurrentPlayer()))
                    {
                        StateManager.PushCurrentState();
                        TileRulesLogic.AddDiscToTile(chooseTile);
                        StateManager.CurrentState.GameLogicState.MapGrid.SetCell(chooseTile.SelectedTile);
                        RenderGameState();
                        SwapPlayers();
                    }    

                }
                // Opponent owned tile
                else if (owner == StateManager.CurrentState.GameLogicState.GetOtherPlayer())
                {
                    List<MapTile> tiles = TileRulesLogic.GetBiggerOpponentStackTiles(chooseTile);
                    foreach(MapTile bigTile in tiles )
                    {
                        Debug.Log("Larger Stack: " + bigTile.GamePosition);
                        //TODO: select between options
                        if (tiles.Count == 1)
                        {
                            StateManager.PushCurrentState();
                            chooseTile.SelectedTile.ComponentOnTile = bigTile.ComponentOnTile;
                            bigTile.ComponentOnTile = null;
                            StateManager.CurrentState.GameLogicState.MapGrid.SetCell(chooseTile.SelectedTile);
                            StateManager.CurrentState.GameLogicState.MapGrid.SetCell(bigTile);
                            RenderGameState();
                            SwapPlayers();
                        }
                    }
                }
            }
        }

        private void SwapPlayers()
        {
            StateManager.CurrentState.GameLogicState.CurrentPlayerIndex =
                        (StateManager.CurrentState.GameLogicState.CurrentPlayerIndex == 0) ? (byte)1 : (byte)0;
        }

        private void ChooseDisc()
        {

        }

        private void ZoomCamera(float deltaY)
        {
            MapCamera.ZoomCamera(deltaY / 5);
        }

        private void UndoState()
        {
            if (StateManager.Count > 1)
            {
                StateManager.PopState();
                RenderGameState();
            }
        }

        private void RenderGameState()
        {
            engineManager.MapRenderer.RenderMap(StateManager.CurrentState.GameLogicState.MapGrid, MapHolder);
        }

        private void RenderGameState(GameState gameState)
        {
            engineManager.MapRenderer.RenderMap(gameState.GameLogicState.MapGrid, MapHolder);
        }

    }
}
