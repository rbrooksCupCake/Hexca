using UnityEngine;
using UnityEngine.UI;

public class UI_Scale_Handler : MonoBehaviour
{
    public float screenWidth;
    public float screenHeight;

    public float screenScale;

    public float referenceHeight  = 1920f;
    public float referenceWidth = 1080f;

    // Update is called once per frame
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        if(screenHeight> referenceHeight)
        {
            screenScale = referenceHeight / screenHeight;
            GetComponent<CanvasScaler>().scaleFactor = screenScale*.85f;
        }
        else if (screenHeight < referenceHeight)
        {
            screenScale = screenHeight / referenceHeight;
            GetComponent<CanvasScaler>().scaleFactor = screenScale*.85f;
        }
        else if (screenHeight == referenceHeight)
        {
            screenScale = 1f;
        }

    }
}
