using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class ScoreBehavior : MonoBehaviour
{
    public TextMeshProUGUI FinalPlayerScore;
    public ULongVariable playerScore;

    private void LateUpdate()
    {
        FinalPlayerScore.text = playerScore.RuntimeValue.ToString("D5");
    }
}
