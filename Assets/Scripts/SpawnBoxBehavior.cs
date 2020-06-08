using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoxBehavior : MonoBehaviour
{
    public Transform MyTransform;
    public MaterialVariable spawnBoxMaterial;
    public GameEvent PickColorEvent;
    public GameEvent SpawnEvent;
    public GameEvent DropEvent;
    public GameEvent RestartEvent;

    public Component[] MyListeners;

    public List<HexColor> hexColors;

    public List<GameObject> hexes;

    public static List<GameObject> hexPool = new List<GameObject>();
    public List<GameObject> hexPoolViewer;
    public bool hasStartedCoroutine;
    public static bool hasPoolBeenFilled;


    public bool hasPickedColor;
    public bool hasSpawned;

    public static bool isCoroutineFinished;

    public static bool hasDropped;
    public static bool HasSpawned;

    public FloatVariable TimeBetweenLoads;

    public IntReference numGems;

    public int mySpawnBox_Int;

    public static List<GameObject> activeHexes = new List<GameObject>();

    public AudioSource Load_SFX;
    public static bool hasPlayed_LoadSFX;
    public AudioSource Drop_SFX;
    public static bool hasPlayed_DropSFX;

    public void DisableAllListeners()
    {
        MyListeners = GetComponents(typeof(GameEventListener));
        foreach(GameEventListener listener in MyListeners)
        {
            listener.enabled = false;
        }
    }

    public void EnableAllListeners()
    {
        MyListeners = GetComponents(typeof(GameEventListener));
        foreach (GameEventListener listener in MyListeners)
        {
            listener.enabled = true;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
    }

    public void ResetCamera()
    {

    }
 
    public void SetColor(int num)
    {
        HexColor ChosenColor = hexColors[num];
        spawnBoxMaterial.ChangeColor(ChosenColor.hexColor);
    }



    private void Start()
    {
        if(!hasPoolBeenFilled)
        {
            fillHexPool();
        }
        ResetToMenu();
        TutorialSpawnHandler.CanTap = true;
    }

    public void ResetToMenu()
    {
        GetComponent<Transform>().GetChild(0).gameObject.SetActive(true);
        if (gameObject.name == "SpawnBox_1")
        {
            HexColor ChosenColor = hexColors[0];
            spawnBoxMaterial.ChangeColor(ChosenColor.hexColor);
        }
        if (gameObject.name == "SpawnBox_2")
        {
            HexColor ChosenColor = hexColors[1];
            spawnBoxMaterial.ChangeColor(ChosenColor.hexColor);
        }
        if (gameObject.name == "SpawnBox_3")
        {
            HexColor ChosenColor = hexColors[2];
            spawnBoxMaterial.ChangeColor(ChosenColor.hexColor);
        }
        if (gameObject.name == "SpawnBox_4")
        {
            HexColor ChosenColor = hexColors[4];
            spawnBoxMaterial.ChangeColor(ChosenColor.hexColor);
        }
        if (gameObject.name == "SpawnBox_5")
        {
            HexColor ChosenColor = hexColors[3];
            spawnBoxMaterial.ChangeColor(ChosenColor.hexColor);
        }
    }

    private void fillHexPool()
    {
        for(int i=0; i<numGems;++i)
        {
            GameObject newHex = Instantiate(hexes[0], GetComponent<Transform>().position, Quaternion.identity);
            if (newHex != null)
            {
                newHex.SetActive(false);
                hexPool.Add(newHex);
            }
        }
        hexPoolViewer = hexPool;
        hasPoolBeenFilled = true;
    }

    private void OnEnable()
    {
        hasSpawned = false;
        isCoroutineFinished = false;
        hasPickedColor = false;
        hasDropped = false;
        Time.timeScale = 1.0f;
    }

    private void OnDisable()
    {
        spawnBoxMaterial.ResetColor();
    }

    public void Restart()
    {
        hasPickedColor = false;
        hasStartedCoroutine = false;
        hasDropped = false;
        hasSpawned = false;
        HasSpawned = false;
        hasPlayed_DropSFX = false;
        hasPlayed_LoadSFX = false;
        StopAllCoroutines();
        PickColorEvent.Raise();
    }

    public void RestartWithoutColor()
    {
        hasPickedColor = false;
        hasStartedCoroutine = false;
        hasDropped = false;
        hasSpawned = false;
        HasSpawned = false;
        hasPlayed_DropSFX = false;
        hasPlayed_LoadSFX = false;
    }

    public IEnumerator PickColorDelay()
    {
        mySpawnBox_Int = Random.Range(0, hexColors.Count-1);
        HexColor ChosenColor = hexColors[mySpawnBox_Int];
        spawnBoxMaterial.ChangeColor(ChosenColor.hexColor);
        yield return new WaitForSeconds(1.0f);
        if(!HasSpawned)
        {
            SpawnEvent.Raise();
            HasSpawned = true;
        }
    }

    public void PickColor()
    {
        if(!hasPickedColor)
        {
            StartCoroutine(PickColorDelay());
            hasPickedColor = true;
        }
    }

    public void SetColor(char color)
    {
        if(color=='B')
        {
            mySpawnBox_Int = 0;
        }
        if (color == 'G')
        {
            mySpawnBox_Int = 1;
        }
        if (color == 'R')
        {
            mySpawnBox_Int = 2;
        }
        if (color == 'O')
        {
            mySpawnBox_Int = 3;
        }
        if (color == 'P')
        {
            mySpawnBox_Int = 4;
        }
    }


    public void Spawn(int num)
    {
        if(num>5|| num<-1)
        {
            Debug.LogError("Number is not valid.");
        }
        mySpawnBox_Int = num;
        SpawnWithPause();
    }

    public void ClearBoard()
    {
        if (hexPool.Count > 0)
        {
            for (int i = hexPool.Count - 1; i >= 0; --i)
            {
                if (hexPool[i].activeInHierarchy)
                {
                    hexPool[i].GetComponent<HexBehavior>().Reset();
                }
            }
        }
        StopAllCoroutines();
    }

    public void ResetColorToMenu()
    {
        if (gameObject.name == "SpawnBox_1")
        {
            SetColor(0);
        }
        if (gameObject.name == "SpawnBox_2")
        {
            SetColor(1);
        }
        if (gameObject.name == "SpawnBox_3")
        {
            SetColor(2);
        }
        if (gameObject.name == "SpawnBox_4")
        {
            SetColor(4);
        }
        if (gameObject.name == "SpawnBox_5")
        {
            SetColor(3);
        }
    }

    public void Spawn()
    {
        if(!hasStartedCoroutine)
        {
            hasDropped = false;
            
            StartCoroutine(SpawnDelay());
            hasStartedCoroutine = true;
        }
    }

    public void SpawnWithPause()
    {
        hasDropped = false;

        StartCoroutine(SpawnDelayWithPause());
    }

    public IEnumerator SpawnDelay()
    {

            if (!hasPlayed_LoadSFX)
            {
                Load_SFX.Play();
                hasPlayed_LoadSFX = true;
            }

            if (mySpawnBox_Int == 0)
            {
                if (hexPool.Count > 0)
                {
                    for (int i = hexPool.Count - 1; i >= 0; --i)
                    {
                        if (hexPool[i] != null)
                        {
                            if (!hexPool[i].activeInHierarchy && !hasSpawned)
                            {
                                GetComponent<Renderer>().material.color = hexColors[0].hexColor;
                                hexPool[i].transform.position = transform.position;
                                hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[0];
                                hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingBlue;
                                hexPool[i].SetActive(true);
                                hasSpawned = true;
                            }
                        }
                    }
                }
            }
            else if (mySpawnBox_Int == 1)
            {
                if (hexPool.Count > 0)
                {
                    for (int i = hexPool.Count - 1; i >= 0; --i)
                    {
                        if (hexPool[i] != null)
                        {
                            if (!hexPool[i].activeInHierarchy && !hasSpawned)
                            {
                                GetComponent<Renderer>().material.color = hexColors[1].hexColor;
                                hexPool[i].transform.position = transform.position;
                                hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[1];
                                hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingGreen;
                                hexPool[i].SetActive(true);
                                hasSpawned = true;
                            }
                        }
                    }
                }
            }
            else if (mySpawnBox_Int == 2)
            {
                if (hexPool.Count > 0)
                {
                    for (int i = hexPool.Count - 1; i >= 0; --i)
                    {
                        if (hexPool[i] != null)
                        {
                            if (!hexPool[i].activeInHierarchy && !hasSpawned)
                            {
                                GetComponent<Renderer>().material.color = hexColors[2].hexColor;
                                hexPool[i].transform.position = transform.position;
                                hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[2];
                                hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingRed;
                                hexPool[i].SetActive(true);
                                hasSpawned = true;
                            }
                        }
                    }
                }
            }
            else if (mySpawnBox_Int == 3)
            {
                if (hexPool.Count > 0)
                {
                    for (int i = hexPool.Count - 1; i >= 0; --i)
                    {
                        if (hexPool[i] != null)
                        {
                            if (!hexPool[i].activeInHierarchy && !hasSpawned)
                            {
                                GetComponent<Renderer>().material.color = hexColors[3].hexColor;
                                hexPool[i].transform.position = transform.position;
                                hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[3];
                                hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingOrange;
                                hexPool[i].SetActive(true);
                                hasSpawned = true;
                            }
                        }
                    }
                }
            }
            else if (mySpawnBox_Int == 4)
            {
                if (hexPool.Count > 0)
                {
                    for (int i = hexPool.Count - 1; i >= 0; --i)
                    {
                        if (hexPool[i] != null)
                        {
                            if (!hexPool[i].activeInHierarchy && !hasSpawned)
                            {
                                GetComponent<Renderer>().material.color = hexColors[4].hexColor;
                                hexPool[i].transform.position = transform.position;
                                hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[4];
                                hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingPurple;
                                hexPool[i].SetActive(true);
                                hasSpawned = true;
                            }
                        }
                    }
                }
            }
            else if(mySpawnBox_Int == -1)
        {
            if (hexPool.Count > 0)
            {
                for (int i = hexPool.Count - 1; i >= 0; --i)
                {
                    if (hexPool[i] != null)
                    {
                        if (!hexPool[i].activeInHierarchy && !hasSpawned)
                        {
                            GetComponent<Renderer>().material.color = hexColors[5].hexColor;
                            hexPool[i].transform.position = transform.position;
                            hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[5];
                            hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.defaultNull;
                            hexPool[i].SetActive(true);
                            hasSpawned = true;
                        }
                    }
                }
            }
        }
        if(hasSpawned && !hasDropped)
        {
            yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);

            if(!hasPlayed_DropSFX)
            {
                Drop_SFX.Play();
                hasPlayed_DropSFX = true;
            }

            if (gameObject.name == "SpawnBox_1")
            {
                DropEvent.Raise();
                RestartEvent.Raise();
            }

            hasDropped = true;
        }
    }

    public IEnumerator SpawnDelayWithPause()
    {

        if (!hasPlayed_LoadSFX)
        {
            Load_SFX.Play();
            hasPlayed_LoadSFX = true;
        }

        if (mySpawnBox_Int == 0)
        {
            if (hexPool.Count > 0)
            {
                for (int i = hexPool.Count - 1; i >= 0; --i)
                {
                    if (hexPool[i] != null)
                    {
                        if (!hexPool[i].activeInHierarchy && !hasSpawned)
                        {
                            GetComponent<Renderer>().material.color = hexColors[0].hexColor;
                            hexPool[i].transform.position = transform.position;
                            hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[0];
                            hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingBlue;
                            hexPool[i].SetActive(true);
                            hasSpawned = true;
                        }
                    }
                }
            }
        }
        else if (mySpawnBox_Int == 1)
        {
            if (hexPool.Count > 0)
            {
                for (int i = hexPool.Count - 1; i >= 0; --i)
                {
                    if (hexPool[i] != null)
                    {
                        if (!hexPool[i].activeInHierarchy && !hasSpawned)
                        {
                            GetComponent<Renderer>().material.color = hexColors[1].hexColor;
                            hexPool[i].transform.position = transform.position;
                            hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[1];
                            hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingGreen;
                            hexPool[i].SetActive(true);
                            hasSpawned = true;
                        }
                    }
                }
            }
        }
        else if (mySpawnBox_Int == 2)
        {
            if (hexPool.Count > 0)
            {
                for (int i = hexPool.Count - 1; i >= 0; --i)
                {
                    if (hexPool[i] != null)
                    {
                        if (!hexPool[i].activeInHierarchy && !hasSpawned)
                        {
                            GetComponent<Renderer>().material.color = hexColors[2].hexColor;
                            hexPool[i].transform.position = transform.position;
                            hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[2];
                            hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingRed;
                            hexPool[i].SetActive(true);
                            hasSpawned = true;
                        }
                    }
                }
            }
        }
        else if (mySpawnBox_Int == 3)
        {
            if (hexPool.Count > 0)
            {
                for (int i = hexPool.Count - 1; i >= 0; --i)
                {
                    if (hexPool[i] != null)
                    {
                        if (!hexPool[i].activeInHierarchy && !hasSpawned)
                        {
                            GetComponent<Renderer>().material.color = hexColors[3].hexColor;
                            hexPool[i].transform.position = transform.position;
                            hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[3];
                            hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingOrange;
                            hexPool[i].SetActive(true);
                            hasSpawned = true;
                        }
                    }
                }
            }
        }
        else if (mySpawnBox_Int == 4)
        {
            if (hexPool.Count > 0)
            {
                for (int i = hexPool.Count - 1; i >= 0; --i)
                {
                    if (hexPool[i] != null)
                    {
                        if (!hexPool[i].activeInHierarchy && !hasSpawned)
                        {
                            GetComponent<Renderer>().material.color = hexColors[4].hexColor;
                            hexPool[i].transform.position = transform.position;
                            hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[4];
                            hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.loadingPurple;
                            hexPool[i].SetActive(true);
                            hasSpawned = true;
                        }
                    }
                }
            }
        }
        else if (mySpawnBox_Int == -1)
        {
            if (hexPool.Count > 0)
            {
                for (int i = hexPool.Count - 1; i >= 0; --i)
                {
                    if (hexPool[i] != null)
                    {
                        if (!hexPool[i].activeInHierarchy && !hasSpawned)
                        {
                            GetComponent<Renderer>().material.color = hexColors[5].hexColor;
                            hexPool[i].transform.position = transform.position;
                            hexPool[i].GetComponent<HexBehavior>().myColor = hexColors[5];
                            hexPool[i].GetComponent<Renderer>().material = hexPool[i].GetComponent<HexBehavior>().theMaterialPack.defaultNull;
                            hexPool[i].SetActive(true);
                            hasSpawned = true;
                        }
                    }
                }
            }
        }
        if (hasSpawned && !hasDropped)
        {
            yield return new WaitForSeconds(TimeBetweenLoads.RuntimeValue);

            if (!hasPlayed_DropSFX)
            {
                Drop_SFX.Play();
                hasPlayed_DropSFX = true;
            }

            if (gameObject.name == "SpawnBox_1")
            {
                DropEvent.Raise();
            }

            hasDropped = true;
        }
    }


}
