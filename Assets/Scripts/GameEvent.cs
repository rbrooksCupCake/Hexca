using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{

    private List<GameEventListener> listeners = new List<GameEventListener>();
    public bool enableDebug;
    public string eventName;
    [System.NonSerialized]
    public float timeRaised;

    public void Raise()
    {
        if (listeners.Count > 0)
        {
            for (int i = listeners.Count - 1; i >= 0; --i)
            {
                if(listeners[i]!=null)
                {
                    if (enableDebug)
                    {
                        timeRaised = Time.time;
                    }
                    listeners[i].OnEventRaised();
                }
            }
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }

}
