using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Profile
{
    public ulong HighestScore;

    public ulong Score;

    private TimeSpan timeSpan;

    public float RoundTime;
    public float StartTime;

    public float TimeSeconds;
    public float TimeMinutes;
    public float TimeHours;

    public const string BlueColorName = "Blue";
    public const string GreenColorName = "Green";
    public const string RedColorName = "Red";
    public const string OrangeColorName = "Orange";
    public const string PurpleColorName = "Purple";

    public int BluePopped;
    public int GreenPopped;
    public int RedPopped;
    public int OrangePopped;
    public int PurplePopped;

    public int BlueFlipped;
    public int GreenFlipped;
    public int RedFlipped;
    public int OrangeFlipped;
    public int PurpleFlipped;

    public int BlueCubesMade;
    public int GreenCubesMade;
    public int RedCubesMade;
    public int PurpleCubesMade;
    public int OrangeCubesMade;

    public int BlueLinesMade;
    public int GreenLinesMade;
    public int RedLinesMade;
    public int OrangeLinesMade;
    public int PurpleLinesMade;

    public int L_BlueMade;
    public int L_GreenMade;
    public int L_RedMade;
    public int L_OrangeMade;
    public int L_PurpleMade;

    public int T_BlueMade;
    public int T_GreenMade;
    public int T_RedMade;
    public int T_OrangeMade;
    public int T_PurpleMade;

    public Profile()
    {
        Score = 0;

        BluePopped = 0;
        GreenPopped = 0;
        RedPopped = 0;
        OrangePopped = 0;
        PurplePopped = 0;
    }

    public void SetStartTime()
    {
        StartTime = Time.time;
    }

    public void SetRoundTime()
    {
        RoundTime = Time.time - StartTime;
        timeSpan = TimeSpan.FromSeconds(RoundTime);
        TimeSeconds = timeSpan.Seconds;
        TimeMinutes = timeSpan.Minutes;
        TimeHours = timeSpan.Hours;
    }

    public void SetScore(ulong newScore)
    {
        Score = newScore;
    }

    public void AddToBlueHexed()
    {
        ++BluePopped;
    }

    public void AddToGreenHexed()
    {
        ++GreenPopped;
    }

    public void AddToRedHexed()
    {
        ++RedPopped;
    }

    public void AddToOrangeHexed()
    {
        ++OrangePopped;
    }

    public void AddToPurpleHexed()
    {
        ++PurplePopped;
    }

    public void AddToBlueFlipped()
    {
        ++BlueFlipped;
    }

    public void AddToGreenFlipped()
    {
        ++GreenFlipped;
    }

    public void AddToRedFlipped()
    {
        ++RedFlipped;
    }

    public void AddToOrangeFlipped()
    {
        ++OrangeFlipped;
    }

    public void AddToPurpleFlipped()
    {
        ++PurpleFlipped;
    }
    
    public void AddToFlipped(string colorName)
    {
        switch (colorName)
        {
            case BlueColorName:
                ++BlueFlipped;
                break;
            case GreenColorName:
                ++GreenFlipped;
                break;
            case RedColorName:
                ++RedFlipped;
                break;
            case OrangeColorName:
                ++OrangeFlipped;
                break;
            case PurpleColorName:
                ++PurpleFlipped;
                break;
        }
    }

    public void AddToCubes(string colorName)
    {
        switch (colorName)
        {
            case BlueColorName:
                ++BlueCubesMade;
                break;
            case GreenColorName:
                ++GreenCubesMade;
                break;
            case RedColorName:
                ++RedCubesMade;
                break;
            case OrangeColorName:
                ++OrangeCubesMade;
                break;
            case PurpleColorName:
                ++PurpleCubesMade;
                break;
        }
    }

    public void AddToLines(string colorName)
    {
        switch (colorName)
        {
            case BlueColorName:
                ++BlueLinesMade;
                break;
            case GreenColorName:
                ++GreenLinesMade;
                break;
            case RedColorName:
                ++RedLinesMade;
                break;
            case OrangeColorName:
                ++OrangeLinesMade;
                break;
            case PurpleColorName:
                ++PurpleLinesMade;
                break;
        }
    }

    public void AddToLs(string colorName)
    {
        switch (colorName)
        {
            case BlueColorName:
                ++L_BlueMade;
                break;
            case GreenColorName:
                ++L_GreenMade;
                break;
            case RedColorName:
                ++L_RedMade;
                break;
            case OrangeColorName:
                ++L_OrangeMade;
                break;
            case PurpleColorName:
                ++L_PurpleMade;
                break;
        }
    }

    public void AddToTs(string colorName)
    {
        switch (colorName)
        {
            case BlueColorName:
                ++T_BlueMade;
                break;
            case GreenColorName:
                ++T_GreenMade;
                break;
            case RedColorName:
                ++T_RedMade;
                break;
            case OrangeColorName:
                ++T_OrangeMade;
                break;
            case PurpleColorName:
                ++T_PurpleMade;
                break;
        }
    }
    
    public void AddRound(Profile newRound)
    {
        if(newRound.Score > HighestScore)
        {
            HighestScore = newRound.Score;
        }

        BluePopped += newRound.BluePopped;
        GreenPopped += newRound.GreenPopped;
        RedPopped += newRound.RedPopped;
        OrangePopped += newRound.OrangePopped;
        PurplePopped += newRound.PurplePopped;
    }
}
