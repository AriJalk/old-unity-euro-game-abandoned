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
using UnityEngine.Events;

namespace EDBG.GameLogic.Core
{
    /// <summary>
    /// Central class for handling the logic side of the game
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public UnityEvent<string> GameMessageEvent;

        private TurnManager turnManager;

        public EngineManagerScpritableObject EngineManager;

        public AnimationManager AnimationManager { get; private set; }



        public Transform GameWorld;
        public CameraController MapCamera;
        public Camera TokenCamera;
        public GameUI GameUI;
        public OverlayUI OverlayUI;
        public DiceTrayObject DiceTrayObject;
        public MapHolder MapHolder;

        private void Awake()
        {
            EngineManager.InitializeScene(MapHolder);
            GameMessageEvent = new UnityEvent<string>();
        }

        void Start()
        {

            MapCamera.Initialize();
            LoadPrefabs();
            EngineManager.InputManager.InputEvents.SubscribeToAllEvents(MoveCamera, MouseClicked, ZoomCamera);
            EngineManager.ScreenManager.ScreenChanged += ScreenChanged;
            AnimationManager = new AnimationManager();


            //Set new game
            HumanPlayer human = new HumanPlayer("HumanPlayer", PlayerColors.Black, PlayerColors.Red, 10, new BeginnerCorporation(Ownership.HumanPlayer));
            BotPlayer bot = new BotPlayer("BotPlayer", PlayerColors.White, PlayerColors.Green, 10, new BeginnerCorporation(Ownership.BotPlayer));
            turnManager = new TurnManager(this, GameBuilder.BuildInitialState(4, 4, human, bot));
            EngineManager.VisualManager.RenderGameState(true, turnManager.LogicState);

            OverlayUI.Initialize(this);
            OverlayUI.ButtonEvent.AddListener(ActionSelected);

            GameMessageEvent.Invoke($"{turnManager.LogicState.CurrentPlayer.Name} Turn, select tile");

        }


        void Update()
        {

            if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.Z))
            {
                turnManager.UndoState();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                turnManager.Confirm();
            }
        }

        private void LateUpdate()
        {

        }

        private void OnDestroy()
        {
            EngineManager.InputManager.InputEvents.UnsubscribeFromAllEvents(MoveCamera, MouseClicked, ZoomCamera);
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

        private void ActionSelected(UICommands command)
        {
            if (command == UICommands.Confirm)
            {
                turnManager.Confirm();
            }
            else if (command == UICommands.Undo)
            {
                turnManager.UndoState();
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
        private void MouseClicked(bool[] mouseButtons, Vector2 position)
        {
            Transform hit;
            switch (turnManager.LogicState.RoundState)
            {
                case RoundStates.GameStart:
                    break;

                case RoundStates.ChooseTile:
                    hit = Raycast(position, LayerMask.GetMask("Tile"));
                    if (hit != null)
                        turnManager.SelectTile(hit.GetComponent<MapTileGameObject>().TileData);
                    break;
                case RoundStates.ChooseCaptureStack:
                    hit = Raycast(position, LayerMask.GetMask("Tile"));
                    if (hit != null)
                        turnManager.SelectStack(hit.GetComponent<MapTileGameObject>().TileData);
                    break;
                default:
                    break;
            }
        }

        private Transform Raycast(Vector2 position, LayerMask layer)
        {
            CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();
            Transform tileTransform = cameraRaycaster.Raycast(position, layer);
            return tileTransform;
        }


        private void ZoomCamera(float deltaY)
        {
            MapCamera.ZoomCamera(deltaY / 5);
        }


        private void RenderGameState(LogicState gameState, bool isAnimated)
        {
            EngineManager.VisualManager.MapRenderer.RenderMap(gameState.MapGrid, isAnimated);
        }

    }
}
