using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MaterialPack : ScriptableObject
{
    [Header("Loading Materials")]
    public Material defaultBlue;
    public Material defaultGreen;
    public Material defaultRed;
    public Material defaultOrange;
    public Material defaultPurple;
    public Material defaultNull;

    [Header("Loading Materials")]
    public Material loadingBlue;
    public Material loadingGreen;
    public Material loadingRed;
    public Material loadingOrange;
    public Material loadingPurple;

    [Header("Shatter Materials")]
    public Material shatterBlue;
    public Material shatterGreen;
    public Material shatterRed;
    public Material shatterOrange;
    public Material shatterPurple;

    [Header("Explosions")]
    public GameObject BlueExplosion;
    public GameObject GreenExplosion;
    public GameObject RedExplosion;
    public GameObject OrangeExplosion;
    public GameObject PurpleExplosion;

    [Header("Colors")]
    public Color hexBlue;
    public Color hexGreen;
    public Color hexRed;
    public Color hexOrange;
    public Color hexPurple;
    public Color hexNull;
}
