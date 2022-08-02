using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaymentPhase : PhaseBase
{
    private Wepons wepons;

    public PaymentPhase(Wepons _wepons)
    {
        wepons = _wepons;
    }

    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;
        _storeContext.ChoicePanel.SetActive(true);
        
        ChoiceController choiceController = _storeContext.ChoicePanel.GetComponentInChildren<ChoiceController>();
        Text paymentText = _storeContext.ChoicePanel.GetComponentInChildren<Text>();

        paymentText.text = $"{wepons.name}��{wepons.cost}�ł��B\n��낵���ł����H";

        choiceController.InitCommand();
        choiceController.OnSelectable();

        int currentCommand = 0;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        currentCommand = choiceController.currentId;
        choiceController.OffSelectable();
        if(currentCommand == 0)
        {
            paymentText.text = $"�悵�A�������I";
            yield return new WaitForSeconds(0.5f);
            _storeContext.ChoicePanel.SetActive(false);
            choiceController.OffSelectable();
            nextPhase = new SelectItemsPhase();
        }
        else
        {
            _storeContext.ChoicePanel.SetActive(false);
            choiceController.OffSelectable();
            nextPhase = new SelectItemsPhase();
        }

    }
}
