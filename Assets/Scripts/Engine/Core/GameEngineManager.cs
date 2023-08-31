using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EDBG.Rules;
using EDBG.MapSystem;

public class GameEngineManager : MonoBehaviour
{
    public enum LoopGameStates
    {
        Menu,
        PauseMenu,
        PlayerTurn,
        OpponentTurn,
        Action,
        Maintenance
    }

    public enum UIStates
    {
        SelectTokens,
        SelectAction,
        SelectTile,
        ActionConfirmation,
        LinearProgression
    }

    public PoolManager PoolManager;
    public MapRenderer MapRenderer;
    public ObjectsRenderer DiscRenderer;
    public MaterialPool MaterialPool;
    public Transform MapHolderObject;
    public UserInterfaceObject UserInterface;
    public InputHandler InputHandler;
    public CameraController CameraController;
    public PlatformManager PlatformManager;
    public ScreenManager ScreenManager;

    private ActionsManager ActionsManager;

    private float time = 0f;
    private int counter = 0;

    private Stack<LogicGameState> gameStateStack;


    // Start is called before the first frame update
    void Start()
    {
        string moves = string.Empty;
        bool[,] grid = TileRulesLogic.GetPossibleMoves(gameStateStack.Peek().mapGrid, gameStateStack.Peek().mapGrid.GetCell(0, 0), 3);
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] == true)
                {
                    moves += "O ";
                }

                else
                {
                    moves += "x ";
                    ((GameStack<Disc>)((MapTile)gameStateStack.Peek().mapGrid.GetCell(i, j)).ComponentOnTile).PopItem();
                    ((GameStack<Disc>)((MapTile)gameStateStack.Peek().mapGrid.GetCell(i, j)).ComponentOnTile).PopItem();
                }

            }
            moves += "\n";
        }
        Debug.Log(moves);

        MapRenderer.RenderMap(gameStateStack.Peek().mapGrid, MapHolderObject, MaterialPool, DiscRenderer);
    }

    void Awake()
    {
        InitizalizeScripts();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 1f)
        {
            counter++;
            UserInterface.SetText(counter.ToString());
            time = 0;
        }
        InputHandler.Listen();
    }

    void InitizalizeScripts()
    {

        gameStateStack = new Stack<LogicGameState>();

        gameStateStack.Push(new LogicGameState(new MapGrid()));

        PoolManager.Initialize();

        MaterialPool.Initialize();
        MapRenderer.Initialize(PoolManager);
        DiscRenderer.Initialize(PoolManager);


        UserInterface.Initialize(this);
        InputHandler.Initialize(UserInterface);

        PlatformManager.Initialize();

        CameraController.Initialize();

        ScreenManager.Initialize();

        //Listen to screen change event
        ScreenManager.ScreenChanged += ScreenChanged;

        ActionsManager = new ActionsManager();
        ActionsManager.RegisterAction("MoveDiscAction", new MoveDiscAction());
    }

    public void MoveDiscs(MapTile sourceTile, MapTile targetTile)
    {
        LogicGameState newState = (LogicGameState)gameStateStack.Peek().Clone();
        // Move the disc from the source stack to the destination stack
        IAction action = ActionsManager.GetAction("MoveDiscAction");
        action.ActionCompleted += RenderChanges;
        newState.SourceTile = (MapTile)sourceTile.Clone();
        newState.TargetTile = (MapTile)targetTile.Clone();
        gameStateStack.Push(newState);
        action.ExecuteAction(newState);

    }

    public void MoveCamera(float horizontal, float vertical)
    {
        CameraController.MoveCamera(horizontal, vertical);
    }

    private void RenderChanges(object sender, ActionCompletedEventArgs e)
    {

        List<EntityNames> fields = e.ItemsToUpdate;
        //Go over list to update and render changes
        foreach (EntityNames field in fields)
        {
            switch (field)
            {

                case EntityNames.SourceTile:
                    {
                        MapTile tile = gameStateStack.Peek().SourceTile;
                        if (tile != null)
                        {
                            SquareTileObject tileObject = MapRenderer.GetTileObject(tile.GamePosition.Y, tile.GamePosition.X);
                            tileObject.TileData = tile;
                            DiscRenderer.RenderObjectsOnTileObject(tileObject, MaterialPool);
                        }
                        break;
                    }
                case EntityNames.TargetTile:
                    {
                        MapTile tile = gameStateStack.Peek().TargetTile;
                        if (tile != null)
                        {
                            SquareTileObject tileObject = MapRenderer.GetTileObject(tile.GamePosition.Y, tile.GamePosition.X);
                            tileObject.TileData = tile;
                            DiscRenderer.RenderObjectsOnTileObject(tileObject, MaterialPool);
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }
    }


    //TODO: color from list, Find model 
    public void SelectObject(Vector3 position, bool[] mouseButtons)
    {
        var camera = CameraController.GetComponentInChildren<Camera>();
        Ray ray = camera.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.TryGetComponent<SquareTileObject>(out var tile))
            {
                Renderer renderer = tile.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    // Change the material of the renderer
                    if (mouseButtons[0])
                        renderer.material = MaterialPool.GetMaterial("Materials/RedWoodMaterial");
                    else if (mouseButtons[1])
                        renderer.material = MaterialPool.GetMaterial("Materials/BlueWoodMaterial");
                }
            }
        }
    }

    private void ScreenChanged(object sender, ScreenChangedEventArgs e)
    {
        CameraController.UpdateAspectRatio(e.NewWidth, e.NewHeight, e.NewScreenOrientation);
        if (e.NewWidth > e.NewHeight)
        {
            UserInterface.ScreenChanged(ScreenOrientation.LandscapeLeft);
        }
        else
        {
            UserInterface.ScreenChanged(ScreenOrientation.Portrait);
        }
    }

    public void ZoomCamera(float deltaY)
    {
        CameraController.ZoomCamera(deltaY / 5);
    }

    public void TestMove()
    {
        gameStateStack.Peek().SourceTile = (MapTile)gameStateStack.Peek().mapGrid.GetCell(1, 0);
        gameStateStack.Peek().TargetTile = (MapTile)gameStateStack.Peek().mapGrid.GetCell(1, 1);

        if (((GameStack<Disc>)gameStateStack.Peek().SourceTile.ComponentOnTile).Count > 0)
            MoveDiscs(gameStateStack.Peek().SourceTile, gameStateStack.Peek().TargetTile);



        /* Debug.Log($"Current: {((GameStack<Disc>)newState.SourceTile.ComponentOnTile).Count}" +
             $"\nPrevious {((GameStack<Disc>)gameStateStack.Peek().SourceTile.ComponentOnTile).Count}");*/

        //gameStateStack.Push(newState);
    }

    public void Undo()
    {
        if (gameStateStack.Count > 1)
        {
            gameStateStack.Pop();
        }

        MapRenderer.RenderMap(gameStateStack.Peek().mapGrid, MapHolderObject, MaterialPool, DiscRenderer);



    }
}
