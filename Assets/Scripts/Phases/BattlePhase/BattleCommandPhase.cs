using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
public class BattleCommandPhase : PhaseBase
{
    public override IEnumerator Execute(BattleContext _battleContext)
    {
        yield return null;
        BattleSelectCommand command = _battleContext.SelectActionPanel.GetComponent<BattleSelectCommand>();
        command.InitCommand();
        command.OnSelectable();
        int currentCommand = 0;


        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        currentCommand = command.currentId;
        command.OffSelectable();
        nextPhase = new BattleEndPhase();
    }
}
