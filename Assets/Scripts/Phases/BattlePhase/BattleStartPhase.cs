using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battle;
public class BattleStartPhase : PhaseBase
{
    public override IEnumerator Execute(BattleContext _battleContext)
    {
        yield return null;
        _battleContext.EncountPanel.SetActive(true);
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        _battleContext.EncountPanel.SetActive(false);
        
        
        _battleContext.PlayerNamePanel.SetActive(true);
        _battleContext.SelectActionPanel.SetActive(true);
        //_battleContext.Enemies.battler.Init();
        _battleContext.Enemies.SetUp(_battleContext.Enemies.GetComponent<EnemyUnitController>().SelectEnemy());
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        nextPhase = new BattleCommandPhase();
    }
}
