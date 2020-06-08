using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class StatViewer_Handler : MonoBehaviour
{

    public RectTransform MyTransform;
    public const float MinimumYPosition = 53f;

    public static Profile RoundProfile;
    public static Profile BestRoundProfile;
    public int BluePopped;
    public int GreenPopped;
    public int RedPopped;
    public int OrangePopped;

    public const string DigitFormat_Score = "D11";
    public const string DigitFormat_Count = "D6";

    public const string NewRecordText = "NEW" + "\n" + "RECORD!";

    public static string RoundToJSON;
    public static string BestRoundJSON;
    public const string Round_FileName = @"\CurrentRound.json";
    public const string RecordRound_FileName = @"\HighScore.json";

    public TextMeshProUGUI ScoreText = null;

    public TextMeshProUGUI RoundHoursText = null;
    public TextMeshProUGUI RoundMinutesText = null;
    public TextMeshProUGUI RoundSecondsText = null;

    public TextMeshProUGUI BluePoppedText = null;
    public TextMeshProUGUI GreenPoppedText = null; 
    public TextMeshProUGUI RedPoppedText = null;
    public TextMeshProUGUI OrangePoppedText = null;
    public TextMeshProUGUI PurplePoppedText = null;

    public TextMeshProUGUI BlueFlippedText = null;
    public TextMeshProUGUI GreenFlippedText = null;
    public TextMeshProUGUI RedFlippedText = null;
    public TextMeshProUGUI OrangeFlippedText = null;
    public TextMeshProUGUI PurpleFlippedText = null;

    public TextMeshProUGUI BlueCubeCreated = null;
    public TextMeshProUGUI GreenCubeCreated = null;
    public TextMeshProUGUI RedCubeCreated = null;
    public TextMeshProUGUI OrangeCubeCreated = null;
    public TextMeshProUGUI PurpleCubeCreated = null;

    public TextMeshProUGUI BlueLCreatedText = null;
    public TextMeshProUGUI GreenLCreatedText = null;
    public TextMeshProUGUI RedLCreatedText = null;
    public TextMeshProUGUI OrangeLCreatedText = null;
    public TextMeshProUGUI PurpleLCreatedText = null;

    public TextMeshProUGUI BlueLineCreatedText = null;
    public TextMeshProUGUI GreenLineCreatedText = null;
    public TextMeshProUGUI RedLineCreatedText = null;
    public TextMeshProUGUI OrangeLineCreatedText = null;
    public TextMeshProUGUI PurpleLineCreatedText = null;

    public TextMeshProUGUI HighestScore = null;

    public static void LoadRoundFile()
    {
        using (StreamReader sr = new StreamReader(Application.persistentDataPath + Round_FileName))
        {
            RoundToJSON = sr.ReadToEnd();
        }
        RoundProfile = JsonUtility.FromJson<Profile>(RoundToJSON);
    }

    public static void LoadRecordFile()
    {
        using (StreamReader sr = new StreamReader(Application.persistentDataPath + RecordRound_FileName))
        {
            BestRoundJSON = sr.ReadToEnd();
        }
        BestRoundProfile = JsonUtility.FromJson<Profile>(BestRoundJSON);
    }

    public static void LoadProfiles()
    {
        if (File.Exists(Application.persistentDataPath + Round_FileName))
        {
            LoadRoundFile();
        }
        if (File.Exists(Application.persistentDataPath + RecordRound_FileName))
        {
            LoadRecordFile();
        }
    }

    private void Start()
    {
        LoadProfiles();
        if (RoundProfile != null)
        {
            ScoreText.text = RoundProfile.Score.ToString(DigitFormat_Score);

            BluePoppedText.text = RoundProfile.BluePopped.ToString(DigitFormat_Count);
            GreenPoppedText.text = RoundProfile.GreenPopped.ToString(DigitFormat_Count);
            RedPoppedText.text = RoundProfile.RedPopped.ToString(DigitFormat_Count);
            OrangePoppedText.text = RoundProfile.OrangePopped.ToString(DigitFormat_Count);
            PurplePoppedText.text = RoundProfile.PurplePopped.ToString(DigitFormat_Count);

            BlueFlippedText.text = RoundProfile.BlueFlipped.ToString(DigitFormat_Count);
            GreenFlippedText.text = RoundProfile.GreenFlipped.ToString(DigitFormat_Count);
            RedFlippedText.text = RoundProfile.RedFlipped.ToString(DigitFormat_Count);
            OrangeFlippedText.text = RoundProfile.OrangeFlipped.ToString(DigitFormat_Count);
            PurpleFlippedText.text = RoundProfile.PurpleFlipped.ToString(DigitFormat_Count);
        }
        if(BestRoundProfile!=null)
        {
            HighestScore.text = BestRoundProfile.Score.ToString(DigitFormat_Score);
        }
    }

    public static void SaveRoundToFile(Profile newProfile)
    {
        string jsonString = JsonUtility.ToJson(newProfile);
        File.Delete(Application.persistentDataPath + Round_FileName);
        File.WriteAllText(Application.persistentDataPath + Round_FileName, jsonString);
    }

    public static void SaveRecordToFile(Profile newProfile)
    {
        string jsonString = JsonUtility.ToJson(newProfile);
        File.Delete(Application.persistentDataPath + RecordRound_FileName);
        File.WriteAllText(Application.persistentDataPath + RecordRound_FileName, jsonString);
    }

}
