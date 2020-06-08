using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject, 
ISerializationCallbackReceiver
{
    public int initialValue;

    public int RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {
    }

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "The generic int value.";
#endif

    public void SetValue(int value)
    {
        RuntimeValue = value;
    }

    public void SetValue(IntVariable value)
    {
        RuntimeValue = value.RuntimeValue;
    }

    public void ApplyChange(int amount)
    {
        RuntimeValue += amount;
    }

    public void ApplyChange(IntVariable amount)
    {
        RuntimeValue += amount.RuntimeValue;
    }
}
