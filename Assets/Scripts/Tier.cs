using UnityEngine;

public class Tier : ScriptableObject
{
    public bool isTierMet;
    public IntVariable scoreThreshold;
    public IntVariable currentScore;

    void checkScore()
    {
        if(currentScore.RuntimeValue>=scoreThreshold.RuntimeValue)
        {
            isTierMet = true;
        }
        else
        {
            isTierMet = false;
        }
    }
}
