using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Battle;

public class BattleSystem : MonoBehaviour
{
    public UnityAction OnBattleEnd;
    [SerializeField] GameObject EncountPanel,BattleDialog,PlayerNamePanel,SelectActionPanel;
    [SerializeField] Text BattleDialogText;

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit Enemies;
    PhaseBase phaseState;
    BattleContext battleContext;
    public void BattleStart()
    {
        
        battleContext = new BattleContext(BattleDialog, EncountPanel,   PlayerNamePanel, SelectActionPanel, BattleDialogText, Enemies);
        phaseState = new BattleStartPhase();
        StartCoroutine(_battleStart());
        
    }

    private void _battleEnd()
    {
        OnBattleEnd?.Invoke();
    }

   

    IEnumerator _battleStart()
    {
        while (!(phaseState is BattleEndPhase))
        {
            yield return phaseState.Execute(battleContext);
            phaseState = phaseState.nextPhase;

        }
        yield return phaseState.Execute(battleContext);
        _battleEnd();

    }
}

public struct BattleContext{
    public GameObject EncountPanel,BattleDialog,PlayerNamePanel,SelectActionPanel;
    public Text BattleDialogText;
    public BattleUnit Enemies;
    public BattleContext(GameObject _battleDialog,GameObject _encountPanel,GameObject _playerNamePanel,GameObject _selectActionPanel,Text _battleDialogText, BattleUnit _enemies)
    {
        BattleDialog = _battleDialog;
        EncountPanel = _encountPanel;
        PlayerNamePanel = _playerNamePanel;
        SelectActionPanel = _selectActionPanel;
        BattleDialogText = _battleDialogText;
        Enemies = _enemies;

    }
}