using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EDBG.Rules;
using EDBG.MapSystem;

public class GameEngineManager : MonoBehaviour
{
    public enum GameStates
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
    public SquareMapHolderObject MapHolder;
    public UserInterfaceObject UserInterface;
    public InputHandler InputHandler;
    public CameraController CameraController;
    public PlatformManager PlatformManager;
    public ScreenManager ScreenManager;

    private ActionsManager ActionsManager;
    private GameStates gameState;

    private float time = 0f;
    private int counter = 0;
    private Dictionary<EntityNames, object> logicGameState;


    // Start is called before the first frame update
    void Start()
    {
        string moves = string.Empty;
        bool[,] grid = TileRulesLogic.GetPossibleMoves(MapHolder.GetMap(), MapHolder.GetDataTile(0, 0), 3);
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
                    //MapGrid.GetDataTile(i, j).discStack.PopItem();
                    //MapGrid.GetDataTile(i, j).discStack.PopItem();
                }

            }
            moves += "\n";
        }
        Debug.Log(moves);
        GridContainer container = MapHolder.GetMap().GetCellAsContainer(0, 0);
        grid = TileRulesLogic.GetPossibleMoves(container, container.GetCell(0, 0), 1);
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
                    MapLocation location = container.GetCell(i, j) as MapLocation;
                    location.DiscStack.PopItem();
                    location.DiscStack.PopItem();
                }

            }
            moves += "\n";
        }
        MapRenderer.RenderMap(MapHolder, MaterialPool, DiscRenderer);
        Debug.Log(moves);
    }

    void Awake()
    {
        InitizalizeScripts();
        gameState = GameStates.PlayerTurn;
        logicGameState = new Dictionary<EntityNames, object>();
        logicGameState[EntityNames.Map] = MapHolder;
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
        if (gameState == GameStates.PlayerTurn)
        {
            InputHandler.Listen();
        }
    }

    void InitizalizeScripts()
    {
        PoolManager.Initialize();

        MaterialPool.Initialize();
        MapRenderer.Initialize(PoolManager);
        DiscRenderer.Initialize(PoolManager);

        MapHolder.Initialize();

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

    public void MoveDiscs(MapLocation sourceLocation, MapLocation targetLocation)
    {
        // Move the disc from the source stack to the destination stack
        gameState = GameStates.Action;
        IAction action = ActionsManager.GetAction("MoveDiscAction");
        action.ActionCompleted += RenderChanges;
        logicGameState[EntityNames.SourceLocation] = sourceLocation;
        logicGameState[EntityNames.TargetLocation] = targetLocation;
        action.ExecuteAction(logicGameState);
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
                case EntityNames.TargetTile:
                    {
                        MapTile tile = (MapTile)logicGameState[field];
                        if (tile != null)
                            DiscRenderer.RenderObjectsOnTileObject(MapRenderer.GetTileObject(tile.GamePosition.X, tile.GamePosition.Y), MaterialPool);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }
        gameState = GameStates.PlayerTurn;
    }


    //TODO: color from list, Find model 
    public void SelectObject(Vector3 position)
    {
        var camera = CameraController.GetComponentInChildren<Camera>();
        Ray ray = camera.ScreenPointToRay(position);
        var collide = Physics.Raycast(ray);
        if (collide)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                SquareTileObject tile = hitObject.GetComponent<SquareTileObject>();
                if (tile != null)
                {
                    Renderer renderer = hitObject.GetComponentInChildren<Renderer>();
                    if (renderer != null)
                    {
                        // Change the material of the renderer
                        renderer.material = MaterialPool.GetMaterial("Materials/RedWoodMaterial");
                    }
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
        logicGameState[EntityNames.SourceTile] = MapHolder.GetDataTile(1, 0);
        logicGameState[EntityNames.TargetTile] = MapHolder.GetDataTile(1, 1);
        logicGameState[EntityNames.SourceLocation] = MapHolder.GetDataTile(1, 0).GetCell(0, 0);
        logicGameState[EntityNames.TargetLocation] = MapHolder.GetDataTile(1, 1).GetCell(0, 0);
        MoveDiscs((MapLocation)logicGameState[EntityNames.SourceLocation], (MapLocation)logicGameState[EntityNames.TargetLocation]);
    }

}
