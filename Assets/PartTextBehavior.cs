using UnityEngine;

public class PartTextBehavior : MonoBehaviour
{
    public Material textMaterial;
    public float Speed;

    public void ChangeColor(Color32 newColor) 
    {
        textMaterial.SetColor("_RimColor", newColor);
    }
}
