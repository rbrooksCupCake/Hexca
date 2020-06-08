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
    public const string DigitFormat_Time = "D2";


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

    public TextMeshProUGUI BlueTCreatedText = null;
    public TextMeshProUGUI GreenLTCreatedText = null;
    public TextMeshProUGUI RedTCreatedText = null;
    public TextMeshProUGUI OrangeTCreatedText = null;
    public TextMeshProUGUI PurpleTCreatedText = null;

    public TextMeshProUGUI HighestScore = null;

    public TextMeshProUGUI RoundHours = null;
    public TextMeshProUGUI RoundMinutes = null;
    public TextMeshProUGUI RoundSeconds = null;

    public TextMeshProUGUI RecordSecond = null;
    public TextMeshProUGUI RecordMinute = null;
    public TextMeshProUGUI RecordHour = null;


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

            BlueCubeCreated.text = RoundProfile.BlueCubesMade.ToString(DigitFormat_Count);
            GreenCubeCreated.text = RoundProfile.GreenCubesMade.ToString(DigitFormat_Count);
            RedCubeCreated.text = RoundProfile.RedCubesMade.ToString(DigitFormat_Count);
            OrangeCubeCreated.text = RoundProfile.OrangeCubesMade.ToString(DigitFormat_Count);
            PurpleCubeCreated.text = RoundProfile.PurpleCubesMade.ToString(DigitFormat_Count);

            BlueLCreatedText.text = RoundProfile.L_BlueMade.ToString(DigitFormat_Count);
            GreenLCreatedText.text = RoundProfile.L_GreenMade.ToString(DigitFormat_Count);
            RedLCreatedText.text = RoundProfile.L_RedMade.ToString(DigitFormat_Count);
            OrangeLCreatedText.text = RoundProfile.L_OrangeMade.ToString(DigitFormat_Count);
            PurpleLCreatedText.text = RoundProfile.L_PurpleMade.ToString(DigitFormat_Count);


            BlueLineCreatedText.text = RoundProfile.BlueLinesMade.ToString(DigitFormat_Count);
            GreenLineCreatedText.text = RoundProfile.GreenLinesMade.ToString(DigitFormat_Count);
            RedLineCreatedText.text = RoundProfile.RedLinesMade.ToString(DigitFormat_Count);
            OrangeLineCreatedText.text = RoundProfile.OrangeLinesMade.ToString(DigitFormat_Count);
            PurpleLineCreatedText.text = RoundProfile.PurpleLinesMade.ToString(DigitFormat_Count);

            BlueTCreatedText.text = RoundProfile.T_BlueMade.ToString(DigitFormat_Count);
            GreenLTCreatedText.text = RoundProfile.T_GreenMade.ToString(DigitFormat_Count);
            RedTCreatedText.text = RoundProfile.T_RedMade.ToString(DigitFormat_Count);
            OrangeTCreatedText.text = RoundProfile.T_OrangeMade.ToString(DigitFormat_Count);
            PurpleTCreatedText.text = RoundProfile.T_PurpleMade.ToString(DigitFormat_Count);

            RoundHours.text = System.Convert.ToInt32(RoundProfile.TimeSeconds).ToString(DigitFormat_Time);
            RoundMinutes.text = System.Convert.ToInt32(RoundProfile.TimeMinutes).ToString(DigitFormat_Time);
            RoundSeconds.text = System.Convert.ToInt32(RoundProfile.TimeHours).ToString(DigitFormat_Time);
        }
        if (BestRoundProfile!=null)
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
