using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_Title_Behavior : MonoBehaviour
{

    private string cursor = "▐";
    public string typedText;
    private int trackedIndex=0;
    public Text theText;
    public FloatReference timeBetweenCharacters;
    private bool isTyping;
    [SerializeField]
    private bool includeCursor = false;
    
    IEnumerator typeText()
    {
        while (trackedIndex <= typedText.Length)
        {
            if(includeCursor)
            {
                theText.text = typedText.Substring(0, trackedIndex) + cursor;
            }
            else
            {
                theText.text = typedText.Substring(0, trackedIndex);

            }
            yield return new WaitForSecondsRealtime(timeBetweenCharacters);
            ++trackedIndex;
        }
        if(includeCursor)
        {
            if(trackedIndex>=typedText.Length)
            {
                theText.enabled = false;
            }
        }
    }

    public void TypeText()
    {
        if(!isTyping)
        {
            theText.enabled = true;
            StartCoroutine(typeText());
            isTyping = true;
        }
    }
}
