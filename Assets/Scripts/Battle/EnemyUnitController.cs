using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyUnitController : BattleUnit
{
    [SerializeField] Battler[] EnemyBattlers;
    [SerializeField] EnemyNamePanel enemyNamePanel;
    [SerializeField] Image EnemyImage;


    public Battler[] EnemyBattlers1 { get => EnemyBattlers; }

    public Battler SelectEnemy()
    {
        int idx = Random.Range(0, EnemyBattlers.Length);
        return EnemyBattlers[idx];
    }

    public override void SetUp(Battler battler)
    {
        base.SetUp(battler);
        enemyNamePanel.gameObject.SetActive(true);
        EnemyImage.transform.parent.gameObject.SetActive(true);
        EnemyImage.sprite = battler.Base.Sprite;
        enemyNamePanel.InitList(battler);
    }

    public override void Reset()
    {
        enemyNamePanel.ResetList();
        enemyNamePanel.gameObject.SetActive(false);
        EnemyImage.transform.parent.gameObject.SetActive(false);
    }
}
