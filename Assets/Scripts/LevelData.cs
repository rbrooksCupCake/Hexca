using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public struct LevelNode
{
    public ulong neededScore;
    public float SpawnTime;
}

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [SerializeField]
    public List<LevelNode> LevelInfo = new List<LevelNode>();
}