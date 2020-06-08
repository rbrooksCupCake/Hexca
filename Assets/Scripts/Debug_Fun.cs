using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Debug_Fun : MonoBehaviour
{
    public GameEvent Spawn;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Spawn.Raise();
        }
    }
}
