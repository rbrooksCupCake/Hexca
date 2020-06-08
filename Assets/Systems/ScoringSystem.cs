using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Systems/Scoring System")]
public class ScoringSystem : ScriptableObject
{
    public static Profile CurrentProfile;
    public ULongVariable playerScore;
    public ulong shapeMultiplier;
    public HexColor previousTappedColor;
    public ulong colorMultiplier=1;
    public int colorMultiplierLevel=0;
    public Text scoreText;
    public IntVariable currentLevel;
    public LevelData theLevelData;
    public FloatVariable TimeBetweenLoads;

    public MultiplierBehavior MyMultiplierBehavior;

    private void Awake()
    {
        previousTappedColor = null;
        colorMultiplier = 1;
        colorMultiplierLevel = 0;
    }

    public void AddScore(ulong amount)
    {
        if(colorMultiplierLevel > 0)
        {
            playerScore.RuntimeValue += amount * shapeMultiplier * colorMultiplier;
        }
        else
        {
            playerScore.RuntimeValue += amount * shapeMultiplier;
        }
        if (theLevelData.LevelInfo.Count > currentLevel.RuntimeValue)
        {
            if (theLevelData.LevelInfo[currentLevel.RuntimeValue].neededScore <= playerScore.RuntimeValue)
            {
                ++currentLevel.RuntimeValue;
                TimeBetweenLoads.RuntimeValue = theLevelData.LevelInfo[currentLevel.RuntimeValue].SpawnTime;
            }
        }
    }

    public void UpdateShapeMultiplier(ulong multiplierAmnt)
    {
        shapeMultiplier = multiplierAmnt;
    }

    public void IncreaseColorMultiplier(HexColor tappedColor)
    {
        if(colorMultiplierLevel==0)
        {
            previousTappedColor = tappedColor;
        }
        if(previousTappedColor == tappedColor)
        {
            if (colorMultiplierLevel < 10)
            {
                ++colorMultiplierLevel;
                if (colorMultiplierLevel<=1)
                {
                    colorMultiplier = 1;
                }
                if(colorMultiplierLevel>1)
                {
                    MultiplierBehavior.UpdateOutline();
                }
                if (colorMultiplierLevel ==2)
                {
                    colorMultiplier = 2;
                }
                if(colorMultiplierLevel == 3)
                {
                    colorMultiplier = 3;
                }
                if(colorMultiplierLevel == 4)
                {
                    colorMultiplier = 5;
                }
                if(colorMultiplierLevel == 5)
                {
                    colorMultiplier = 10;
                }
                if (colorMultiplierLevel == 6)
                {
                    colorMultiplier = 15;
                }
                if(colorMultiplierLevel == 7)
                {
                    colorMultiplier = 20;
                }
                if(colorMultiplierLevel == 8)
                {
                    colorMultiplier = 25;
                }
                if(colorMultiplierLevel == 9)
                {
                    colorMultiplier = 50;
                }
                if(colorMultiplierLevel >= 10)
                {
                    colorMultiplier = 100;
                }
                MultiplierBehavior.UpdateMultiplierLabel();
            }
        }
        else
        {
            ResetColorMultiplier();
        }
        previousTappedColor = tappedColor;
    }

    public void ResetColorMultiplier()
    {
        colorMultiplierLevel = 1;
        colorMultiplier = 1;
        MultiplierBehavior.UpdateMultiplierLabel();
        MultiplierBehavior.ResetOutline();
    }

    public void ResetScore()
    {
        GameOverBox_Behavior.IsGameOver = false;
        currentLevel.RuntimeValue = 0;
        playerScore.RuntimeValue = 0;
        shapeMultiplier = 1;
        colorMultiplier = 0;
        HexBehavior.ResetStoringBoxes();
        Time.timeScale = 1.0f;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1.0f;
    }

    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }

    public static void PauseTimeStatic()
    {
        Time.timeScale = 0.0f;
    }
    public static void ResumeTimeStatic()
    {
        Time.timeScale = 1.0f;
    }


    public void CreateProfile()
    {
        CurrentProfile = new Profile();
    }

    public void UpdateScoreText()
    {
        scoreText.text = playerScore.RuntimeValue + " ";
    }
}
