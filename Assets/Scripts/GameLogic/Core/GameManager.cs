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
using EDBG.Director;
using Unity.VisualScripting;

namespace EDBG.GameLogic.Core
{
    /// <summary>
    /// Central class for handling the logic side of the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private LogicState currentState
        {
            get
            {
                return StateManager.CurrentState;
            }
        }
        public StateManager StateManager { get; private set; }

        private GameEngineManager engineManager;

        public Transform GameWorld;
        public CameraController MapCamera;
        public Camera TokenCamera;
        public GameUI GameUI;
        public DiceTrayObject DiceTrayObject;
        public MapHolder MapHolder;
        public GameDirector Director;



        void Start()
        {
            StateManager = new StateManager();
            engineManager = GameEngineManager.Instance;
            MapCamera.Initialize();
            LoadPrefabs();
            engineManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);
            engineManager.ScreenManager.ScreenChanged += ScreenChanged;
            engineManager.ObjectsRenderer.SetDirector(Director);
            engineManager.MapRenderer.SetDirector(Director);


            //Set new game
            HumanPlayer human = new HumanPlayer("HumanPlayer", PlayerColors.Black, 10, new BeginnerCorporation(Ownership.HumanPlayer));
            BotPlayer bot = new BotPlayer("BotPlayer", PlayerColors.White, 10, new BeginnerCorporation(Ownership.BotPlayer));
            StateManager.PushState(GameBuilder.BuildInitialState(4, 4, human, bot));
            Director.BuildGameState(currentState, true);
        }


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.U))
            {
                UndoState();
            }
        }

        private void LateUpdate()
        {

        }

        private void OnDestroy()
        {
            engineManager.InputEvents.UnsubscribeFromAllEvents(MoveCamera, SelectObject, ZoomCamera);
            engineManager.ScreenManager.ScreenChanged -= ScreenChanged;

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
            if (Director.IsGameLocked == false)
            {
                switch (StateManager.CurrentState.RoundState)
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
        }

        private void ChooseTile(Vector2 position)
        {

            CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();

            Transform tileTransform = cameraRaycaster.Raycast(position, LayerMask.GetMask("Tile"));
            if (tileTransform != null)
            {
                MapTileGameObject tile = tileTransform.GetComponent<MapTileGameObject>();
                ChooseTile chooseTile = new ChooseTile(tile.TileData, StateManager.CurrentState);
                Player owner = chooseTile.SelectedTile.GetOwner();
                // Empty Tile
                if (owner == null)
                {
                    StateManager.PushCurrentState();
                    //TODO: move to director
                    //Add disc to tile logic map
                    chooseTile.UpdateState(StateManager.CurrentState);
                    TileRulesLogic.AddDiscToTile(chooseTile);

                    engineManager.ObjectsRenderer.PlaceNewDisc(new Disc(StateManager.CurrentState.GetCurrentPlayer()), chooseTile.SelectedTile, MapHolder, true);
                    SwapPlayers();
                }

                // Player owned tile
                else if (owner == StateManager.CurrentState.GetCurrentPlayer())
                {
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
                            engineManager.ObjectsRenderer.PlaceNewDisc(new Disc(StateManager.CurrentState.GetCurrentPlayer()), chooseTile.SelectedTile, MapHolder, true);
                            excess--;
                        }

                        SwapPlayers();
                    }

                }
                // Opponent owned tile
                else if (owner == StateManager.CurrentState.GetOtherPlayer())
                {
                    List<MapTile> tiles = TileRulesLogic.GetBiggerOpponentStackTiles(chooseTile);
                    if (tiles.Count > 0)
                    {

                        StateManager.PushCurrentState();
                        StateManager.CurrentState.TargetTile = chooseTile.SelectedTile;
                        StateManager.CurrentState.RoundState = RoundStates.ChooseStack;
                        Debug.Log("ChooseStack start: Choose stack to capture with");
                        NewDirector.Instance.StopAllAnimations();
                        foreach (MapTile bigTile in tiles)
                        {
                            Debug.Log("Larger Stack: " + bigTile.GamePosition);
                            MapTileGameObject tileObject = MapHolder.GetTile(bigTile.GamePosition);
                            Transform stack = tileObject.Stack.transform;
                            stack.GetComponent<AnimatedObject>().IsLooping = true;
                            stack.AddComponent<BreathingAnimation>();
                        }
                    }

                }
            }
        }

        private void SwapPlayers()
        {
            StateManager.CurrentState.CurrentPlayerIndex =
                        (StateManager.CurrentState.CurrentPlayerIndex == 0) ? (byte)1 : (byte)0;
        }

        private void ChooseStack(Vector2 position)
        {

            ChooseTile chooseTile = new ChooseTile(StateManager.CurrentState.TargetTile, StateManager.CurrentState);
            List<MapTile> legalTiles = TileRulesLogic.GetBiggerOpponentStackTiles(chooseTile);
            CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();

            Transform tileTransform = cameraRaycaster.Raycast(position, LayerMask.GetMask("Tile"));
            if (tileTransform != null)
            {
                MapTile captureOrigin = tileTransform.GetComponent<MapTileGameObject>().TileData;
                if (legalTiles.Contains(captureOrigin))
                {
                    StateManager.PushCurrentState();
                    captureOrigin = StateManager.CurrentState.MapGrid.GetCell(captureOrigin.GamePosition) as MapTile;
                    chooseTile.UpdateState(StateManager.CurrentState);
                    chooseTile.SelectedTile.ComponentOnTile = captureOrigin.ComponentOnTile;
                    captureOrigin.ComponentOnTile = null;
                    NewDirector.Instance.StopAllAnimations();
                    Director.BuildGameState(currentState, false);
                    StateManager.CurrentState.RoundState = RoundStates.ChooseTile;
                    SwapPlayers();
                }
                //Switch target if possible
                else
                {
                    StateManager.PopState();
                    NewDirector.Instance.StopAllAnimations();
                    ChooseTile(position);
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
                if (StateManager.CurrentState.RoundState == RoundStates.ChooseStack && StateManager.Count > 1)
                {
                    StateManager.PopState();
                }
                NewDirector.Instance.StopAllAnimations();
                Director.BuildGameState(currentState, false);
            }
        }

        private void RenderGameState(bool isAnimated)
        {
            engineManager.MapRenderer.RenderMap(StateManager.CurrentState.MapGrid, MapHolder, isAnimated);
        }

        private void RenderGameState(LogicState gameState, bool isAnimated)
        {
            engineManager.MapRenderer.RenderMap(gameState.MapGrid, MapHolder, isAnimated);
        }

    }
}
