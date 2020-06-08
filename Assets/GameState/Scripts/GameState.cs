
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{

    public GameObject Nut;
    public GameObject nullNut;
    public static List<GameObject> nutPool = new List<GameObject>();
    public static List<GameObject> nutActivePool = new List<GameObject>();
    GameObject NutPoolObj;

    public GameObject SpawnPoint_One;
    public GameObject SpawnPoint_Two;
    public GameObject SpawnPoint_Three;
    public GameObject SpawnPoint_Four;
    public GameObject SpawnPoint_Five;

    public GameObject MainMenu;
    public GameObject ScoreLabel;
    public GameObject ArcadeUI;
    public GameObject OptionsUI;
    public GameObject OptionsTitle;
    public GameObject PauseUI;
    public GameObject GameOverUI;

    public bool firstStart = true;
    public bool isChanging;
    public static bool isTimeSlowed;
    public static bool isTimeCoroutineRunning;
    [Range(1f, 4.5f)]
    public float timeBetweenLoads = 4.5f;
    public float previousTimeBetweenLoads = 0;
    public const float TUTORIALTIMEBETWEENLOADS = 1.25f;


    public enum GameMode { MainMenu, Tutorial, Arcade };
    public GameMode currentMode;

    public enum TutorialPhases { Matching_Two, Storing, Line, Box, L_Shape, Shifting, Finished };
    public static TutorialPhases current_TutorialPhase;

    public float soundFxVolume;
    public float musicVolume;
    public float screenShakeAmount;
    public float popIntensity;
    public bool isColorBlindModeEnabled;

    public bool DebugModeEnabled;
    public enum DebugShapeMode { None, MatchTwo, Storing, Shifting, Box, Line, L_Block };
    public DebugShapeMode debugMode;
    public GameObject[,] boardState;
    public static int i = 0;
    public static int playerScore = 0;
    public static int shapeMultiplier = 1;
    public static int colorMultiplier = 1;
    public int colorMultiplierReadOnly = colorMultiplier;
    public static int plusMultiplier = 1;
    public static bool hasCheckedColor = false;

    public static NUT_Behavior.nutColor lastColorPopped;

    public static bool canTap;

    public static bool tutorialActive;
    public bool tutorialCompleted;

    public static float STARTING_BG_STAGELIGHT_INTENSITY = 30.0f;

    public static GameObject BG_StageLight;
    public Color stageClear;
    public Color stageBlue;
    public Color stageGreen;
    public Color stageRed;
    public Color stageOrange;
    public Color stagePurple;

    public static bool isPaused;
    public GameObject PauseMenu;

    public Material SkyboxMat;
    public GameObject blueBackgroundPop;
    public GameObject greenBackgroundPop;
    public GameObject redBackgroundPop;
    public GameObject purpleBackgroundPop;
    public GameObject orangeBackgroundPop;
    public GameObject AnnouncerTextExplosion;
    public Text AnnouncerText;
    public enum Announcements { None, Boxy, Linear, Plus_Time,  L_Piece};

    public static bool isScoreChanging;
    public static int changeInScore;
    public static int previousScore;
    public int changeInScoreReadme;

    public static int numNuts_Score = 0;
    public int numNuts_Score_Readme;
    public static bool firstPassScore = false;

    private static GameState _GameState;

    [SerializeField]
    private AudioHandler myAudioHandler = null;
    [SerializeField]
    private ScreenShake myScreenShakeHandler = null;

    public ScreenShake GetScreenShake()
    {
        return myScreenShakeHandler;
    }

    public AudioHandler GetAudioHandler()
    {
        return myAudioHandler;
    }

    private void Awake()
    {
        if(_GameState==null)
        {
            _GameState = this;
        }
    }

    public static GameState GetGameState()
    {
            return _GameState;
    }

    public void clearBoard()
    {
        GameObject[] Nuts = GameState.nutActivePool.ToArray();
        foreach (GameObject Nut in Nuts)
        {
            Nut.GetComponent<NUT_Behavior>().removesphere();
        }
    }

    public static void addScore(int addedScore)
    {
        playerScore += (addedScore * shapeMultiplier);
        --numNuts_Score;

        if (numNuts_Score == 0 && firstPassScore)
        {
            firstPassScore = false;
            isScoreChanging = false;
        }
        firstPassScore = true;
    }

    public void cmdCancelInvoke()
    {
        CancelInvoke();
    }

    public void loadNUTS(string colorCode)
    {
        string[] colors = colorCode.Split(',');
        Debug.Log(colors[0]);
        Debug.Log(colors[1]);
        Debug.Log(colors[2]);
        Debug.Log(colors[3]);
        Debug.Log(colors[4]);


        if (colors[0] == "B")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
            nutPool[0].transform.position = (SpawnPoint_One.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[0] == "G")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Green;
            nutPool[0].transform.position = (SpawnPoint_One.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[0] == "R")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Red;
            nutPool[0].transform.position = (SpawnPoint_One.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[0] == "P")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Purple;
            nutPool[0].transform.position = (SpawnPoint_One.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[0] == "O")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Orange;
            nutPool[0].transform.position = (SpawnPoint_One.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }

        if (colors[1] == "B")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
            nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[1] == "G")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Green;
            nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[1] == "R")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Red;
            nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[1] == "P")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Purple;
            nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[1] == "O")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Orange;
            nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }

        if (colors[2] == "B")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
            nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[2] == "G")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Green;
            nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[2] == "R")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Red;
            nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[2] == "P")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Purple;
            nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[2] == "O")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Orange;
            nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }

        if (colors[3] == "B")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
            nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[3] == "G")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Green;
            nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[3] == "R")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Red;
            nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[3] == "P")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Purple;
            nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[3] == "O")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Orange;
            nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }

        if (colors[4] == "B")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
            nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[4] == "G")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Green;
            nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[4] == "R")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Red;
            nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[4] == "P")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Purple;
            nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }
        else if (colors[4] == "O")
        {
            nutPool[0].SetActive(true);
            nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Orange;
            nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
            nutActivePool.Add(nutPool[0]);
            nutPool.RemoveAt(0);
        }


    }

    public void loadNUTS()
    {

        if (tutorialActive && current_TutorialPhase == TutorialPhases.Matching_Two)
        {
            GetComponent<TutorialHandler>().Tutorial_MatchingTwo();
        }
        if (tutorialActive && current_TutorialPhase == TutorialPhases.Storing)
        {
            GetComponent<TutorialHandler>().Tutorial_StoringPhase();
        }
        if (tutorialActive && current_TutorialPhase == TutorialPhases.Shifting)
        {
            GetComponent<TutorialHandler>().Tutorial_ShiftingPhase();
        }
        if (tutorialActive && current_TutorialPhase == TutorialPhases.Box)
        {
            GetComponent<TutorialHandler>().Tutorial_BoxPhase();
        }

        if (!DebugModeEnabled && !tutorialActive)
        {
            if (isChanging)
            {
                isChanging = false;
            }
            else
            {
                GameState.canTap = true;
                if (nutPool.Count >= 5)
                {
                    GetComponent<AudioHandler>().playLoadInEffect();


                    if (SpawnPoint_One != null)
                    {

                        if (nutPool[0].activeInHierarchy == true)
                        {
                            if (!nutActivePool.Contains(nutPool[0]))
                            {
                                nutActivePool.Add(nutPool[0]);
                            }
                            nutPool.RemoveAt(0);
                        }
                        else
                        {
                            nutPool[0].SetActive(true);
                            nutPool[0].GetComponent<Rigidbody>().position = (SpawnPoint_One.transform.position);
                            nutPool[0].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                            nutPool[0].GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
                            nutActivePool.Add(nutPool[0]);
                            nutPool.RemoveAt(0);

                            nutPool[0].SetActive(true);
                            nutPool[0].GetComponent<Rigidbody>().position = (SpawnPoint_Two.transform.position);
                            nutPool[0].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                            nutPool[0].GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
                            nutActivePool.Add(nutPool[0]);
                            nutPool.RemoveAt(0);

                            nutPool[0].SetActive(true);
                            nutPool[0].GetComponent<Rigidbody>().position = (SpawnPoint_Three.transform.position);
                            nutPool[0].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                            nutPool[0].GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
                            nutActivePool.Add(nutPool[0]);
                            nutPool.RemoveAt(0);

                            nutPool[0].SetActive(true);
                            nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
                            nutPool[0].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                            nutPool[0].GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
                            nutActivePool.Add(nutPool[0]);
                            nutPool.RemoveAt(0);

                            nutPool[0].SetActive(true);
                            nutPool[0].GetComponent<Rigidbody>().position = (SpawnPoint_Five.transform.position);
                            nutPool[0].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                            nutPool[0].GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
                            nutActivePool.Add(nutPool[0]);
                            nutPool.RemoveAt(0);
                            return;
                        }
                    }
                }
                else
                {
                    fillPool();
                }
            }
        }
        else if (DebugModeEnabled)
        {
            if (nutPool.Count >= 5)
            {

                if (SpawnPoint_One != null)
                {
                    if (debugMode == DebugShapeMode.Box)
                    {
                        if (nutPool[0].activeInHierarchy == true)
                        {
                            nutPool.RemoveAt(0);
                        }
                        else
                        {

                            if (i == 0)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }
                            if (i == 1)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }
                            if (i == 2)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }

                            if (i == 3)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }


                            if (i > 3)
                            {
                                i = 0;
                                DebugModeEnabled = false;
                            }
                            ++i;


                        }
                    }
                    else if (debugMode == DebugShapeMode.Line)
                    {
                        if (nutPool[0].activeInHierarchy == true)
                        {
                            nutPool.RemoveAt(0);
                        }
                        else
                        {

                            if (i == 0)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }
                            if (i == 1)
                            {
                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_One.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);
                            }
                            if (i == 2)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }

                            if (i == 3)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }
                            if (i == 4)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }


                            if (i > 4)
                            {
                                i = 0;
                                debugMode = DebugShapeMode.None;
                            }
                            ++i;
                        }
                    }
                    else if (debugMode == DebugShapeMode.L_Block)
                    {

                        if (nutPool[0].activeInHierarchy == true)
                        {
                            nutPool.RemoveAt(0);
                        }
                        else
                        {

                            if (i == 0)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }
                            if (i == 1)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);

                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);


                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Four.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);
                            }
                            if (i == 2)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);



                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);

                            }

                            if (i == 3)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);



                                nutPool[0].SetActive(true);
                                nutPool[0].transform.position = (SpawnPoint_Five.transform.position);
                                nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                nutPool.RemoveAt(0);
                            }



                            if (i == 4)
                            {
                                Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                            }

                            if (i > 4)
                            {
                                i = 0;
                                DebugModeEnabled = false;
                            }
                            ++i;

                        }
                    }
                    if (nutPool.Count >= 5)
                    {

                        if (SpawnPoint_One != null)
                        {
                            if (debugMode == DebugShapeMode.MatchTwo)
                            {
                                if (nutPool[0].activeInHierarchy == true)
                                {
                                    nutPool.RemoveAt(0);
                                }
                                else
                                {

                                    if (i == 0)
                                    {
                                        Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }
                                    if (i == 1)
                                    {
                                        Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);

                                        nutPool[0].SetActive(true);
                                        nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                        nutPool.RemoveAt(0);

                                        nutPool[0].SetActive(true);
                                        nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
                                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                        nutPool.RemoveAt(0);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }
                                    if (i == 2)
                                    {
                                        Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }


                                    if (i > 2)
                                    {

                                    }
                                    ++i;


                                }
                            }
                        }
                    }
                    if (nutPool.Count >= 5)
                    {

                        if (SpawnPoint_One != null)
                        {
                            if (debugMode == DebugShapeMode.Storing)
                            {
                                if (nutPool[0].activeInHierarchy == true)
                                {
                                    nutPool.RemoveAt(0);
                                }
                                else
                                {

                                    if (i == 0)
                                    {

                                        nutPool[0].SetActive(true);
                                        nutPool[0].transform.position = (SpawnPoint_One.transform.position);
                                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                        nutPool.RemoveAt(0);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }
                                    if (i == 1)
                                    {
                                        Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }
                                    if (i == 2)
                                    {
                                        Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }


                                    if (i > 2)
                                    {

                                    }
                                    ++i;


                                }
                            }
                        }
                    }
                    if (nutPool.Count >= 5)
                    {

                        if (SpawnPoint_One != null)
                        {
                            if (debugMode == DebugShapeMode.Shifting)
                            {
                                if (nutPool[0].activeInHierarchy == true)
                                {
                                    nutPool.RemoveAt(0);
                                }
                                else
                                {

                                    if (i == 0)
                                    {

                                        nutPool[0].SetActive(true);
                                        nutPool[0].transform.position = (SpawnPoint_One.transform.position);
                                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                        nutPool.RemoveAt(0);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }
                                    if (i == 1)
                                    {
                                        Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }
                                    if (i == 2)
                                    {
                                        nutPool[0].SetActive(true);
                                        nutPool[0].transform.position = (SpawnPoint_One.transform.position);
                                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                                        nutPool.RemoveAt(0);
                                        Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                                        Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                                    }


                                    if (i > 2)
                                    {

                                    }
                                    ++i;


                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void createPool()
    {
        NutPoolObj = new GameObject("Nut Pool");
        for (int numNuts = 0; numNuts < 60; ++numNuts)
        {
            GameObject newNut = Instantiate(Nut, new Vector3(-1000, -1000, -1000), Quaternion.identity);
            newNut.transform.SetParent(NutPoolObj.transform);
            newNut.SetActive(false);
            nutPool.Add(newNut);
        }
    }

    void fillPool()
    {
        if (NutPoolObj != null)
        {
            for (int numNuts = 0; numNuts < 60; ++numNuts)
            {
                GameObject newNut = Instantiate(Nut, new Vector3(-1000, -1000, -1000), Quaternion.identity);
                newNut.transform.SetParent(NutPoolObj.transform);
                newNut.SetActive(false);
                nutPool.Add(newNut);
            }
        }
    }


    void Start()
    {
        BG_StageLight = GameObject.Find("BG_StageLight");
        lastColorPopped = NUT_Behavior.nutColor.Null;
        SkyboxMat = RenderSettings.skybox;
        currentMode = GameMode.MainMenu;
        tutorialActive = true;
       //GameObject SpawnPoint_One = GameObject.Find("SpawnPoint_One");
       //GameObject SpawnPoint_Two = GameObject.Find("SpawnPoint_Two");
       //GameObject SpawnPoint_Three = GameObject.Find("SpawnPoint_Three");
       //GameObject SpawnPoint_Four = GameObject.Find("SpawnPoint_Four");
       //GameObject SpawnPoint_Five = GameObject.Find("SpawnPoint_Five");

        createPool();

        previousTimeBetweenLoads = timeBetweenLoads;
        Application.targetFrameRate = 60;


    }


    public void StartGame()
    {
        MainMenu.SetActive(false);
        ArcadeUI.SetActive(true);
        playerScore = 0;
        currentMode = GameMode.Arcade;
        Time.timeScale = 1.0f;

        if (tutorialActive)
        {
            InvokeRepeating("loadNUTS", 0, TUTORIALTIMEBETWEENLOADS);

        }
        else if (!tutorialActive && currentMode == GameMode.Arcade)
        {
            CancelInvoke();

            canTap = true;
            InvokeRepeating("loadNUTS", 0, timeBetweenLoads);

        }

    }

    public IEnumerator PlusPowerUp()
    {
        if (!isTimeCoroutineRunning)
        {
            isTimeCoroutineRunning = true;
            Time.timeScale = 0.5f;
            Debug.Log("Time should be slowed.");
            Debug.Log(Time.time);
            yield return new WaitForSecondsRealtime(9.0f);
            Debug.Log("Time should be normal.");
            Debug.Log(Time.time);
            Time.timeScale = 1.0f;
            isTimeSlowed = false;
            isTimeCoroutineRunning = false;
        }
    }

    public void openOptions()
    {
        OptionsUI.SetActive(true);
        OptionsTitle.SetActive(true);
    }

    public void PauseGame()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        colorMultiplierReadOnly = colorMultiplier;
        numNuts_Score_Readme = numNuts_Score;

        if (previousScore != playerScore && !isScoreChanging) 
        {

            changeInScore = (playerScore - previousScore);
            changeInScoreReadme = changeInScore;
            previousScore = playerScore;

        }


        if (isTimeSlowed)
        {
            if(!isTimeCoroutineRunning)
            {
                StartCoroutine(PlusPowerUp());
            }
        }

        if (!Mathf.Approximately(timeBetweenLoads,previousTimeBetweenLoads) && currentMode == GameMode.Arcade && !isChanging)
        {
            isChanging = true;
            CancelInvoke();
            previousTimeBetweenLoads = timeBetweenLoads;
            if (isChanging)
            {
                InvokeRepeating("loadNUTS", 0, timeBetweenLoads);
            }

        }

        tutorialActive = GetComponent<TutorialHandler>().tutorialActive;

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!DebugModeEnabled)
            {
                Debug.Log("Debug Mode Enabled");
                timeBetweenLoads = 1.0f;
                DebugModeEnabled = !DebugModeEnabled;
            }

        }

        if (DebugModeEnabled)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                CancelInvoke();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {

                Debug.Log("Debug Mode Disabled");
                DebugModeEnabled = !DebugModeEnabled;
            }
        }
    }
}
