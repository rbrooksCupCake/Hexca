using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor_Flicker_Effect : MonoBehaviour
{

    public FloatReference timeBetweenFlickers;
    public Text titleText;
    public string finalText;
    private bool isCoroutineRunning;
    private bool isTextComplete;
    private int indexTracker;

    IEnumerator flicker()
    {
       

            isCoroutineRunning = true;
            titleText.color = Color.white;
            yield return new WaitForSeconds(timeBetweenFlickers);
            SpellOut();
            titleText.color = Color.clear;
            ++indexTracker;
        
        isCoroutineRunning = false;
        
    }

    void SpellOut()
    {
        while (indexTracker != finalText.Length + 1)
        {
            titleText.text = finalText.Substring(0, indexTracker) + "▐";
        }
    }

        // Update is called once per frame
        void Update()
    {
        if(!isCoroutineRunning)
        {
            StartCoroutine(flicker());
        }
    }
}
