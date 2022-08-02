using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoreData", menuName = "Create StoreData")]
public class StoreDatas : ScriptableObject
{
    public List<Store> store;
    
}


[System.Serializable]
public class Wepons
{
    public string name;
    public int cost;
}


[System.Serializable]
public class Store
{
    public Vector2Int pos;
    public string name;
    [Multiline(3)]
    public string message;
    //public StoreDatas storeDatas;
    public List<Wepons> wepons;
}