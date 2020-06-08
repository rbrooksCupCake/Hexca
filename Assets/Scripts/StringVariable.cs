using UnityEngine;

[CreateAssetMenu]
public class StringVariable : ScriptableObject
{

    public string initialValue;

    [System.NonSerialized]
    public string RuntimeValue;

    private void OnEnable()
    {
        RuntimeValue = initialValue;
    }

    private void OnDisable()
    {
        RuntimeValue = initialValue;
    }

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "The generic int value.";
#endif

    public void SetValue(string value)
    {
        RuntimeValue = value;
    }

    public void SetValue(StringVariable value)
    {
        RuntimeValue = value.RuntimeValue;
    }
}
