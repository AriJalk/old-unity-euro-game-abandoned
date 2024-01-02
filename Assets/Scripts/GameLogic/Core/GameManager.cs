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
using EDBG.GameLogic.Components;
using EDBG.Engine.Animation;

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
        public SquareMapHolderObject MapHolder;
        public AnimationManager AnimationManager;


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
            RenderGameState(true);

            GameUI.Initialize(this);

            /* Test tile search
            TileRulesLogic.GetCellsInDirectLine(StateManager.CurrentState.GameLogicState.MapGrid, StateManager.CurrentState.GameLogicState.MapGrid.GetCell(1, 1), false);
            */

            // Test bfs
            bool[,] distanceMap = TileRulesLogic.GetCellsInDistance(StateManager.CurrentState.GameLogicState.MapGrid, StateManager.CurrentState.GameLogicState.MapGrid.GetCell<MapTile>(1, 1), 2);
            Debug.Log(distanceMap);

            MapHolder.Initialize(4,4); 
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
            engineManager.PrefabManager.RegisterPrefab<MapTileGameObject>(squarePrefab, 16);

            GameObject discPrefab = Resources.Load<GameObject>("Prefabs/3D/DiscPrefab");
            if (discPrefab == null)
            {
                Debug.Log("Square Tile Prefab is not found in Resources/Prefabs/3D/DiscPrefab.");
                return;
            }
            engineManager.PrefabManager.RegisterPrefab<DiscObject>(discPrefab, 50);

            GameObject diePrefab = Resources.Load<GameObject>("Prefabs/3D/DiePrefab");
            if (diePrefab == null)
            {
                Debug.Log("Die Prefab is not found in Resources/Prefabs/3D/DiePrefab");
                return;
            }
            engineManager.PrefabManager.RegisterPrefab<DieObject>(diePrefab, 16);
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
                case RoundStates.ChooseStack:
                    if (mouseButtons[0] == true)
                        ChooseStack(position);
                    break;
            }
        }

        private void ChooseTile(Vector2 position)
        {

            CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();

            Transform tileTransform = cameraRaycaster.Raycast(position, LayerMask.GetMask("Tile"));
            if (tileTransform != null)
            {
                MapTileGameObject tile = tileTransform.GetComponent<MapTileGameObject>();
                ChooseTile chooseTile = new ChooseTile(tile.TileData, StateManager.CurrentState.GameLogicState);
                Player owner = chooseTile.SelectedTile.GetOwner();
                // Empty Tile
                if (owner == null)
                {
                    StateManager.PushCurrentState();
                    chooseTile.UpdateState(StateManager.CurrentState.GameLogicState);

                    TileRulesLogic.AddDiscToTile(chooseTile);
                    //StateManager.CurrentState.GameLogicState.MapGrid.SetCell(chooseTile.SelectedTile);

                    engineManager.ObjectsRenderer.PlaceNewDisc(new Disc(StateManager.CurrentState.GameLogicState.GetCurrentPlayer()), chooseTile.SelectedTile, MapHolder, true);
                    //RenderGameState();
                    SwapPlayers();
                }

                // Player owned tile
                else if (owner == StateManager.CurrentState.GameLogicState.GetCurrentPlayer())
                {
                    List<MapTile> tiles = TileRulesLogic.GetTilesWithComponentInAllDirections(
                        StateManager.CurrentState.GameLogicState.MapGrid,
                        chooseTile.SelectedTile, StateManager.CurrentState.GameLogicState.GetCurrentPlayer(), true, true);
                    if(tiles.Count > chooseTile.SelectedTile.GetComponentOnTile<GameStack<Disc>>().Count)
                    {
                        StateManager.PushCurrentState();
                        int excess = tiles.Count - chooseTile.SelectedTile.GetComponentOnTile<GameStack<Disc>>().Count;
                        while(excess > 0)
                        {
                            
                            chooseTile.UpdateState(StateManager.CurrentState.GameLogicState);
                            TileRulesLogic.AddDiscToTile(chooseTile);
                            //RenderDisc
                            engineManager.ObjectsRenderer.PlaceNewDisc(new Disc(StateManager.CurrentState.GameLogicState.GetCurrentPlayer()), chooseTile.SelectedTile, MapHolder, true);
                            excess--;
                        }
                        
                        

                        SwapPlayers();
                    }

                }
                // Opponent owned tile
                else if (owner == StateManager.CurrentState.GameLogicState.GetOtherPlayer())
                {
                    List<MapTile> tiles = TileRulesLogic.GetBiggerOpponentStackTiles(chooseTile);
                    if (tiles.Count > 0)
                    {

                        StateManager.PushCurrentState();
                        StateManager.CurrentState.GameLogicState.TargetTile = chooseTile.SelectedTile;
                        StateManager.CurrentState.GameLogicState.RoundState = RoundStates.ChooseStack;
                        Debug.Log("ChooseStack start: Choose stack to capture with");
                        foreach (MapTile bigTile in tiles)
                        {
                            Debug.Log("Larger Stack: " + bigTile.GamePosition);
                            MapTileGameObject tileObject = MapHolder.GetTile(bigTile.GamePosition);
                            Transform stack = tileObject.transform.Find("Stack");
                            GameEngineManager.Instance.AnimationManager.StartAnimation(stack.GetComponent<AnimatedObject>(), "BreathTrigger");

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

        private void ChooseStack(Vector2 position)
        {

            ChooseTile chooseTile = new ChooseTile(StateManager.CurrentState.GameLogicState.TargetTile, StateManager.CurrentState.GameLogicState);
            List<MapTile> legalTiles = TileRulesLogic.GetBiggerOpponentStackTiles(chooseTile);
            CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();

            Transform tileTransform = cameraRaycaster.Raycast(position, LayerMask.GetMask("Tile"));
            if (tileTransform != null)
            {
                MapTile captureOrigin = tileTransform.GetComponent<MapTileGameObject>().TileData;
                bool isMatching = false;
                foreach (MapTile tile in legalTiles)
                {
                    if (captureOrigin.Equals(tile))
                    {
                        isMatching = true;
                        break;
                    }
                }
                if (isMatching == true)
                {
                    StateManager.PushCurrentState();
                    captureOrigin = StateManager.CurrentState.GameLogicState.MapGrid.GetCell(captureOrigin.GamePosition) as MapTile;
                    chooseTile.UpdateState(StateManager.CurrentState.GameLogicState);
                    chooseTile.SelectedTile.ComponentOnTile = captureOrigin.ComponentOnTile;
                    captureOrigin.ComponentOnTile = null;
                    RenderGameState(false);
                    StateManager.CurrentState.GameLogicState.RoundState = RoundStates.ChooseTile;
                    SwapPlayers();
                }
            }
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
                RenderGameState(false);
            }
        }

        private void RenderGameState(bool isAnimated)
        {
            engineManager.MapRenderer.RenderMap(StateManager.CurrentState.GameLogicState.MapGrid, MapHolder, isAnimated);
        }

        private void RenderGameState(GameState gameState, bool isAnimated)
        {
            engineManager.MapRenderer.RenderMap(gameState.GameLogicState.MapGrid, MapHolder, isAnimated);
        }

    }
}
