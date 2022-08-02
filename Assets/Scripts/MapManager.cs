using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MapManager : MonoBehaviour
{
    [SerializeField] TextAsset MapFile;
    [SerializeField] GameObject[] prefabs,doorPrefabs;
    [SerializeField] GameObject playerPrefabs,Map2D;

    //[SerializeField] MapDatas MapDatas;


    WALL_TYPE[,] tileTable,doorTable;
    public int[,] tileTableInt,doorTableInt;
    DIRECTION[] directions;
    float tileSize;
    Vector2 centerPos;
    

    
    public Player player;
    public Dictionary<GameObject, Vector2Int> moveObjPosOnTile = new Dictionary<GameObject, Vector2Int>();
    public Dictionary<Vector2Int, GameObject> mapTiles = new Dictionary<Vector2Int, GameObject>();
    public int maxX, maxY;


    public int[,] wallPattern = new int[16, 4] {
    {0,0,0,0 },{1,0,0,0 },{0,1,0,0 },{0,0,1,0 },{0,0,0,1 },
    {1,1,0,0 },{0,1,1,0 },{0,0,1,1 },{1,0,0,1 },{1,0,1,0 },
    {0,1,0,1 }, {0,1,1,1 },{1,0,1,1 },{1,1,0,1 },{1,1,1,0 },{ 1,1,1,1}
    };

    public void LoadMapData()
    {

        //warps = MapDatas.warps;
        string[] lines = MapFile.text.Split(new[] { '\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        int row = lines.Length;
        int col = lines[0].Split(new[] { ',' }).Length;
        maxY = row;
        maxX = col;
        tileTable = new WALL_TYPE[col, row];
        doorTable = new WALL_TYPE[col, row];
        tileTableInt = new int[col, row];
        doorTableInt = new int[col, row];



        foreach (var (line,y) in lines.Select((_line,_y) => (_line,_y)))
        {
            
            string[] values = line.Split(new[] { ',' });
            
            foreach(var (value,x) in values.Select((_value,_x) => (_value,_x)))
            {
  
                char door = value[0];
                char wall = value[1];
                tileTableInt[x,y] = int.Parse(wall.ToString(), System.Globalization.NumberStyles.HexNumber);
                doorTableInt[x, y] = int.Parse(door.ToString(), System.Globalization.NumberStyles.HexNumber);
                tileTable[x, y] = (WALL_TYPE)int.Parse(wall.ToString(),System.Globalization.NumberStyles.HexNumber);
                doorTable[x, y] = (WALL_TYPE)int.Parse(door.ToString(),System.Globalization.NumberStyles.HexNumber);
                
            }
        }
    }

 

    public void CreateMap2D()
    {
        //タイルサイズ取得
        tileSize = prefabs[0].GetComponent<SpriteRenderer>().bounds.size.x;

        //中心位置取得
        //縦横の個数を半分にしてタイルサイズを掛ける
        centerPos.x = (tileTable.GetLength(0) / 2) * tileSize;
        centerPos.y = (tileTable.GetLength(1) / 2) * tileSize;

        directions = (DIRECTION[])Enum.GetValues(typeof(DIRECTION));

        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for(int x = 0;x < tileTable.GetLength(0); x++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                GameObject obj = Instantiate(prefabs[(int)tileTable[x,y]], transform.position,Quaternion.identity,Map2D.transform);
                
                obj.transform.localPosition = GetScreenPositionFromTileTable(pos);
                mapTiles.Add(pos, obj);
                
                if (doorTable[x, y] > 0)
                {
                    string[] doors = doorTable[x, y].ToString().Split("_");
                    foreach (DIRECTION d in directions)
                    {
                        if (Array.IndexOf(doors, d.ToString()) != -1)
                        {
                            GameObject doorObj = Instantiate(doorPrefabs[(int)d], transform.position,Quaternion.identity,obj.transform);
                            doorObj.transform.localPosition = Vector2.zero;
                        }
                    }
                }
                obj.SetActive(false);

                //Player初期位置
                if (y==0 && x == 0)
                {

                    GameObject playerObj = Instantiate(playerPrefabs, GetScreenPositionFromTileTable(pos), Quaternion.identity,transform);
                    
                    playerObj.transform.localPosition = GetScreenPositionFromTileTable(pos);
                    player = playerObj.GetComponent<Player>();
                    moveObjPosOnTile.Add(playerObj, pos);
                }
            }
        }
    }

    public Vector2 GetScreenPositionFromTileTable(Vector2Int _pos)
    {
        return new Vector2(
            _pos.x * tileSize - centerPos.x ,
            (_pos.y *tileSize - centerPos.y) *-1
            );
    }

    public WALL_TYPE GetNextTileTable(Vector2Int _pos, bool isDoor = false)
    {


        if (isDoor)
        {
            return doorTable[_pos.x, _pos.y];
        }
        return tileTable[_pos.x, _pos.y];
    }

    public int GetTileTalbe(Vector2Int _pos,int _d,bool isDoor = false)
    {
        if (isDoor)
        {
            return wallPattern[doorTableInt[_pos.x, _pos.y], _d];
        }
        return wallPattern[tileTableInt[_pos.x, _pos.y], _d];
    }

    public void Show2DMap(Vector2Int _pos)
    {
        mapTiles[_pos].SetActive(true);        
    }

  

    
}
