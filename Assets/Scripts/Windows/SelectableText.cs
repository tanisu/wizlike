using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableText : Selectable
{
    public UnityAction<Transform> OnSelectAction = null;
    //[SerializeField] bool defaultSelect;
    public override void OnDeselect(BaseEventData eventData)
    {
        //base.OnDeselect(eventData);
        //Debug.Log($"{gameObject.name}‚Ì‘I‘ð‚ª‚Í‚¸‚ê‚½");
    }

    public override void OnSelect(BaseEventData eventData)
    {
        //base.OnSelect(eventData);
        //Debug.Log($"{gameObject.name}‚ª‘I‘ð‚³‚ê‚½");
        OnSelectAction?.Invoke(transform);
    }

    public override void Select()
    {
        base.Select();
        OnSelectAction.Invoke(transform);
    }
}
