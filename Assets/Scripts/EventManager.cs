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

// TODO: Implement Awake() method
// - Set up the Singleton pattern (if Instance is null, set it to this, else destroy this GameObject)
// - Optional: Add DontDestroyOnLoad(gameObject) to persist across scenes

// TODO: Implement Subscribe() method
// - Store the listener for the given eventName in a dictionary
// - Dictionary structure: Dictionary<string, List<Action<object>>>
// - If eventName doesn't exist in dictionary, create a new entry
// - Add the listener to the list for that eventName

// TODO: Implement Unsubscribe() method
// - Remove the listener from the list associated with the eventName
// - Check if eventName exists in the dictionary before attempting to remove
// - Optional: Remove the dictionary entry if the list becomes empty

// TODO: Implement TriggerEvent() method
// - Check if the eventName exists in the dictionary
// - If it exists, loop through all listeners for that event
// - Invoke each listener with the provided data parameter
