using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyListTexts : MonoBehaviour
{
    [SerializeField] Text EnemyName, EnemyCount, EnemyActiveCount;

    public void InitDatas(Battler _battler)
    {
        Debug.Log(_battler.Base.name);
        EnemyName.text = _battler.Base.Name;
    }
}
