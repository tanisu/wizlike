using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StorePhase;

public class CommandPhase : PhaseBase
{
    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;

        
        CommandController commandController = _storeContext.StoreCommandBoard.GetComponent<CommandController>();
        commandController.InitCommand();
        commandController.OnSelectable();
        int currentCommand = 0;
        
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        currentCommand = commandController.currentId;
        commandController.OffSelectable();
        switch (currentCommand)
        {
            case 0:
                nextPhase = new SelectItemsPhase();
                break;
            case 1:
                nextPhase = new SellPhase();
                break;
            case 2:
                nextPhase = new TalkPhase();
                break;
            case 3:
                nextPhase = new ExitPhase();
                break;

        }
        
    }
}
