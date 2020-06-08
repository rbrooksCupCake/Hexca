using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    public bool oneShot;
    [System.NonSerialized]
    public GameObject attachedObject;

    private void OnEnable()
    {
        attachedObject = gameObject;
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
            Response.Invoke();
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
