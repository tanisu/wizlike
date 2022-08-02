using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPhase : PhaseBase
{
    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;
        Debug.Log("SellPhase");

        nextPhase = new BuyPhase();
    }
}
