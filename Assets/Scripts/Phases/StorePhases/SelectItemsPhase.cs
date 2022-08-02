using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItemsPhase : PhaseBase
{
    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;
        int currentSelect = 0;
        StoreListController storeListController =  _storeContext.StoreListPanel.GetComponent<StoreListController>();
        
        if(_storeContext.CurrentStore.wepons.Count > 0)
        {
            
            storeListController.InitStoreList(_storeContext.CurrentStore.wepons,_storeContext.StoreListPanel.transform);
            storeListController.OnSelectable();
            _storeContext.StoreListPanel.SetActive(true);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            currentSelect = storeListController.currentId;
            
            if(currentSelect < _storeContext.CurrentStore.wepons.Count)
            {
                storeListController.OffSelectable();
                storeListController.DeleteStoreList(_storeContext.StoreListPanel.transform);
                nextPhase = new PaymentPhase(_storeContext.CurrentStore.wepons[currentSelect]);
            }
            else
            {
                storeListController.DeleteStoreList(_storeContext.StoreListPanel.transform);
                nextPhase = new CommandPhase();
            }
                
            
            
        }
        else
        {

            //commandController.OnSelectable();
            nextPhase = new CommandPhase();
        }
        

    }
}
