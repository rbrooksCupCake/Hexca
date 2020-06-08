using UnityEngine;
using System.Collections;
//Author: Gist.
//Modified by Rukia Brooks.
//Github:https://gist.github.com/ftvs/5822103
public class ScreenShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public static float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    [Range(0,1)]
    public static float shakeAmount = 0.25f;
    public float maxShakeAmount = 1.0f;
    public float decreaseFactor = 1.0f;
    public float originalShakeAmount;

    public GameObject ScreenShakeSlider;

    Vector3 originalPos;

    public bool originalPositionLocked;
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    public void GetOriginalPosition()
    {
        originalPos = camTransform.localPosition;
    }

    public static void Shake()
    {
        shakeDuration = 0.5f;
    }

    public static void PowerShake()
    {
        shakeAmount = .55f;
        shakeDuration = 0.05f;
    }

    public void Reset()
    {
        shakeDuration = 0f;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            //ScreenShakeSlider.GetComponent<sliderBehavior>().previousShakeAmount = shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            if (originalPositionLocked)
            {
                shakeAmount = originalShakeAmount;
                shakeDuration = 0f;
                camTransform.localPosition = originalPos;
            }
        }
    }
}