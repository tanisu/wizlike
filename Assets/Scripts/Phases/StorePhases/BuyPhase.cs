using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPhase : PhaseBase
{
    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;
        Debug.Log("BuyPhase");

        nextPhase = new TalkPhase();
    }


}
