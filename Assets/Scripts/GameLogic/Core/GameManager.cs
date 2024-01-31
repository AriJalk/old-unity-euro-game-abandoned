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
using EDBG.Director;
using Unity.VisualScripting;
using EDBG.Commands;

namespace EDBG.GameLogic.Core
{
    /// <summary>
    /// Central class for handling the logic side of the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private LogicState currentState;
        private Stack<CommandBase> commandStack;

        public EngineManagerScpritableObject EngineManager;

        public AnimationManager GameDirector { get; private set; }



        public Transform GameWorld;
        public CameraController MapCamera;
        public Camera TokenCamera;
        public GameUI GameUI;
        public DiceTrayObject DiceTrayObject;
        public MapHolder MapHolder;

        private void Awake()
        {
            EngineManager.InitializeScene(MapHolder);
        }

        void Start()
        {
            commandStack = new Stack<CommandBase>();
            MapCamera.Initialize();
            LoadPrefabs();
            EngineManager.InputManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);
            EngineManager.ScreenManager.ScreenChanged += ScreenChanged;
            GameDirector = new AnimationManager();


            //Set new game
            HumanPlayer human = new HumanPlayer("HumanPlayer", PlayerColors.Black, PlayerColors.Red, 10, new BeginnerCorporation(Ownership.HumanPlayer));
            BotPlayer bot = new BotPlayer("BotPlayer", PlayerColors.White, PlayerColors.Black, 10, new BeginnerCorporation(Ownership.BotPlayer));
            currentState = GameBuilder.BuildInitialState(4, 4, human, bot);
            RenderGameState(true);
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
            EngineManager.InputManager.InputEvents.UnsubscribeFromAllEvents(MoveCamera, SelectObject, ZoomCamera);
            EngineManager.ScreenManager.ScreenChanged -= ScreenChanged;

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
            EngineManager.ResourcesManager.PrefabManager.RegisterPrefab<MapTileGameObject>(squarePrefab, 16);

            GameObject discPrefab = Resources.Load<GameObject>("Prefabs/3D/DiscPrefab");
            if (discPrefab == null)
            {
                Debug.Log("Square Tile Prefab is not found in Resources/Prefabs/3D/DiscPrefab.");
                return;
            }
            EngineManager.ResourcesManager.PrefabManager.RegisterPrefab<DiscObject>(discPrefab, 50);

            GameObject diePrefab = Resources.Load<GameObject>("Prefabs/3D/DiePrefab");
            if (diePrefab == null)
            {
                Debug.Log("Die Prefab is not found in Resources/Prefabs/3D/DiePrefab");
                return;
            }
            EngineManager.ResourcesManager.PrefabManager.RegisterPrefab<DieObject>(diePrefab, 16);
        }

        private void ActionSelected(UIAction action)
        {
            if (action.name == "ConfirmAction")
            {
                Debug.Log("Confirm");
            }
            else if (action.name == "UndoCommand")
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
            if (GameDirector.IsGameLocked == false)
            {
                switch (currentState.RoundState)
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
            if (!GameDirector.IsGameLocked)
            {
                CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();
                Transform tileTransform = cameraRaycaster.Raycast(position, LayerMask.GetMask("Tile"));
                if (tileTransform != null && currentState.CurrentPlayer.DiscStock > 0)
                {
                    MapTileGameObject tile = tileTransform.GetComponent<MapTileGameObject>();
                    PlaceDiscCommand command = new PlaceDiscCommand(tile.TileData, currentState.CurrentPlayer, EngineManager.VisualManager.ObjectsRenderer);
                    command.ExecuteCommand();
                    commandStack.Push(command);
                    currentState.SwapCurrentPlayer();
                }
            }

        }

        private void ChooseStack(Vector2 position)
        {


        }

        private void ZoomCamera(float deltaY)
        {
            MapCamera.ZoomCamera(deltaY / 5);
        }

        private void UndoState()
        {
            if (commandStack.Count > 0)
            {
                CommandBase command = commandStack.Pop();
                command.UndoCommand();
                currentState.SwapCurrentPlayer();
            }
        }

        private void RenderGameState(bool isAnimated)
        {
            EngineManager.VisualManager.MapRenderer.RenderMap(currentState.MapGrid, isAnimated);
        }

        private void RenderGameState(LogicState gameState, bool isAnimated)
        {
            EngineManager.VisualManager.MapRenderer.RenderMap(gameState.MapGrid, isAnimated);
        }

    }
}
