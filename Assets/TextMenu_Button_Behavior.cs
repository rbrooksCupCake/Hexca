using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class TextMenu_Button_Behavior : MonoBehaviour,IPointerDownHandler
{
    public Button ThisButton;
    public void OnPointerDown(PointerEventData eventData)
    {
        ThisButton.onClick.Invoke();
    }
}
