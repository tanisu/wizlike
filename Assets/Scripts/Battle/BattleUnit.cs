using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    public Battler battler { get; set; }

    public virtual void SetUp( Battler _battler)
    {
        battler = _battler;
        battler.Init();
    }

    public virtual void Reset()
    {

    }
}
