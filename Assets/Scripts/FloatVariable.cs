using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject,ISerializationCallbackReceiver
{
    public float initialValue;

    public float RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {
    }

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "The generic float value.";
#endif

    public void SetValue(float value)
    {
        RuntimeValue = value;
    }

    public void SetValue(FloatVariable value)
    {
        RuntimeValue = value.RuntimeValue;
    }

    public void ApplyChange(float amount)
    {
        RuntimeValue += amount;
    }

    public void ApplyChange(FloatVariable amount)
    {
        RuntimeValue += amount.RuntimeValue;
    }
}
