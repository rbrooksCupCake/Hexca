using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialHandler : MonoBehaviour
{

    public bool tutorialActive;
    public bool isTutorialDone;

    public GameObject GameStateObj;
    GameObject nullNut;
    public List<GameObject> nutPool;

    public GameObject LevelCompleteParticle;

    public GameObject SpawnPoint_One;
    public GameObject SpawnPoint_Two;
    public GameObject SpawnPoint_Three;
    public GameObject SpawnPoint_Four;
    public GameObject SpawnPoint_Five;

    public GameObject TutorialTextObj;
    public string tutorialText;
    public GameObject tapAnimObject;
    GameObject pivotNut;
    GameObject tempTapAnim;

    public static int phase = 0;
    public static bool isLevelCompleteRunning;


    public void Start()
    {
        //GameStateObj = GameObject.Find("Main Camera");
        nullNut = GameStateObj.GetComponent<GameState>().nullNut;
        tempTapAnim = Instantiate(tapAnimObject, Vector3.zero, Quaternion.identity);
        tempTapAnim.SetActive(false);
        nutPool = GameState.nutPool;
        GameState.canTap = false;
    }

    IEnumerator levelComplete()
    {
        isLevelCompleteRunning = true;
        GameObject LevelComplete = Instantiate(LevelCompleteParticle, new Vector3(3, 7, -2), Quaternion.identity);
        tempTapAnim.SetActive(false);
        yield return new WaitForSeconds(1.25f);
        GameState.canTap = false;
        phase = 0;
        Destroy(LevelComplete);
        clearBoard();

        isLevelCompleteRunning = false;
    }

    void checkTutorialPhases()
    {
        if (GameState.playerScore > 10 && GameState.current_TutorialPhase == GameState.TutorialPhases.Matching_Two)
        {
            tempTapAnim.SetActive(false);
            Debug.Log("Phase One Completed!");
            tutorialText = "";
            GameState.current_TutorialPhase = GameState.TutorialPhases.Shifting;

            StartCoroutine("levelComplete");
        }

        if (NUT_Behavior.boxTwoStored && GameState.current_TutorialPhase == GameState.TutorialPhases.Shifting && phase == 3)
        {
            ++phase;
        }
        if (!NUT_Behavior.boxTwoStored && GameState.current_TutorialPhase == GameState.TutorialPhases.Shifting && phase == 4)
        {
            ++phase;
        }

        if (GameState.playerScore > 30 && GameState.current_TutorialPhase == GameState.TutorialPhases.Shifting && phase == 5)
        {
            tempTapAnim.SetActive(false);
            Debug.Log("Phase Three Completed!");
            tutorialText = "";
            GameState.current_TutorialPhase = GameState.TutorialPhases.Box;

            StartCoroutine("levelComplete");
        }


        if (GameState.playerScore > 30 && GameState.current_TutorialPhase == GameState.TutorialPhases.Box && phase == 1)
        {
            tempTapAnim.SetActive(false);
            Debug.Log("Phase Three Completed!");
            tutorialText = "";
            GameState.current_TutorialPhase = GameState.TutorialPhases.Box;
        }
    }

    private void Update()
    {
        if (GameState.tutorialActive)
        {
            checkTutorialPhases();
            if (TutorialTextObj.GetComponent<Text>().text != tutorialText)
            {
                TutorialTextObj.GetComponent<Text>().text = tutorialText;
            }
        }





    }

    private void clearBoard()
    {
        GameObject[] Nuts = GameState.nutActivePool.ToArray();
        foreach (GameObject Nut in Nuts)
        {
            Nut.GetComponent<NUT_Behavior>().removesphere();
        }
    }

    public void Tutorial_ShiftingPhase()
    {


        if (GameState.nutPool.Count >= 5)
        {

            if (SpawnPoint_One != null)
            {
                if (GameState.current_TutorialPhase == GameState.TutorialPhases.Shifting)
                {



                    if (phase == 0)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        pivotNut = nutPool[0];
                        nutPool.RemoveAt(0);
                        GameState.nutActivePool.Add(pivotNut);



                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }
                    if (phase == 1)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);
                    }
                    if (phase == 2)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        nutPool.RemoveAt(0);


                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }


                    if (phase == 3)
                    {
                        GameState.canTap = true;
                        Debug.Log("Can tap now.");
                        Vector3 tapPosition = new Vector3(pivotNut.transform.position.x, pivotNut.transform.position.y, -1);
                        tempTapAnim.transform.position = tapPosition;
                        tempTapAnim.SetActive(true);
                        tutorialText = "You can store a piece that is by its self.";
                    }


                    if (phase == 4)
                    {
                        Vector3 tapPosition = new Vector3(pivotNut.transform.position.x, pivotNut.transform.position.y, -1);
                        tempTapAnim.transform.position = tapPosition;
                        tempTapAnim.SetActive(true);
                        tutorialText = "You can place a piece back by tapping it if it is stored.";
                    }

                    if (phase == 5)
                    {
                        GameState.canTap = true;
                        Debug.Log("Can tap now.");
                        Vector3 tapPosition = new Vector3(pivotNut.transform.position.x, pivotNut.transform.position.y, -1);
                        tempTapAnim.transform.position = tapPosition;
                        tempTapAnim.SetActive(true);
                        tutorialText = "Tap two or more adjacent colors to get points!";
                    }

                    if (phase < 3)
                    {
                        ++phase;
                    }


                }
            }
        }
    }

    public void Tutorial_StoringPhase()
    {
        if (GameState.nutPool.Count >= 5)
        {

            if (SpawnPoint_One != null)
            {
                if (GameState.current_TutorialPhase == GameState.TutorialPhases.Storing)
                {
                    if (GameState.nutPool[0].activeInHierarchy == true)
                    {
                        GameState.nutPool.RemoveAt(0);
                    }



                    if (phase == 0)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }
                    if (phase == 1)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        pivotNut = nutPool[0];
                        nutPool.RemoveAt(0);
                        GameState.nutActivePool.Add(pivotNut);



                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }

                    if (phase == 2)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }
                    if (phase == 3)
                    {

                        if (Mathf.Approximately(Mathf.Round(pivotNut.transform.position.x), 2f) && Mathf.Approximately(Mathf.Round(pivotNut.transform.position.y), 2f))
                        {
                            GameState.canTap = true;
                            Vector3 tapPosition = new Vector3(pivotNut.transform.position.x, pivotNut.transform.position.y, -1);
                            tempTapAnim.SetActive(true);
                            tempTapAnim.transform.position = tapPosition;
                            tutorialText = "When pieces are alone you can store them by tapping them.";
                        }

                    }
                    if (phase < 3)
                    {
                        ++phase;
                    }


                }
            }
        }
    }


    public void Tutorial_MatchingTwo()
    {


        if (GameState.nutPool.Count >= 5)
        {

            if (SpawnPoint_One != null)
            {
                if (GameState.current_TutorialPhase == GameState.TutorialPhases.Matching_Two)
                {
                    /*   if (GameState.nutPool[0].active == true)
                       {
                           GameState.nutPool.RemoveAt(0);
                       }
                       else
                       {*/


                    if (phase == 0)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);
                    }
                    if (phase == 1)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        pivotNut = nutPool[0];
                        nutPool.RemoveAt(0);


                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        nutPool.RemoveAt(0);
                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }
                    if (phase == 2)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }


                    if (phase == 3)
                    {
                        if (Mathf.Approximately(Mathf.Round(pivotNut.transform.position.x), 2f) && Mathf.Approximately(Mathf.Round(pivotNut.transform.position.y), 2f) && pivotNut.GetComponent<NUT_Behavior>().touchList.Count > 1)
                        {
                            GameState.canTap = true;
                            Vector3 tapPosition = new Vector3(pivotNut.transform.position.x, pivotNut.transform.position.y, -1);
                            tempTapAnim.SetActive(true);
                            tempTapAnim.transform.position = tapPosition;
                            tutorialText = "Tap two or more adjacent colors to get points!";

                        }
                    }
                    if (phase < 3)
                    {
                        Debug.Log(phase.ToString());
                        ++phase;
                    }


                }
            }
        }
    }



    public void Tutorial_BoxPhase()
    {


        if (GameState.nutPool.Count >= 5)
        {

            if (SpawnPoint_One != null)
            {
                if (GameState.current_TutorialPhase == GameState.TutorialPhases.Box)
                {
                    /*   if (GameState.nutPool[0].active == true)
                       {
                           GameState.nutPool.RemoveAt(0);
                       }
                       else
                       {*/


                    if (phase == 0)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);
                    }
                    if (phase == 1)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        pivotNut = nutPool[0];
                        nutPool.RemoveAt(0);


                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        nutPool.RemoveAt(0);
                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }
                    if (phase == 2)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Two.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        pivotNut = nutPool[0];
                        nutPool.RemoveAt(0);


                        nutPool[0].SetActive(true);
                        nutPool[0].transform.position = (SpawnPoint_Three.transform.position);
                        nutPool[0].GetComponent<NUT_Behavior>().myColor = NUT_Behavior.nutColor.Blue;
                        nutPool.RemoveAt(0);
                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                    }

                    if(phase==3)
                    {
                        GameObject NullNut = Instantiate(nullNut, SpawnPoint_One.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Two.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Three.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Four.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);

                        NullNut = Instantiate(nullNut, SpawnPoint_Five.transform.position, Quaternion.identity);
                        GameState.nutActivePool.Add(NullNut);
                    }
                    if (phase == 4)
                    {

                            GameState.canTap = true;
                            Vector3 tapPosition = new Vector3(pivotNut.transform.position.x, pivotNut.transform.position.y, -1);
                            tempTapAnim.SetActive(true);
                            tempTapAnim.transform.position = tapPosition;
                            tutorialText = "Making different shapes will impact the board in various ways.";

                        
                    }
                    if (phase ==5)
                    {

                        GameState.canTap = true;
                        Vector3 tapPosition = new Vector3(pivotNut.transform.position.x, pivotNut.transform.position.y, -1);
                        tempTapAnim.SetActive(false);
                        tutorialText = "Making different shapes will impact the board in various ways.";


                    }
                    if (phase <= 5)
                    {
                        Debug.Log(phase.ToString());
                        ++phase;
                    }


                }
            }
        }
    }
}