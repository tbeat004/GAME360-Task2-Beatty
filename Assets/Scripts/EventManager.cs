using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    private  readonly Dictionary<string , List<Action<object>>> eventDictionary = new Dictionary<string, List<Action<object>>>();

    private void Awake() 
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
    }

    public void Subscribe(string eventName, Action<object> listener) 
    {
        if (!eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName] = new List<Action<object>>();
        }
        eventDictionary[eventName].Add(listener);
    }

    public void Unsubscribe(string eventName, Action<object> listener)
    { 
        if (eventDictionary.ContainsKey(eventName))
        {
            eventDictionary[eventName].Remove(listener);
            if (eventDictionary[eventName].Count == 0)
            {
                eventDictionary.Remove(eventName);
            }
        }
    }

    public void TriggerEvent(string eventName, object data = null) 
    {
        if (eventDictionary.ContainsKey(eventName))
        {
            foreach (Action<object> listener in eventDictionary[eventName])
            {
                listener?.Invoke(data);
            }
        }
    }
}

