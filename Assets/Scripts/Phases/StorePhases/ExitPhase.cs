using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StorePhase;
public class ExitPhase : PhaseBase
{
    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;
        _storeContext.SignboardText.text = "";
        _storeContext.StoreCommandBoard.GetComponent<CommandController>().ResetArrow();
        _storeContext.StoreCommandBoard.SetActive(false);
        _storeContext.StoreListPanel.SetActive(false);
        _storeContext.StorePanel.SetActive(false);
        _storeContext.MessageBoard.SetActive(false);
        _storeContext.MessageBoard.GetComponentInChildren<Text>().text = "";
        _storeContext.StoreListPanel.GetComponent<StoreListController>().DeleteStoreList(_storeContext.StoreListPanel.transform);
        
        
        
        GameManager.I.CanMove();
    }
}
