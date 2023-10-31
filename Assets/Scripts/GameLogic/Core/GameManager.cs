using System.Collections.Generic;
using UnityEngine;
using EDBG.MapSystem;


namespace EDBG.Rules
{
    public class GameManager : MonoBehaviour
    {

        private GameEngineManager engineManager { get; set; }

        private Stack<LogicGameState> gameStateStack { get; set; }
        private LogicGameState currentGameState
        {
            get
            {
                return gameStateStack.Peek();
            }
        }


        public Transform GameWorld;

        public CameraController CameraController;

        public GameUI GameUI;

        void Start()
        {
            CameraController.Initialize();
            CreateTestGame();
            MoveDiscAction moveDisc = new MoveDiscAction();
            moveDisc.SetAction((MapTile)currentGameState.MapGrid.GetCell(0, 0), (MapTile)currentGameState.MapGrid.GetCell(1, 1), 3, currentGameState);
            moveDisc.ExecuteAction();

            engineManager = GameEngineManager.Instance;
            //Draw map at head of stack
            engineManager.MapRenderer.RenderMap(currentGameState.MapGrid, GameWorld.Find("SquareMapHolder"));
            engineManager.InputEvents.SubscribeToAllEvents(MoveCamera, SelectObject, ZoomCamera);
            engineManager.ScreenManager.ScreenChanged += ScreenChanged;
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

            //UtilityFunctions.PrintArray(array);

            UtilityFunctions.ShuffleArray(array);

            //UtilityFunctions.PrintArray(array);

            Stack<int> faces = new Stack<int>(array);
            //UtilityFunctions.PrintStack(faces);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    MapTile tile = new MapTile(new GamePosition(i, j), faces.Pop());
                    tile.ComponentOnTile = new GameStack<Disc>();
                    if (i == 0 && j == 0)
                    {
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.Black));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.Black));
                    }
                    else if (i == 3 && j == 3)
                    {
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.White));
                        ((GameStack<Disc>)tile.ComponentOnTile).PushItem(new Disc(PieceColors.White));
                    }
                    mapGrid.SetCell(tile);
                }
            }





            LogicGameState newState = new LogicGameState(mapGrid);



            /*
             * MoveDiscAction moveDiscAction = new MoveDiscAction((MapTile)mapGrid.GetCell(0, 0), (MapTile)mapGrid.GetCell(0, 0), 3);
            moveDiscAction.UpdateCanExecute(newState);
            moveDiscAction.ExecuteAction();
            moveDiscAction.UpdateCanExecute(newState);
            moveDiscAction.ExecuteAction();
            moveDiscAction.UpdateCanExecute(newState);
            moveDiscAction.ExecuteAction();
            */

            /* Bag-builder test build
             * 
             * List<ActionToken> tokens = new List<ActionToken>
            {
                new ActionToken(TokenColors.Red),
                new ActionToken(TokenColors.Blue),
                new ActionToken(TokenColors.Green),
                new ActionToken(TokenColors.Red),
                new ActionToken(TokenColors.Blue),
                new ActionToken(TokenColors.Green),
                new ActionToken(TokenColors.Red),
                new ActionToken(TokenColors.Blue),
                new ActionToken(TokenColors.Green),
                new ActionToken(TokenColors.Green)
            };

            UtilityFunctions.ShuffleList(tokens);

            newState.playerTokenBag = new GameStack<ActionToken>(tokens);

            newState.playerHand.PushItem(newState.playerTokenBag.PopItem());
            newState.playerHand.PushItem(newState.playerTokenBag.PopItem());
            newState.playerHand.PushItem(newState.playerTokenBag.PopItem());
            gameUI.BuildHand(newState.playerHand);
            */

            newState.PlayerList = new List<PlayerBase>() {
                new HumanPlayer("Player", 10, new BeginnerCorporation()),
                new BotPlayer("Bot", 10, new BeginnerCorporation())
            };
            gameStateStack.Push(newState);

            GameUI.Initialize(newState.PlayerList[0]);
        }
    }
}
