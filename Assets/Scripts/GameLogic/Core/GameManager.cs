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
        private UIEvents uiEvents;


        public Transform GameWorld;
        public CameraController CameraController;
        public Camera DiceCamera;
        public GameUI GameUI;
        public DiceTrayObject DiceTrayObject;


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

            engineManager.MapRenderer.RenderMap(StateManager.CurrentState.GameLogicState.MapGrid, GameWorld.Find("SquareMapHolder"));
            engineManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);
            engineManager.ScreenManager.ScreenChanged += ScreenChanged;





        }


        void Update()
        {
            engineManager.InputHandler.Listen();
        }

        private void OnDestroy()
        {
            uiEvents.UnsubscribeToAllEvents(ActionSelected);
        }


        void MoveCamera(Vector2 axis)
        {
            CameraController.MoveCamera(axis.x, axis.y);
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

        //TODO: move to UI
        void SelectObject(bool[] mouseButtons, Vector2 position)
        {
            if (StateManager.CurrentState.UIState.Name == "ChooseDie" || StateManager.CurrentState.UIState.Name == "ChooseAction")
            {
                Ray ray = DiceCamera.ScreenPointToRay(position);
                LayerMask layerMask;
                layerMask = LayerMask.GetMask("Die");
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    if(StateManager.CurrentState.UIState.Name=="ChooseAction")
                    {
                        DieObject previousDie = ((ChooseAction)StateManager.CurrentState.UIState).ChosenDie;
                        previousDie.Highlight.gameObject.SetActive(false);
                    }
                    DieObject obj = hit.transform.GetComponent<DieObject>();
                    obj.Highlight.gameObject.SetActive(true);
                    //StateManager.CurrentState.GameLogicState.DiceTray.RemoveDie(int.Parse(obj.name));
                    //DiceTrayObject.SetDice(StateManager.CurrentState.GameLogicState.DiceTray, engineManager.PrefabManager);
                    StateManager.NextState(new ChooseAction(obj, GameUI));
                }
            }

        }

        void ZoomCamera(float deltaY)
        {
            CameraController.ZoomCamera(deltaY / 5);
        }

        //TODO: builder class
        void CreateTestGame()
        {
            StateManager = new StateManager();
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

            LogicState newState = new LogicState(mapGrid);

            newState.PlayerList = new List<Player>() {
                new HumanPlayer("Player", 10, new BeginnerCorporation(Ownership.HumanPlayer)),
                new BotPlayer("Bot", 10, new BeginnerCorporation(Ownership.BotPlayer)),
            };
            newState.DiceTray = new DiceTray();
            newState.DiceTray.SetDice(5);
            newState.DiceTray.RollAllDice();
            newState.CurrentPlayerIndex = 0;
            GameState gameState = new GameState(newState, new ChooseDie());
            StateManager.NextState(gameState);
            uiEvents = new UIEvents();
            uiEvents.SubscribeToAllEvents(ActionSelected);

            //TODO: index
            GameUI.Initialize(this, uiEvents);
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

        private void UndoState()
        {
            StateManager.UndoState(GameUI);
            DiceTrayObject.SetDice(StateManager.CurrentState.GameLogicState.DiceTray, engineManager.PrefabManager);
        }
    }
}
