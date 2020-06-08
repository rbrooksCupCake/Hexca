using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraBehavior : MonoBehaviour
{
    private ZoomParameter zoomParameter;
    private bool isZooming;

    private void Awake()
    {
        StatViewer_Handler.LoadProfiles();
    }

    public void ZoomCamera(ZoomParameter theParameter)
    {
        isZooming = true;
        zoomParameter = theParameter;
    }

    public void ResetCamera()
    {
        GetComponent<Transform>().position = new Vector3(3, 5, -20);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (isZooming)
        {
            if (!(Vector3.Distance(transform.position, zoomParameter.finalPosition) == 0))
            {
                GetComponent<Transform>().position = Vector3.MoveTowards(transform.position, zoomParameter.finalPosition, zoomParameter.ZoomAmnt * Time.deltaTime);
            }
            else
            {
                isZooming = false;
                GetComponent<ScreenShake>().GetOriginalPosition();
                GetComponent<ScreenShake>().originalPositionLocked = true;
            }
        }   
    }


}
