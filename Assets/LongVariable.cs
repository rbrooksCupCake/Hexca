using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ULongVariable : ScriptableObject,
ISerializationCallbackReceiver
{
    public ulong initialValue;

    public ulong RuntimeValue;

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

    public void SetValue(ulong value)
    {
        RuntimeValue = value;
    }

    public void SetValue(ULongVariable value)
    {
        RuntimeValue = value.RuntimeValue;
    }

    public void ApplyChange(ulong amount)
    {
        RuntimeValue += amount;
    }

    public void ApplyChange(ULongVariable amount)
    {
        RuntimeValue += amount.RuntimeValue;
    }
}
