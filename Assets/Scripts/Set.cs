using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Set : ScriptableObject
{
    public List<GameObject> Items = new List<GameObject>();

    public void Add(GameObject thing)
    {
        if (!Items.Contains(thing))
            Items.Add(thing);
    }

    public void Remove(GameObject thing)
    {
        if (Items.Contains(thing))
            Items.Remove(thing);
    }
}
