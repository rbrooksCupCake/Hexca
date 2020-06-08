using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultiplierBehavior : MonoBehaviour
{

    public static TextMeshProUGUI MultiplierLabel;
    public  static TextMeshProUGUI MultiplierOutline;
    public  TextMeshProUGUI MultiplierLabelReference;
    public TextMeshProUGUI MultiplierOutlineReference;

    public static ScoringSystem TheScoringSystem;

    public static bool MultiplierEnabled = true;

    public ScoringSystem ScoringSystemReference;

    private void Awake()
    {
        TheScoringSystem = ScoringSystemReference;
        MultiplierLabel = MultiplierLabelReference;
        MultiplierOutline = MultiplierOutlineReference;
        MultiplierEnabled = true;
    }

    public static void ResetOutline()
    {
        MultiplierOutline.color = Color.white;
    }

    public static void UpdateOutline()
    {
        MultiplierOutline.color = TheScoringSystem.previousTappedColor.hexColor;
    }

    public static void UpdateMultiplierLabel()
    {
        if(MultiplierEnabled)
        {
            MultiplierOutline.text = "X" + TheScoringSystem.colorMultiplier;
            MultiplierLabel.text = "X" + TheScoringSystem.colorMultiplier;
        }
    }
}
