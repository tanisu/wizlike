using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyNamePanel : MonoBehaviour
{
    [SerializeField] GameObject EnemisList;

    List<GameObject> enemiesList = new List<GameObject>();
    
    public void InitList(Battler battler)
    {
        GameObject enemy = Instantiate(EnemisList,transform);
        enemiesList.Add(enemy);
        enemy.GetComponent<EnemyListTexts>().InitDatas(battler);
        
    }

    public void ResetList()
    {
        foreach(GameObject _enemy in enemiesList)
        {
            Destroy(_enemy);
        }
    }
}
