using ResourcePool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EDBG.Rules;

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
    public DiscRenderer DiscRenderer;
    public MaterialPool MaterialPool;
    public SquareMapHolderObject SquareMap;
    public UserInterfaceObject UserInterface;
    public InputHandler InputHandler;
    public Transform CameraTransform;
    public PlatformManager PlatformManager;
    public ScreenOrientationManager ScreenOrientationManager;

    private GameStates gameState;

    private float time = 0f;
    private int counter = 0;
    private Dictionary<Names.EntityNames, object> logicGameState;


    // Start is called before the first frame update
    void Start()
    {
        string moves = string.Empty;
        bool[,] grid = TileRulesLogic.GetPossibleMoves(SquareMap.GetMap(), SquareMap.GetDataTile(0, 0), 3);
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
                    SquareMap.GetDataTile(i, j).discStack.PopDisc();
                    SquareMap.GetDataTile(i, j).discStack.PopDisc();
                }

            }
            moves += "\n";
        }
        Debug.Log(moves);
        MapRenderer.RenderMap(SquareMap, MaterialPool, DiscRenderer);
    }

    void Awake()
    {
        InitizalizeScripts();
        gameState = GameStates.PlayerTurn;
        logicGameState = new Dictionary<Names.EntityNames, object>();
        logicGameState[Names.EntityNames.Map] = SquareMap;
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

        SquareMap.Initialize();

        UserInterface.Initialize(this);
        InputHandler.Initialize(UserInterface);

        TileRulesLogic.Initialize(SquareMap.GetMap().Rows, SquareMap.GetMap().Columns);
        
        PlatformManager.Initialize();

        ScreenOrientationManager.Initialize(this);
    }

    public void MoveDiscs(int sourceRow, int sourceCol, int targetRow, int targetCol)
    {
        // Move the disc from the source stack to the destination stack
        gameState = GameStates.Action;
        IAction action = new MoveDiscAction();
        action.ActionCompleted += RenderChanges;
        logicGameState[Names.EntityNames.SourceTile] = SquareMap.GetDataTile(sourceRow, sourceCol);
        logicGameState[Names.EntityNames.TargetTile] = SquareMap.GetDataTile(targetRow, targetCol);
        action.ExecuteAction(logicGameState);

        // Render the updated tiles
    }

    public void MoveCamera(float horizontal, float vertical)
    {
        float panSpeed = 10f;
        float newX=horizontal * panSpeed *Time.deltaTime;
        float newZ=vertical * panSpeed *Time.deltaTime;
        CameraTransform.Translate(new Vector3(newX,0,newZ), Space.World);
    }

    private void RenderChanges(object sender, ActionCompletedEventArgs e)
    {
        List<Names.EntityNames> fields = e.ItemsToUpdate;
        foreach (Names.EntityNames field in fields)
        {
            SquareTile tile = (SquareTile)logicGameState[field];
            if (tile != null)
                DiscRenderer.RenderDiscsOnTileObject(MapRenderer.GetTileObject(tile.Row, tile.Column), MaterialPool);
        }
        gameState = GameStates.PlayerTurn;
    }
    //TODO: WHOLE ACTION PHASE PROCCESS
    private void MoveDiscStart()
    {
        IAction action = new MoveDiscAction();
        action.ActionCompleted += RenderChanges;
    }

    //TODO: color from list, Find model 
    public void SelectObject(Vector3 position)
    {
        var camera = CameraTransform.GetComponentInChildren<Camera>();
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
                        renderer.material = MaterialPool.GetMaterial("Materials/GreenMaterial");
                    }
                }

            }
        }
    }

    public void ChangeOrientation(ScreenOrientation orientation)
    {
        Camera camera = CameraTransform.GetComponentInChildren<Camera>();
        if(orientation==ScreenOrientation.Portrait || orientation == ScreenOrientation.PortraitUpsideDown)
        {
            camera.orthographicSize = 4;
        }
        else 
        {
            camera.orthographicSize = 2;
        }
    }
}
