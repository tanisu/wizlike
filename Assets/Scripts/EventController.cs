using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    [SerializeField] GameObject MessagePanel;
    [SerializeField] EventDatas EventData;
    [SerializeField] Text message;
    Dictionary<Vector2Int, EventType> events = new Dictionary<Vector2Int, EventType>();
    Dictionary<Vector2Int, Vector2Int> warps = new Dictionary<Vector2Int, Vector2Int>();
    Dictionary<Vector2Int, string> messages = new Dictionary<Vector2Int, string>();
    

    
    void Start()
    {
        foreach (Events e in EventData.events)
        {
            events.Add(e.pos, e.eventType);
            
        }
        foreach (Warps w in EventData.warps)
        {
            warps.Add(w.warpIn, w.warpOut);
        }
        foreach(Messages m in EventData.messages)
        {
            messages.Add(m.pos, m.message);
        }

    }

    
    public Vector2Int Warp(Vector2Int _pos)
    {
        return warps[_pos];
    }

    public EventType CheckEvents(Vector2Int _pos)
    {
        if (events.ContainsKey(_pos))
        {
            return events[_pos];
        }
        return EventType.NONE;
    }

    public void Messages(Vector2Int _pos)
    {
        message.text = messages[_pos];
        MessagePanel.SetActive(true);
    }



    public void HideMassages()
    {
        message.text = "";
        MessagePanel.SetActive(false);
    }

    public void ShowMessage(string _message)
    {
        message.text = _message;
        MessagePanel.SetActive(true);
    }
}
