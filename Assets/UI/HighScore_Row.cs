using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScore_Row
{
    [SerializeField]
    public int playerScore;
    [SerializeField]
    public string playerName;

    public HighScore_Row(string name, int score)
    {
        playerScore = score;
        playerName = name;
    }
}
