using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkPhase : PhaseBase
{
    public override IEnumerator Execute(StoreContext _storeContext)
    {
        yield return null;
        Debug.Log("TalkPhase");
        nextPhase = new ExitPhase();
    }
}
