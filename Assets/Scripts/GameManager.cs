using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    [SerializeField] MapManager map;
    [SerializeField] WallsArr[] wallsArr;
    [SerializeField] Transform GameView,DoorsView;
    [SerializeField] EventController eventController;
    [SerializeField] StoreController store;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] int encountRate;
    
    Player player;
    
    public bool isLomilwa;
    public State currentState;
    bool isMessage,isStoreFront;
    
    DIRECTION direction = DIRECTION.RIGHT;
    int[,] move = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
    int[,,] locations = {
        //TOP
        {
            {-1,-4 },{ 1,-4},{0,-4 },
            {-1,-3 },{ 1,-3},{0,-3 },
            {-1,-2 },{ 1,-2},{0,-2 },
            {-1,-1 },{ 1,-1},{0,-1 },
            {-1,0 },{ 1,0},{0,0 },
        },
        //RIGHT
        {
            {4,-1 },{ 4,1},{4,0 },
            {3,-1 },{ 3,1},{3,0 },
            {2,-1 },{ 2,1},{2,0 },
            {1,-1 },{ 1,1},{1,0 },
            {0,-1 },{ 0,1},{0,0 },
        },
        //down
        {
            {1,4 },{ -1,4},{0,4 },
            {1,3 },{ -1,3},{0,3 },
            {1,2 },{ -1,2},{0,2 },
            {1,1 },{ -1,1},{0,1 },
            {1,0 },{ -1,0},{0,0 },
        },
        //left
        {
            {-4,1 },{ -4,-1},{-4,-0 },
            {-3,1 },{ -3,-1},{-3,-0 },
            {-2,1 },{ -2,-1},{-2,-0 },
            {-1,1 },{ -1,-1},{-1,-0 },
            {0,1 },{ 0,-1},{0,-0 },

        },
    };
    
    float rotateDeg = 90f;
    string[] currentTileText;
    List<GameObject> wallList,doorList;
    Vector2Int currentPlayerPositionOnTile,nextPlayerPositionOnTile;
    int _x, _y;
    private void Awake()
    {
       if(I == null)
        {
            I = this;
        }
        wallList = new List<GameObject>();
        doorList = new List<GameObject>();
        _initWall();
    }

    void Start()
    {
        map.LoadMapData();
        map.CreateMap2D();
        player = map.player;
        currentPlayerPositionOnTile = map.moveObjPosOnTile[player.gameObject];
        _view3D();
        _initPlayerRote();
        _view2D();
        currentState = State.MOVE;
        battleSystem.OnBattleEnd += _endBattle;
    }

    void _initPlayerRote()
    {
        switch (direction)
        {
            case DIRECTION.TOP:
                break;
            case DIRECTION.RIGHT:
                player.Turn(new Vector3(0, 0, -rotateDeg));
                break;
            case DIRECTION.BOTTOM:
                player.Turn(new Vector3(0, 0, rotateDeg * 2));
                break;
            case DIRECTION.LEFT:
                player.Turn(new Vector3(0, 0, rotateDeg));
                break;
            default:
                break;
        }
        
    }

    void _initWall()
    {
        foreach(WallsArr walls in wallsArr)
        {
            foreach(GameObject wall in walls.wall)
            {
                if(wall != null)
                {
                    GameObject w = Instantiate(wall, GameView.position, Quaternion.identity, GameView);
                    w.name = wall.name;
                    wallList.Add(w);
                    w.SetActive(false);
                }
            }

            foreach(GameObject door in walls.doors)
            {
                if(door != null)
                {
                    GameObject d = Instantiate(door, DoorsView.position, Quaternion.identity, DoorsView);
                    d.name = door.name;
                    doorList.Add(d);
                    d.SetActive(false);
                }
            }
        }
        
    }

    void _hideWall()
    {
        foreach(GameObject wall in wallList)
        {
            wall.SetActive(false);
        }
        foreach(GameObject door in doorList)
        {
            door.SetActive(false);
        }
    }

    void _updatePos(int _loc)
    {
         _x = (currentPlayerPositionOnTile.x + locations[(int)direction, _loc, 0] + map.maxX) % map.maxX;
         _y = (currentPlayerPositionOnTile.y + locations[(int)direction, _loc, 1] + map.maxY) % map.maxY;
    }
    
    void _view2D()
    {
        
        map.Show2DMap(currentPlayerPositionOnTile);
    }



    void _view3D()
    {
        _hideWall();
        currentPlayerPositionOnTile = map.moveObjPosOnTile[player.gameObject];
        int sort = 1;
        for (int i = 0; i < 15; i++)
        {
            if(!isLomilwa && i < 9)
            {
                continue;
            }
            _updatePos(i);


            for (int j = 0; j < 4; j++)
            {
                int wallInt = map.GetTileTalbe(new Vector2Int(_x, _y), ((int)direction + j )% (int)DIRECTION.MAX);
                int doorInt = map.GetTileTalbe(new Vector2Int(_x, _y), ((int)direction + j) % (int)DIRECTION.MAX, true);
                int isDoor = wallInt + doorInt;
                if (wallInt == (int)WALL_CATE.WALL_CATE_WALL)
                {

                    if (wallsArr[i].wall[j] != null)
                    {
                        for (int k = 0; k < wallList.Count; k++)
                        {
                            if (wallsArr[i].wall[j].name == wallList[k].name)
                            {
                                wallList[k].SetActive(true);
                                wallList[k].GetComponent<SpriteRenderer>().sortingOrder = sort ;
                                
                            }

                            if (isDoor == (int)WALL_CATE.WALL_CATE_DOOR && wallsArr[i].doors[j].name == doorList[k].name)
                            {
                                sort++;
                                doorList[k].SetActive(true);
                                doorList[k].GetComponent<SpriteRenderer>().sortingOrder = sort;
                                
                            }

                        }
                    }
                }
                sort++;
            }

        }
    }
    void _checkEncount()
    {
        if(UnityEngine.Random.Range(0,100) < encountRate)
        {
            _startBattle();
            
        }
    }

    void _startBattle()
    {
        currentState = State.BATTLE;
        GameView.gameObject.SetActive(false);
        DoorsView.gameObject.SetActive(false);
        battleSystem.gameObject.SetActive(true);
        battleSystem.BattleStart();
    }

    void _endBattle()
    {
        currentState = State.MOVE;
        GameView.gameObject.SetActive(true);
        DoorsView.gameObject.SetActive(true);
        battleSystem.gameObject.SetActive(false);

    }

    void _checkEvents(Vector2Int _pos)
    {
        
        switch (eventController.CheckEvents(_pos))
        {
            case EventType.STAIRS:
                break;
            case EventType.TRAPS:
                break;
            case EventType.MESSAGES:
                
                eventController.Messages(_pos);
                isMessage = true;
                break;
            case EventType.WARP:
                player.MoveForward(map.GetScreenPositionFromTileTable(eventController.Warp(_pos)));
                map.moveObjPosOnTile[player.gameObject] = eventController.Warp(_pos);
                break;
            case EventType.SHOP:
                store.EnterStore(_pos);
                currentState = State.SHOPPING;
                isStoreFront = false;
                break;
            default:
                
                _checkEncount();
                break;
        }
    }



    void _checkNextEvents()
    {
        
        nextPlayerPositionOnTile = _getNextPos();
        
        switch (eventController.CheckEvents(nextPlayerPositionOnTile))
        {
            case EventType.STAIRS:
                break;
            case EventType.TRAPS:
                break;
            case EventType.MESSAGES:
                break;
            case EventType.WARP:
                break;
            case EventType.SHOP:
                if (_checkDoor(currentPlayerPositionOnTile))
                {
                    store.StoreEntrance(nextPlayerPositionOnTile);
                    isStoreFront = true;
                }
                
                break;
            default:
                if (isStoreFront)
                {
                    store.HideSignboard();
                    isStoreFront = false;
                }
                
                break;
        }
    }

    Vector2Int _getNextPos()
    {
        return currentPlayerPositionOnTile + new Vector2Int(move[(int)direction, 0], move[(int)direction, 1]);
    }

    void Update()
    {
        if(currentState == State.SHOPPING)
        {
            return;
        }
        if(currentState == State.MOVE)
        {
            _move();
        }  
    }

    public void CanMove()
    {
        currentState = State.MOVE;
    }

    void _move()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isMessage)
            {
                eventController.HideMassages();
                isMessage = false;
                //return;
            }

            currentPlayerPositionOnTile = map.moveObjPosOnTile[player.gameObject];
            bool canWalk = _checkWall(currentPlayerPositionOnTile);
            if (canWalk)
            {
                nextPlayerPositionOnTile = _getNextPos();
                nextPlayerPositionOnTile = _moveLimit(nextPlayerPositionOnTile, currentPlayerPositionOnTile);
                player.MoveForward(map.GetScreenPositionFromTileTable(nextPlayerPositionOnTile));
                map.moveObjPosOnTile[player.gameObject] = nextPlayerPositionOnTile;
                _checkEvents(nextPlayerPositionOnTile);


            }
            _view3D();
            _view2D();
            _checkNextEvents();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isMessage)
            {
                eventController.HideMassages();
                isMessage = false;
                //return;
            }

            currentPlayerPositionOnTile = map.moveObjPosOnTile[player.gameObject];
            //•Ç”»’è
            bool canWalk = _checkWall(currentPlayerPositionOnTile);

            //•Ç‚ª‚ ‚é‚È‚çƒhƒA”»’è
            if (!canWalk)
            {
                canWalk = _checkDoor(currentPlayerPositionOnTile);
            }


            if (canWalk)
            {
                nextPlayerPositionOnTile = _getNextPos();
                nextPlayerPositionOnTile = _moveLimit(nextPlayerPositionOnTile, currentPlayerPositionOnTile);

                player.MoveForward(map.GetScreenPositionFromTileTable(nextPlayerPositionOnTile));
                map.moveObjPosOnTile[player.gameObject] = nextPlayerPositionOnTile;

                _checkEvents(nextPlayerPositionOnTile);

            }
            _view3D();
            _view2D();
            _checkNextEvents();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isMessage)
            {
                eventController.HideMassages();
                isMessage = false;
                //return;
            }

            direction--;
            int d = ((int)direction + (int)DIRECTION.MAX) % (int)DIRECTION.MAX;
            direction = (DIRECTION)d;
            player.Turn(new Vector3(0, 0, rotateDeg));
            
            _checkNextEvents();
            

            _view3D();

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isMessage)
            {
                eventController.HideMassages();
                isMessage = false;
                //return;
            }

            direction += 2;
            int d = (int)direction % (int)DIRECTION.MAX;
            direction = (DIRECTION)d;
            player.Turn(new Vector3(0, 0, rotateDeg * 2));
            
            _checkNextEvents();
            

            _view3D();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isMessage)
            {
                eventController.HideMassages();
                isMessage = false;
                //return;
            }

            direction++;
            int d = (int)direction % (int)DIRECTION.MAX;
            direction = (DIRECTION)d;
            player.Turn(new Vector3(0, 0, -rotateDeg));
            
            _checkNextEvents();
            

            _view3D();

        }
    }



    Vector2Int _moveLimit(Vector2Int _next,Vector2Int _current)
    {
        if (_next.y < 0)
        {
            _next = _current + new Vector2Int(move[(int)direction, 0], map.maxY -1 );
        }
        if (_next.y > (map.maxY-1)) 
        {
            _next = _current + new Vector2Int(move[(int)direction, 0], -(map.maxY - 1));
        }
        if (_next.x < 0)
        {
            _next = _current + new Vector2Int(map.maxX - 1, move[(int)direction, 1]);
        }
        if (_next.x > (map.maxX - 1))
        {
            _next = _current + new Vector2Int(-(map.maxX -1), move[(int)direction, 1]);   
        }
        return _next;
    }


    bool _checkWall(Vector2Int _current)
    {
        currentTileText = map.GetNextTileTable(_current).ToString().Split("_");
        int _index = Array.IndexOf(currentTileText, direction.ToString());
        return _index < 0;
    }

    bool _checkDoor(Vector2Int _current)
    {
        currentTileText = map.GetNextTileTable(_current, true).ToString().Split("_");
        int _index = Array.IndexOf(currentTileText, direction.ToString());
        return _index >= 0;
    }

}


[System.Serializable]
public class WallsArr
{   
    public GameObject[] wall;
    public GameObject[] doors;
}

