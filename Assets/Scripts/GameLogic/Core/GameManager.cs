using System.Collections.Generic;

using UnityEngine;

using EDBG.Engine.Core;
using EDBG.Engine.Visual;

using EDBG.Utilities.DataTypes;

using EDBG.GameLogic.Rules;
using EDBG.GameLogic.MapSystem;
using EDBG.GameLogic.Components;
using EDBG.UserInterface;
using Unity.VisualScripting;
using EDBG.States;

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


        void Start()
        {
            StateManager = new StateManager();
            engineManager = GameEngineManager.Instance;
            MapCamera.Initialize();
            LoadPrefabs();
            engineManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);
            engineManager.ScreenManager.ScreenChanged += ScreenChanged;

            //Set new game
            HumanPlayer human = new HumanPlayer("Human", 10, new BeginnerCorporation(Ownership.HumanPlayer));
            BotPlayer bot = new BotPlayer("Bot", 10, new BeginnerCorporation(Ownership.BotPlayer));
            StateManager.PushState(GameBuilder.BuildInitialState(4, 4, human, bot));

            //Render map
            engineManager.MapRenderer.RenderMap(StateManager.CurrentState.GameLogicState.MapGrid, GameWorld.Find("SquareMapHolder"));

            StateManager.CurrentState.GameLogicState.DiceTray.SetDice(5);
            StateManager.CurrentState.GameLogicState.DiceTray.RollAllDice();
            GameUI.Initialize(this);


        }


        void Update()
        {
            engineManager.InputHandler.Listen();
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
            CameraRaycaster cameraRaycaster = MapCamera.GetComponentInChildren<CameraRaycaster>();
            cameraRaycaster.Raycast(position);
            cameraRaycaster = TokenCamera.GetComponent<CameraRaycaster>();
            cameraRaycaster.Raycast(position);
        }

        private void ZoomCamera(float deltaY)
        {
            MapCamera.ZoomCamera(deltaY / 5);
        }

        private void UndoState()
        {
            DiceTrayObject.SetDice(StateManager.CurrentState.GameLogicState.DiceTray, engineManager.PrefabManager);
        }
    }
}
