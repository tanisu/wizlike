using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
public class BattleEndPhase : PhaseBase
{
    public override IEnumerator Execute(BattleContext _battleContext)
    {
        yield return null;
        _battleContext.BattleDialog.SetActive(false);
        _battleContext.SelectActionPanel.SetActive(false);
        _battleContext.PlayerNamePanel.SetActive(false);
        _battleContext.Enemies.Reset();

    }
}
