using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPhase : PhaseBase
{
    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;
        _storeContext.StorePanel.SetActive(true);
        _storeContext.MessageBoard.GetComponentInChildren<Text>().text = _storeContext.CurrentStore.message;
        _storeContext.MessageBoard.SetActive(true);
        _storeContext.SignboardText.text = _storeContext.CurrentStore.name;

        
        nextPhase = new CommandPhase();
    }
}
