using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    BlockSpawn,
    BlockStack,
    AfterBlockStack,    
    AfterMove
}
public class EventBus 
{   
    static IDictionary<EventType,UnityEvent> events = new Dictionary<EventType,UnityEvent>();
    
    public static void Subscribe(EventType type, UnityAction listener)
    {

        if (events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            events.Add(type, thisEvent);
        }
    }

    public static void Unsubscribe(EventType type,UnityAction listener)
    {
        if(events.TryGetValue(type,out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }
   
    public static void Publish(EventType type)
    {
        if(events.TryGetValue(type, out UnityEvent thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
