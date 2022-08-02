using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class StoreController : MonoBehaviour
{
    [SerializeField] StoreDatas storeDatas;
    [SerializeField] GameObject StorePanel,StoreListPanel,StoreCommandBoard,MessageBoard,ChoicePanel;
    
    [SerializeField] Text signboardText;
    
    public int currentId;
    
    Dictionary<Vector2Int, Store> storeData = new Dictionary<Vector2Int, Store>();
    Store currentStore;
    
    PhaseBase phaseState;
    public StoreContext storeContext;


    private void Start()
    {
        foreach (Store s in storeDatas.store)
        {
            storeData.Add(s.pos, s);
        }
    }

    public void HideSignboard()
    {
        signboardText.text = "";
        StorePanel.SetActive(false);
    }

    public void StoreEntrance(Vector2Int _pos)
    {
        signboardText.text = storeData[_pos].name;
        StorePanel.SetActive(true);
    }

    public void EnterStore(Vector2Int _pos)
    {
        currentStore = storeData[_pos];
        storeContext = new StoreContext(signboardText,StorePanel, StoreListPanel, StoreCommandBoard, MessageBoard,ChoicePanel,currentStore);
        phaseState = new StartPhase();
        StartCoroutine(_shopping());
    }

    IEnumerator _shopping()
    {

        while(!(phaseState is ExitPhase))
        {
            yield return phaseState.Execute(storeContext);
            phaseState = phaseState.nextPhase;
            
        }
        yield return phaseState.Execute(storeContext);

    }

}


public struct StoreContext
{
    public GameObject StorePanel, StoreListPanel,StoreCommandBoard,MessageBoard,ChoicePanel;
    public Store CurrentStore;
    public Text SignboardText;

    public StoreContext(Text _signboardText, GameObject _storePanel,GameObject _storeListPanel,GameObject _storeCommandBoard,GameObject _messageBoard,GameObject _paymentPanel,Store _currentStore)
    {
        SignboardText = _signboardText;
        StorePanel = _storePanel;
        StoreListPanel = _storeListPanel;
        StoreCommandBoard = _storeCommandBoard;
        MessageBoard = _messageBoard;
        ChoicePanel = _paymentPanel;
        CurrentStore = _currentStore;

    }
}