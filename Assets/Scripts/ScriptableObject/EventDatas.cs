using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData",menuName ="Create MapData")]
public class EventDatas : ScriptableObject
{
    public Vector2Int[] Position;
    public List<Events> events;
    public List<Traps> traps;
    public List<Warps> warps;
    public List<Messages> messages;
    //public List<Shop> shop;
}

[System.Serializable]
public class Events
{   
    public Vector2Int pos;
    public EventType eventType;

}
[System.Serializable]
public class Traps
{
    public Vector2Int pos;
    public enum TrapType
    {
        PIT,
        DARK,
        TRUN
    }
    public TrapType trapType;
}

[System.Serializable]
public class Messages
{
    public Vector2Int pos;
    public string message;
}


[System.Serializable]
public class Warps
{
    public Vector2Int warpIn,warpOut;
}

