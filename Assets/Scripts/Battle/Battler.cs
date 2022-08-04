using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Battler 
{

    [SerializeField] EnemyDatas _base;
    [SerializeField] int level;

    public EnemyDatas Base { get => _base; }
    public int Level { get => level;}

    public int MaxHp { get; set; }
    public int Hp { get; set; }
    public int Str { get; set; }

    public void Init()
    {
        MaxHp = _base.MaxHp;
        Hp = MaxHp;
        Str = _base.Str;
    }
}
