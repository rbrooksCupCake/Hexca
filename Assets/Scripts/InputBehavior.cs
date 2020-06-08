using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputBehavior : MonoBehaviour
{

    public StringVariable inputString;
    [SerializeField]
    private Text inputText=null;

    // Update is called once per frame
    void Update()
    {
        inputString.SetValue(inputText.text);
    }
}
