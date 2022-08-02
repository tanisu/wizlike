using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhaseBase
{
    public PhaseBase nextPhase;
    public abstract IEnumerator Execute(StoreContext _storeContext);
}
