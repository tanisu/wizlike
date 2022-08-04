using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDatas : ScriptableObject
{
    [SerializeField] new string name;
    
    [SerializeField] int maxHp;
    [SerializeField] int str;
    [SerializeField] Sprite sprite;

    public string Name { get => name; }
    
    public int MaxHp { get => maxHp; }
    public Sprite Sprite { get => sprite; }
    public int Str { get => str;  }
}
