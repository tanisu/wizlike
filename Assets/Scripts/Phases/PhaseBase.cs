using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StorePhase
{
    public abstract class PhaseBase
    {
        public PhaseBase nextPhase;

        public abstract IEnumerator Execute(StoreContext _storeContext);

    }
}

namespace Battle
{
    public abstract class PhaseBase
    {
        public PhaseBase nextPhase;
        public abstract IEnumerator Execute(BattleContext _battleContext);

    }
}
