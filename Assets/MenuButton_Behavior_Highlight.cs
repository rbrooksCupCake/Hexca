using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton_Behavior_Highlight : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public Button ThisButton;
    public TextMeshProUGUI TextButton;
    public Color32 HighlightColor;
    public Color32 DisabledColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ThisButton.targetGraphic.color = ThisButton.colors.highlightedColor;
        TextButton.color = HighlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ThisButton.targetGraphic.color = ThisButton.colors.disabledColor;
        TextButton.color = DisabledColor;
    }
}
