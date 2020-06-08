using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSpawnHandler : MonoBehaviour
{

    public static bool TutorialActive;
    public static bool CanTap;
    public int PhaseLevel;

    public SpawnBoxBehavior SpawnBoxOne;
    public SpawnBoxBehavior SpawnBoxTwo;
    public SpawnBoxBehavior SpawnBoxThree;
    public SpawnBoxBehavior SpawnBoxFour;
    public SpawnBoxBehavior SpawnBoxFive;

    public FloatVariable TimeBetweenLoads;

    public ScoringSystem TutorialScore;

    public float TutorialTimeBetweenLoads;

    public GameEvent GoToMainMenu;

    public GameObject TapHere;
    public static GameObject TapHereInstanced;
    public TextMeshProUGUI MultiplierText;
    public TextMeshProUGUI MultiplierOutline;
    public TextMeshProUGUI TutorialText;
    public TextMeshProUGUI TutorialOutline;
    public void SpawnNullLine()
    {
        SpawnBoxOne.Spawn(-1);
        SpawnBoxTwo.Spawn(-1);
        SpawnBoxThree.Spawn(-1);
        SpawnBoxFour.Spawn(-1);
        SpawnBoxFive.Spawn(-1);
    }

    public void SpawnTwoBlue()
    {
        SpawnBoxOne.Spawn(-1);
        SpawnBoxTwo.Spawn(-1);
        SpawnBoxThree.Spawn(0);
        SpawnBoxFour.Spawn(0);
        SpawnBoxFive.Spawn(-1);
    }

    public void SpawnThreeBlue()
    {
        SpawnBoxOne.Spawn(0);
        SpawnBoxTwo.Spawn(0);
        SpawnBoxThree.Spawn(0);
        SpawnBoxFour.Spawn(-1);
        SpawnBoxFive.Spawn(-1);
    }

    public void SpawnOneBlue()
    {
        SpawnBoxOne.Spawn(0);
        SpawnBoxTwo.Spawn(-1);
        SpawnBoxThree.Spawn(-1);
        SpawnBoxFour.Spawn(-1);
        SpawnBoxFive.Spawn(-1);
    }

    public void SpawnAllBlue()
    {
        SpawnBoxOne.Spawn(0);
        SpawnBoxTwo.Spawn(0);
        SpawnBoxThree.Spawn(0);
        SpawnBoxFour.Spawn(0);
        SpawnBoxFive.Spawn(0);
    }

    public void SpawnBlueT_Bottom()
    {
        SpawnBoxOne.Spawn(-1);
        SpawnBoxTwo.Spawn(-1);
        SpawnBoxThree.Spawn(0);
        SpawnBoxFour.Spawn(-1);
        SpawnBoxFive.Spawn(-1);
    }

    public void SpawnBlueT_Middle()
    {
        SpawnBoxOne.Spawn(-1);
        SpawnBoxTwo.Spawn(0);
        SpawnBoxThree.Spawn(0);
        SpawnBoxFour.Spawn(0);
        SpawnBoxFive.Spawn(-1);
    }

    public void SpawnBlueT_Top()
    {
        SpawnBoxOne.Spawn(-1);
        SpawnBoxTwo.Spawn(-1);
        SpawnBoxThree.Spawn(0);
        SpawnBoxFour.Spawn(-1);
        SpawnBoxFive.Spawn(-1);
    }

    public void Reset()
    {

        SpawnBoxOne.RestartWithoutColor();
        SpawnBoxTwo.RestartWithoutColor();
        SpawnBoxThree.RestartWithoutColor();
        SpawnBoxFour.RestartWithoutColor();
        SpawnBoxFive.RestartWithoutColor();
    }

    void ResetTutorialMessages()
    {
        CanTap = false;
        TutorialText.text = "";
        TutorialOutline.text = "";
        TapHereInstanced?.SetActive(false);
    }

    void SetTutorialText(string text)
    {
        TutorialText.text = text;
        TutorialOutline.text = text;
    }

    IEnumerator Tutorial_PhaseOne()
    {
        TutorialActive = true;
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnTwoBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        if(TapHereInstanced==null)
        {
            TapHereInstanced = Instantiate(TapHere, new Vector3(3, 2, -1), Quaternion.identity);
        }
        CanTap = true;
        SetTutorialText("Tap 2 or more" + "\n" + "adjoining" + "\n" + "hexes to make" + "\n" + "a match.");
    }

    IEnumerator Tutorial_PhaseTwo()
    {
        ResetTutorialMessages();
        yield return new WaitForSeconds(.75f);
        SpawnBoxOne.ClearBoard();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnTwoBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnTwoBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        yield return new WaitForSeconds(.75f);
        CanTap = true;
        SetTutorialText("Certain shapes" + "\n" + "flip adjoining" + "\n" + "hexes to the" + "\n" + " color of the shape.");
        TapHereInstanced.SetActive(true);
        Reset();
    }


    IEnumerator Tutorial_PhaseFour()
    {
        ResetTutorialMessages();
        yield return new WaitForSeconds(.75f);
        SpawnBoxOne.ClearBoard();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnAllBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(.75f);
        CanTap = true;
        TapHereInstanced.SetActive(true);
        Reset();
    }

    IEnumerator Tutorial_PhaseFive()
    {
        ResetTutorialMessages();
        yield return new WaitForSeconds(.75f);
        SpawnBoxOne.ClearBoard();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnOneBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnOneBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(.75f);
        CanTap = true;
        SetTutorialText("A single hex" + "\n" + "can be stored" + "\n");

        TapHereInstanced.transform.position = new Vector3(1, 1, -1);
        TapHereInstanced.SetActive(true);
        Reset();
    }

    IEnumerator Tutorial_PhaseSix()
    {
        ResetTutorialMessages();
        yield return new WaitForSeconds(.75f);
        SpawnBoxOne.ClearBoard();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnBlueT_Bottom();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnBlueT_Middle();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnBlueT_Top();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(.75f);
        CanTap = true;
        if(TapHereInstanced!=null)
        {
            TapHereInstanced.transform.position = new Vector3(3, 3, -1);
            TapHereInstanced.SetActive(true);
        }
    }

    IEnumerator Tutorial_PhaseThree()
    {
        ResetTutorialMessages();
        yield return new WaitForSeconds(.75f);
        SpawnBoxOne.ClearBoard();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnThreeBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnOneBlue();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        SpawnNullLine();
        yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);
        Reset();
    }

    public void StartTutorial()
    {
        MultiplierBehavior.MultiplierEnabled = false;
        MultiplierOutline.text = "";
        MultiplierText.text = "";
        CanTap = false;
        TutorialActive = true;
        TimeBetweenLoads.RuntimeValue = TutorialTimeBetweenLoads;
        StartCoroutine(Tutorial_PhaseOne());
    }

    private void Update()
    {
        
        if(TutorialScore.playerScore.RuntimeValue>=20 && PhaseLevel == 0)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            StartCoroutine(Tutorial_PhaseTwo());
            ++PhaseLevel;
        }
        if (TutorialScore.playerScore.RuntimeValue > 0 && TutorialScore.playerScore.RuntimeValue >= 600 && PhaseLevel == 1)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            StartCoroutine(Tutorial_PhaseThree());
            ++PhaseLevel;
        }
        if (TutorialScore.playerScore.RuntimeValue > 0 && TutorialScore.playerScore.RuntimeValue >= 600 && PhaseLevel == 2)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            ++PhaseLevel;
            StartCoroutine(Tutorial_PhaseFour());
        }
        if (TutorialScore.playerScore.RuntimeValue > 0 && TutorialScore.playerScore.RuntimeValue >= 1220 && PhaseLevel == 3)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            ++PhaseLevel;
            StartCoroutine(Tutorial_PhaseFive());
        }
        if (TutorialScore.playerScore.RuntimeValue > 0 && TutorialScore.playerScore.RuntimeValue >= 1220 && PhaseLevel == 4  && HexBehavior.boxOneStored)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            SetTutorialText("Tap the stored hex" + "\n" + " to place it back" + "\n" +  "in the column.");
            TapHereInstanced.transform.position = new Vector3(1, 0, -2);
            TapHereInstanced.SetActive(true);
            ++PhaseLevel;
        }
        if (TutorialScore.playerScore.RuntimeValue > 0 && TutorialScore.playerScore.RuntimeValue >= 1220 && PhaseLevel == 5 && !HexBehavior.boxOneStored)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            SetTutorialText("");
            TapHereInstanced.SetActive(false);
        }
        if (TutorialScore.playerScore.RuntimeValue>0 && TutorialScore.playerScore.RuntimeValue>=1240 && PhaseLevel == 5)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            ++PhaseLevel;
            StartCoroutine(Tutorial_PhaseSix());
        }
        if (TutorialScore.playerScore.RuntimeValue > 0 && TutorialScore.playerScore.RuntimeValue >= 6000 && PhaseLevel == 6)
        {
            TutorialScore.colorMultiplier = 1;
            TutorialScore.colorMultiplierLevel = 0;
            ++PhaseLevel;
            TapHereInstanced.SetActive(false);
            GoToMainMenu.Raise();
        }
    }

}
