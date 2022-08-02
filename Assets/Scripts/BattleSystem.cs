using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    public UnityAction OnBattleEnd;

    public void BattleStart()
    {
        
    }

    private void _battleEnd()
    {
        OnBattleEnd?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _battleEnd();
        }
    }
}
