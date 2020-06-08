using UnityEngine;

[CreateAssetMenu(menuName="Parameters/Zoom_Parameter")]
public class ZoomParameter : ScriptableObject
{
    public FloatReference ZoomAmnt;
    public Vector3 finalPosition;
}
