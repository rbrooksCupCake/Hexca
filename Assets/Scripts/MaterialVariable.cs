using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Material Variable")]
public class MaterialVariable : ScriptableObject
{
    public Material theMaterial;



    public void ChangeHoloColor(Color newColor)
    {
        theMaterial.SetColor("_MainColor", newColor);
    }

    public void ChangeRimColor(Color newColor)
    {
        theMaterial.SetColor("_RimColor", newColor);
    }

    public void ResetColor()
    {
        theMaterial.SetColor("_Color", Color.white);
        //theMaterial.SetColor("_MainColor", Color.white);
        //theMaterial.SetColor("_RimColor", Color.white);
    }

    public void ChangeColor(Color newColor)
    {
        theMaterial.color = newColor;
    }

    public void ChangeMaterial(Material newMaterial)
    {
        theMaterial = newMaterial;
    }

    public Material getMaterial()
    {
        return theMaterial;
    }

    public void ChangeMaterial(MaterialVariable newMaterial)
    {
        theMaterial = newMaterial.getMaterial();
    }
}
