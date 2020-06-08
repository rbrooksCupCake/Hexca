using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hex/Hex_Color")]
public class HexColor : ScriptableObject
{
    public string colorName;
    public int UniqueID;
    public Color hexColor;
    public Color spawnColor;
    public MaterialVariable loadingMaterial;
    public MaterialVariable defaultMaterial;
    public MaterialVariable shatterMaterial;
}
