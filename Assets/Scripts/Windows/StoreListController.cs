using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreListController : CommandController
{
    
    [SerializeField] GameObject ItemList = default;
    [SerializeField] Text backText;
    List<SelectableText> selectableItemTexts = new List<SelectableText>();
    SelectableText _bakcText;
    public void InitStoreList(List<Wepons> _wepons,Transform _parent)
    {
        currentId = 0;
        foreach (Wepons w in _wepons)
        {
            GameObject obj = Instantiate(ItemList,_parent);
            obj.GetComponentsInChildren<Text>()[0].text = w.name;
            obj.GetComponentsInChildren<Text>()[1].text = w.cost.ToString();
            SelectableText selectableText = obj.GetComponent<SelectableText>();
            selectableItemTexts.Add(selectableText);
            selectableText.OnSelectAction = MoveArrowTo;
        }
        _bakcText = backText.GetComponent<SelectableText>();
        backText.gameObject.transform.SetAsLastSibling();
        _bakcText.enabled = true;
        _bakcText.OnSelectAction = MoveArrowTo;
        
        selectableItemTexts[currentId].Select();
        selectableTexts = selectableItemTexts.ToArray();

    }


    

    public void DeleteStoreList(Transform _tf)
    {
        
        arrow.SetParent(_tf);
        foreach (SelectableText selectableText in selectableItemTexts)
        {
            Destroy(selectableText.gameObject);
        }
        selectableItemTexts.Clear();
        gameObject.SetActive(false);
    }

    public override void OffSelectable()
    {
        foreach (SelectableText selectableText in selectableTexts)
        {
            selectableText.enabled = false;
        }
        _bakcText.enabled = false;
    }

}
