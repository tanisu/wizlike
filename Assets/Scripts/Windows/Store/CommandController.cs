using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] protected Transform arrow = default;
    
    
    protected SelectableText[] selectableTexts;
    
    public int currentId;


    public void InitCommand()
    {
        currentId = 0;
        
        
        selectableTexts = GetComponentsInChildren<SelectableText>();
        foreach (SelectableText selectableText in selectableTexts)
        {
            selectableText.OnSelectAction = MoveArrowTo;
        }
        selectableTexts[currentId].Select();
        gameObject.SetActive(true);
    }


    public void MoveArrowTo(Transform _parent)
    {
        
        arrow.SetParent(_parent);
        arrow.SetAsFirstSibling();
        currentId = _parent.GetSiblingIndex();
    }

    public void ResetArrow()
    {
        selectableTexts[0].Select();
    }

    public virtual void OffSelectable()
    {
        foreach(SelectableText selectableText in selectableTexts)
        {
            selectableText.enabled = false;
        }
    }

    public void OnSelectable()
    {
        foreach (SelectableText selectableText in selectableTexts)
        {
         
            selectableText.enabled = true;
        }
    }


}
