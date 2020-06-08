using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HexBehavior : MonoBehaviour
{
    public static List<GameObject> Explosions;
    public bool hasChecked;
    public MaterialVariable hexMaterial;
    public HexColor myColor;

    public List<GameObject> touchList;

    [SerializeField]
    private Rigidbody myRigidBody = null;

    [SerializeField]
    private Transform myTransform = null;

    [SerializeField]
    private Renderer myRenderer = null;

    public int Spawn_YPos = 9;

    public ulong NormalMultiplier = 1;
    public ulong BoxMultiplier = 5;
    public ulong LMultiplier = 10;
    public ulong LineMultiplier = 20;
    public ulong TMultiplier = 50;

    private bool isStored;

    public static bool boxOneStored;
    private static bool boxTwoStored;
    private static bool boxThreeStored;
    private static bool boxFourStored;
    private static bool boxFiveStored;

    public Light myLight;

    public MaterialPack theMaterialPack;
    public ScoringSystem theScoringSystem;

    public enum Shape_Orientation { Norm_Zero, Norm_Ninty, Norm_OneEighty, Norm_TwoSeventy, Flip_Zero, Flip_Ninty, Flip_OneEighty, Flip_TwoSeventy };
    public Shape_Orientation PowerUp_Orientation;

    public AudioSource Pop_SFX = null;

    public AudioSource Explode_SFX = null;

    public AudioSource Store_SFX = null;

    public AudioSource No_Store_SFX = null;

    public AudioSource Swap_SFX = null;

    public AudioSource BG_Music = null;

    public bool isAccountedForDanger;
    public static int numAtDanger;

    public GameObject ScoreParticle = null;

    public GameObject textDisplay = null;

    public FlipBehavior MyFlipBehavior;

    public const string Blue_ColorName = "Blue";
    public const string Green_ColorName = "Green";
    public const string Red_ColorName = "Red";
    public const string Orange_ColorName = "Orange";
    public const string Purple_ColorName = "Purple";
    public const string Null_ColorName = "Null";

    public const string Blue_HexLabel = "Blue Hex";
    public const string Green_HexLabel = "Green Hex";
    public const string Red_HexLabel = "Red Hex";
    public const string Orange_HexLabel = "Orange Hex";
    public const string Purple_HexLabel = "Purple Hex";

    public const string MainCameraLabel = "Main Camera";

    public const float DefaultTime = 1.0f;
    public const float DestroyTime = 0.5f;
    public const float FlipTime = 0.4f;
    public const float NonFlipTime = 0.2f;

    public const ulong DefaultScore = 10;

    public Vector3 SpawnColumnOne_Pos = new Vector3(1, 8, -1);
    public const int Column_One = 1;
    public Vector3 SpawnColumnTwo_Pos = new Vector3(2, 8, -1);
    public const int Column_Two = 2;
    public Vector3 SpawnColumnThree_Pos = new Vector3(3, 8, -1);
    public const int Column_Three = 3;
    public Vector3 SpawnColumnFour_Pos = new Vector3(4, 8, -1);
    public const int Column_Four = 4;
    public Vector3 SpawnColumnFive_Pos = new Vector3(5, 8, -1);
    public const int Column_Five = 5;

    public const int MinimumTouchCount = 2;


    private void Start()
    {
        textDisplay = GameObject.Find("ShapeText");
    }

    private void Awake()
    {
        Explosions = new List<GameObject>();
    }

    public static void ResetStoringBoxes()
    {
        boxOneStored = false;
        boxTwoStored = false;
        boxThreeStored = false;
        boxFourStored = false;
        boxFiveStored = false;
    }

    void Shatter()
    {
        ScreenShake.Shake();
        if (myColor.name == Blue_ColorName)
        {
            myRenderer.material = theMaterialPack.shatterBlue;
        }
        else if (myColor.name == Green_ColorName)
        {
            myRenderer.material = theMaterialPack.shatterGreen;
        }
        else if (myColor.name == Red_ColorName)
        {
            myRenderer.material = theMaterialPack.shatterRed;
        }
        else if (myColor.name == Orange_ColorName)
        {
            myRenderer.material = theMaterialPack.shatterOrange;
        }
        else if (myColor.name == Purple_ColorName)
        {
            myRenderer.material = theMaterialPack.shatterPurple;
        }
    }

    public void Reset()
    {
        if (myColor.name == Blue_ColorName)
        {
            myRenderer.material = theMaterialPack.loadingBlue;
        }
        else if (myColor.name == Green_ColorName)
        {
            myRenderer.material = theMaterialPack.loadingGreen;
        }
        else if (myColor.name == Red_ColorName)
        {
            myRenderer.material = theMaterialPack.loadingRed;
        }
        else if (myColor.name == Orange_ColorName)
        {
            myRenderer.material = theMaterialPack.loadingOrange;
        }
        else if (myColor.name == Purple_ColorName)
        {
            myRenderer.material = theMaterialPack.loadingPurple;
        }
        myTransform.rotation = Quaternion.Euler(Vector3.zero);
        isStored = false;
        myRigidBody.useGravity = true;
        myRigidBody.isKinematic = true;
        isAccountedForDanger = false;
        SpawnBoxBehavior.activeHexes.Remove(gameObject);
        myRigidBody.WakeUp();
        gameObject.SetActive(false);
    }




    public void ClearExplosions()
    {
        foreach (var explosion in Explosions)
        {
            Destroy(explosion);
        }
    }



    void SpawnExplosion()
    {
        if (myColor.name == Blue_ColorName)
        {
            GameObject Explosion = Instantiate(theMaterialPack.BlueExplosion, transform.position, Quaternion.identity);
            Explosions.Add(Explosion);
            Destroy(Explosion, DestroyTime);
        }
        else if (myColor.name == Green_ColorName)
        {
            GameObject Explosion = Instantiate(theMaterialPack.GreenExplosion, transform.position, Quaternion.identity);
            Explosions.Add(Explosion);
            Destroy(Explosion, DestroyTime);
        }
        else if (myColor.name == Red_ColorName)
        {
            GameObject Explosion = Instantiate(theMaterialPack.RedExplosion, transform.position, Quaternion.identity);
            Explosions.Add(Explosion);
            Destroy(Explosion, DestroyTime);
        }
        else if (myColor.name == Orange_ColorName)
        {
            GameObject Explosion = Instantiate(theMaterialPack.OrangeExplosion, transform.position, Quaternion.identity);
            Explosions.Add(Explosion);
            Destroy(Explosion, DestroyTime);
        }
        else if (myColor.name == Purple_ColorName)
        {
            GameObject Explosion = Instantiate(theMaterialPack.PurpleExplosion, transform.position, Quaternion.identity);
            Explosions.Add(Explosion);
            Destroy(Explosion, DestroyTime);
        }
    }

    IEnumerator Explode()
    {
        if (GetComponent<FlipBehavior>().IsFlipping)
        {
            yield return new WaitForSeconds(FlipTime);
        }
        else
        {
            yield return new WaitForSeconds(NonFlipTime);
        }
        foreach (GameObject Hex in touchList.ToArray())
        {
            if (Hex != null)
            {
                if (Hex != gameObject)
                {
                    if (!Mathf.Approximately(Hex.transform.position.y, Spawn_YPos))
                    {
                        if (Hex.activeInHierarchy == true)
                        {
                            if (Hex.GetComponent<HexBehavior>().myColor == myColor)
                            {
                                if (Hex.activeInHierarchy)
                                {
                                    if (!TutorialSpawnHandler.TutorialActive)
                                    {
                                        if (Hex.GetComponent<HexBehavior>().myColor.colorName == Blue_ColorName)
                                        {
                                            ScoringSystem.CurrentProfile.AddToBlueHexed();
                                        }
                                        if (Hex.GetComponent<HexBehavior>().myColor.colorName == Green_ColorName)
                                        {
                                            ScoringSystem.CurrentProfile.AddToGreenHexed();
                                        }
                                        if (Hex.GetComponent<HexBehavior>().myColor.colorName == Red_ColorName)
                                        {
                                            ScoringSystem.CurrentProfile.AddToRedHexed();
                                        }
                                        if (Hex.GetComponent<HexBehavior>().myColor.colorName == Orange_ColorName)
                                        {
                                            ScoringSystem.CurrentProfile.AddToOrangeHexed();
                                        }
                                        if (Hex.GetComponent<HexBehavior>().myColor.colorName == Purple_ColorName)
                                        {
                                            ScoringSystem.CurrentProfile.AddToPurpleHexed();
                                        }
                                    }
                                    Hex.GetComponent<HexBehavior>().StartCoroutine(Hex.GetComponent<HexBehavior>().Explode());
                                }
                            }
                        }
                    }
                }

            }
        }
        if (isAccountedForDanger)
        {
            isAccountedForDanger = false;
            --numAtDanger;
        }
        GameObject ScorePartInstance = Instantiate(ScoreParticle, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
        theScoringSystem.AddScore(DefaultScore);
        theScoringSystem.UpdateScoreText();
        Shatter();
        SpawnExplosion();
        touchList.Clear();
        myRigidBody.isKinematic = true;
        Reset();
        gameObject.SetActive(false);
        Time.timeScale = DefaultTime;
    }


    bool checkBox()
    {
        if (touchList.Count == 4)
        {
            touchList = touchList.OrderBy(piece => piece.transform.position.x).ThenBy(piece => piece.transform.position.y).ToList<GameObject>();

            if (Mathf.RoundToInt(touchList[0].transform.position.x) == Mathf.RoundToInt(touchList[1].transform.position.x))
            {
                if (Mathf.RoundToInt(touchList[2].transform.position.x) == Mathf.RoundToInt(touchList[3].transform.position.x))
                {
                    if (Mathf.RoundToInt(touchList[0].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y))
                    {
                        if (Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[3].transform.position.y))
                        {
                            if (Mathf.RoundToInt(touchList[0].transform.position.y) < Mathf.RoundToInt(touchList[1].transform.position.y))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.y) < Mathf.RoundToInt(touchList[3].transform.position.y))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

        }
        return false;
    }

    bool checkTShape()
    {
        if (touchList.Count == 5)
        {
            Debug.Log("5");
            touchList = touchList.OrderBy(piece => piece.transform.position.y).ThenBy(piece => piece.transform.position.x).ToList<GameObject>();
            foreach(var Hex in touchList)
            {
                Debug.Log(Hex.name);
                Debug.Log(Hex.transform.position.x);
                Debug.Log(Hex.transform.position.y);
            }
            if (Mathf.RoundToInt(touchList[0].transform.position.x) == Mathf.RoundToInt(touchList[4].transform.position.x))
            {
                Debug.Log("4");

                if (Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y))
                {
                    Debug.Log("3");
                    if (Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[3].transform.position.y))
                    {
                        Debug.Log("2");
                        if (Mathf.RoundToInt(touchList[4].transform.position.y) > Mathf.RoundToInt(touchList[0].transform.position.y))
                        {
                            Debug.Log("1");
                            if (Mathf.RoundToInt(touchList[4].transform.position.y) > Mathf.RoundToInt(touchList[1].transform.position.y))
                            {
                                Debug.Log("0");
                                Debug.Log(Mathf.RoundToInt(touchList[2].transform.position.x));
                                Debug.Log(Mathf.RoundToInt(touchList[3].transform.position.x));
                                if (Mathf.RoundToInt(touchList[3].transform.position.x) > Mathf.RoundToInt(touchList[2].transform.position.x))
                                {
                                    Debug.Log("-1");
                                    return true;
                                }
                            }
                        }

                    }
                }
            }

        }
        return false;
    }

    private void OnEnable()
    {
        BG_Music = GameObject.Find(MainCameraLabel).GetComponent<AudioSource>();
        SpawnBoxBehavior.activeHexes.Add(gameObject);
        if (myColor.name == Blue_ColorName)
        {
            gameObject.name = Blue_HexLabel;
            myLight.color = theMaterialPack.hexBlue;
        }
        else if (myColor.name == Green_ColorName)
        {
            gameObject.name = Green_HexLabel;
            myLight.color = theMaterialPack.hexGreen;
        }
        if (myColor.name == Red_ColorName)
        {
            gameObject.name = Red_HexLabel;
            myLight.color = theMaterialPack.hexRed;
        }
        if (myColor.name == Orange_ColorName)
        {
            gameObject.name = Orange_HexLabel;
            myLight.color = theMaterialPack.hexOrange;
        }
        if (myColor.name == Purple_ColorName)
        {
            gameObject.name = Purple_HexLabel;
            myLight.color = theMaterialPack.hexPurple;
        }
    }

    public void UpdateDefaultMaterial()
    {
        if (myColor.name == "Blue")
        {
            myRenderer.sharedMaterial = theMaterialPack.defaultBlue;
        }
        else if (myColor.name == "Green")
        {
            myRenderer.sharedMaterial = theMaterialPack.defaultGreen;
        }
        if (myColor.name == "Red")
        {
            myRenderer.sharedMaterial = theMaterialPack.defaultRed;
        }
        if (myColor.name == "Orange")
        {
            myRenderer.sharedMaterial = theMaterialPack.defaultOrange;
        }
        if (myColor.name == "Purple")
        {
            myRenderer.sharedMaterial = theMaterialPack.defaultPurple;
        }
    }

    private void OnDisable()
    {
        Reset();
    }

    void AddFlipped()
    {
        if (myColor.colorName == "Blue")
        {
            if(ScoringSystem.CurrentProfile!=null)
            {
                ScoringSystem.CurrentProfile.AddToBlueFlipped();
            }
        }
        if (myColor.colorName == "Green")
        {
            if (ScoringSystem.CurrentProfile != null)
            {
                ScoringSystem.CurrentProfile.AddToGreenFlipped();
            }
        }
        if (myColor.colorName == "Red")
        {
            if (ScoringSystem.CurrentProfile != null)
            {
                ScoringSystem.CurrentProfile.AddToRedFlipped();
            }
        }
        if (myColor.colorName == "Orange")
        {
            if (ScoringSystem.CurrentProfile != null)
            {
                ScoringSystem.CurrentProfile.AddToOrangeFlipped();
            }
        }
        if (myColor.colorName == "Purple")
        {
            if (ScoringSystem.CurrentProfile != null)
            {
                ScoringSystem.CurrentProfile.AddToPurpleHexed();
            }
        }
    }

    void tPowerUp()
    {
        int bottomT_xPos = Mathf.RoundToInt(touchList[0].transform.position.x);
        int bottomT_yPos = Mathf.RoundToInt(touchList[0].transform.position.y);

        int topT_xPos = Mathf.RoundToInt(touchList[4].transform.position.x);
        int topT_yPos = Mathf.RoundToInt(touchList[4].transform.position.y);

        int leftT_xPos = Mathf.RoundToInt(touchList[3].transform.position.x);
        int leftT_yPos = Mathf.RoundToInt(touchList[3].transform.position.y);

        int rightT_xPos = Mathf.RoundToInt(touchList[2].transform.position.x);
        int rightT_yPos = Mathf.RoundToInt(touchList[2].transform.position.y);

        ScoringSystem.CurrentProfile.AddToTs(myColor.name);


        foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
        {
            if (!Hex.GetComponent<HexBehavior>().isStored)
            {
                Debug.Log(Mathf.RoundToInt(Hex.transform.position.x));
                Debug.Log(bottomT_xPos);
                if (Mathf.RoundToInt(Hex.transform.position.x) == bottomT_xPos && Mathf.RoundToInt(Hex.transform.position.y) < bottomT_yPos && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Debug.Log("X");
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }
                if (Mathf.RoundToInt(Hex.transform.position.x) == topT_xPos && Mathf.RoundToInt(Hex.transform.position.y) > topT_yPos && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }

                if (Mathf.RoundToInt(Hex.transform.position.y) == leftT_yPos && Mathf.RoundToInt(Hex.transform.position.x) < leftT_xPos && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }

                if (Mathf.RoundToInt(Hex.transform.position.y) == rightT_yPos && Mathf.RoundToInt(Hex.transform.position.x) > rightT_yPos && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }
            }
        }
    }

    void boxPowerUp()
    {
        textDisplay.transform.GetChild(0).gameObject.SetActive(true);
        textDisplay.transform.GetChild(0).GetComponent<ShapeTextBehavior>().Activate("Cubicle");

        int xPos = Mathf.RoundToInt(touchList[0].transform.position.x);
        int yPos = Mathf.RoundToInt(touchList[0].transform.position.y);

        ScoringSystem.CurrentProfile.AddToCubes(myColor.name);

        foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
        {

            if (!Hex.GetComponent<HexBehavior>().isStored)
            {
                if (Mathf.RoundToInt(Hex.transform.position.x) == xPos && Mathf.RoundToInt(Hex.transform.position.y) == yPos + 2 && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }
                if ((Mathf.RoundToInt(Hex.transform.position.x) == (xPos + 1)) && Mathf.RoundToInt(Hex.transform.position.y) == yPos + 2 && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();

                }

                if ((Mathf.RoundToInt(Hex.transform.position.x) == (xPos)) && Mathf.RoundToInt(Hex.transform.position.y) == yPos - 1 && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();

                }

                if ((Mathf.RoundToInt(Hex.transform.position.x) == (xPos + 1)) && Mathf.RoundToInt(Hex.transform.position.y) == yPos - 1 && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }

                if ((Mathf.RoundToInt(Hex.transform.position.x) == (xPos - 1)) && Mathf.RoundToInt(Hex.transform.position.y) == yPos && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }

                if ((Mathf.RoundToInt(Hex.transform.position.x) == (xPos - 1)) && Mathf.RoundToInt(Hex.transform.position.y) == yPos + 1 && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }

                if ((Mathf.RoundToInt(Hex.transform.position.x) == (xPos + 2)) && Mathf.RoundToInt(Hex.transform.position.y) == yPos && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }

                if ((Mathf.RoundToInt(Hex.transform.position.x) == (xPos + 2)) && Mathf.RoundToInt(Hex.transform.position.y) == yPos + 1 && Hex.GetComponent<HexBehavior>().myColor != gameObject.GetComponent<HexBehavior>().myColor)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }
            }
        }

    }

    bool checkLPiece()
    {
        if (touchList.Count == 4)
        {
            int i = 0;
            foreach (GameObject Nut in touchList)
            {
                ++i;
            }

            foreach (Shape_Orientation myOrientation in (Shape_Orientation[])System.Enum.GetValues(typeof(Shape_Orientation)))
            {
                if (myOrientation == Shape_Orientation.Norm_Zero)
                {
                    touchList = touchList.OrderByDescending(piece => piece.transform.position.x).ThenBy(piece => piece.transform.position.y).ToList<GameObject>();

                    if (Mathf.RoundToInt(touchList[0].transform.position.x) > Mathf.RoundToInt(touchList[1].transform.position.x))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.y) == Mathf.RoundToInt(touchList[1].transform.position.y))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.y) < Mathf.RoundToInt(touchList[2].transform.position.y))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.y) < Mathf.RoundToInt(touchList[3].transform.position.y))
                                {
                                    if ((Mathf.RoundToInt(touchList[1].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x)) && (Mathf.RoundToInt(touchList[2].transform.position.x) == Mathf.RoundToInt(touchList[3].transform.position.x)))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Norm_Zero;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (myOrientation == Shape_Orientation.Flip_Zero)
                {
                    touchList = touchList.OrderBy(piece => piece.transform.position.x).ThenBy(piece => piece.transform.position.y).ToList<GameObject>();

                    if (Mathf.RoundToInt(touchList[0].transform.position.x) < Mathf.RoundToInt(touchList[1].transform.position.x))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.y) == Mathf.RoundToInt(touchList[1].transform.position.y))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.y) < Mathf.RoundToInt(touchList[2].transform.position.y))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.y) < Mathf.RoundToInt(touchList[3].transform.position.y))
                                {
                                    if (Mathf.RoundToInt(touchList[1].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x) && Mathf.RoundToInt(touchList[2].transform.position.x) == Mathf.RoundToInt(touchList[3].transform.position.x))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Flip_Zero;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (myOrientation == Shape_Orientation.Norm_Ninty)
                {
                    touchList = touchList.OrderBy(piece => piece.transform.position.x).ThenBy(piece => piece.transform.position.y).ToList<GameObject>();

                    if (Mathf.RoundToInt(touchList[0].transform.position.y) < Mathf.RoundToInt(touchList[1].transform.position.y))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.x) == Mathf.RoundToInt(touchList[1].transform.position.x))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.x) < Mathf.RoundToInt(touchList[2].transform.position.x))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.x) < Mathf.RoundToInt(touchList[3].transform.position.x))
                                {
                                    if ((Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y)) && (Mathf.RoundToInt(touchList[2].transform.position.y) == Mathf.RoundToInt(touchList[3].transform.position.y)))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Norm_Ninty;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (myOrientation == Shape_Orientation.Flip_Ninty)
                {
                    touchList = touchList.OrderBy(piece => piece.transform.position.x).ThenByDescending(piece => piece.transform.position.y).ToList<GameObject>();


                    if (Mathf.RoundToInt(touchList[0].transform.position.y) > Mathf.RoundToInt(touchList[1].transform.position.y))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.x) == Mathf.RoundToInt(touchList[1].transform.position.x))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.x) < Mathf.RoundToInt(touchList[2].transform.position.x))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.x) < Mathf.RoundToInt(touchList[3].transform.position.x))
                                {
                                    if (Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y) && Mathf.RoundToInt(touchList[2].transform.position.y) == Mathf.RoundToInt(touchList[3].transform.position.y))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Flip_Ninty;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }


                if (myOrientation == Shape_Orientation.Norm_OneEighty)
                {
                    touchList = touchList.OrderBy(piece => piece.transform.position.x).ThenByDescending(piece => piece.transform.position.y).ToList<GameObject>();


                    if (Mathf.RoundToInt(touchList[0].transform.position.y) == Mathf.RoundToInt(touchList[1].transform.position.y))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.x) < Mathf.RoundToInt(touchList[1].transform.position.x))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.y) > Mathf.RoundToInt(touchList[2].transform.position.y))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.y) > Mathf.RoundToInt(touchList[3].transform.position.y))
                                {
                                    if ((Mathf.RoundToInt(touchList[1].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x)) && (Mathf.RoundToInt(touchList[2].transform.position.x) == Mathf.RoundToInt(touchList[3].transform.position.x)))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Norm_OneEighty;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }


                if (myOrientation == Shape_Orientation.Flip_OneEighty)
                {
                    touchList = touchList.OrderByDescending(piece => piece.transform.position.x).ThenByDescending(piece => piece.transform.position.y).ToList<GameObject>();


                    if (Mathf.RoundToInt(touchList[0].transform.position.y) == Mathf.RoundToInt(touchList[1].transform.position.y))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.x) > Mathf.RoundToInt(touchList[1].transform.position.x))
                        {

                            if (Mathf.RoundToInt(touchList[1].transform.position.y) > Mathf.RoundToInt(touchList[2].transform.position.y))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.y) > Mathf.RoundToInt(touchList[3].transform.position.y))
                                {
                                    if ((Mathf.RoundToInt(touchList[1].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x)) && (Mathf.RoundToInt(touchList[2].transform.position.x) == Mathf.RoundToInt(touchList[3].transform.position.x)))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Flip_OneEighty;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }

                if (myOrientation == Shape_Orientation.Norm_TwoSeventy)
                {
                    touchList = touchList.OrderByDescending(piece => piece.transform.position.x).ThenByDescending(piece => piece.transform.position.y).ToList<GameObject>();

                    if (Mathf.RoundToInt(touchList[0].transform.position.x) == Mathf.RoundToInt(touchList[1].transform.position.x))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.y) > Mathf.RoundToInt(touchList[1].transform.position.y))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.x) > Mathf.RoundToInt(touchList[2].transform.position.x))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.x) > Mathf.RoundToInt(touchList[3].transform.position.x))
                                {
                                    if ((Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y)) && (Mathf.RoundToInt(touchList[2].transform.position.y) == Mathf.RoundToInt(touchList[3].transform.position.y)))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Norm_TwoSeventy;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }


                if (myOrientation == Shape_Orientation.Flip_TwoSeventy)
                {
                    touchList = touchList.OrderByDescending(piece => piece.transform.position.x).ThenBy(piece => piece.transform.position.y).ToList<GameObject>();

                    if (Mathf.RoundToInt(touchList[0].transform.position.x) == Mathf.RoundToInt(touchList[1].transform.position.x))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.y) < Mathf.RoundToInt(touchList[1].transform.position.y))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.x) > Mathf.RoundToInt(touchList[2].transform.position.x))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.x) > Mathf.RoundToInt(touchList[3].transform.position.x))
                                {
                                    if ((Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y)) && (Mathf.RoundToInt(touchList[2].transform.position.y) == Mathf.RoundToInt(touchList[3].transform.position.y)))
                                    {
                                        touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation = Shape_Orientation.Flip_TwoSeventy;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            return false;
        }
        return false;
    }

    void LPowerUp()
    {
        textDisplay.transform.GetChild(0).gameObject.SetActive(true);
        textDisplay.transform.GetChild(0).GetComponent<ShapeTextBehavior>().Activate("Linear");
        int xPos = Mathf.RoundToInt(touchList[0].transform.position.x);
        int yPos = Mathf.RoundToInt(touchList[0].transform.position.y);

        ScoringSystem.CurrentProfile.AddToLs(myColor.name);

        Shape_Orientation myOrientation = touchList[0].GetComponent<HexBehavior>().PowerUp_Orientation;

        if (myOrientation == Shape_Orientation.Norm_Ninty)
        {
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) < yPos && Mathf.RoundToInt(Hex.transform.position.x) == xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }

        if (myOrientation == Shape_Orientation.Norm_Zero)
        {

            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) == yPos && Mathf.RoundToInt(Hex.transform.position.x) > xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }
        if (myOrientation == Shape_Orientation.Flip_Zero)
        {
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) == yPos && Mathf.RoundToInt(Hex.transform.position.x) < xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }

        if (myOrientation == Shape_Orientation.Flip_Ninty)
        {
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) > yPos && Mathf.RoundToInt(Hex.transform.position.x) == xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }

        if (myOrientation == Shape_Orientation.Norm_OneEighty)
        {
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) == yPos && Mathf.RoundToInt(Hex.transform.position.x) < xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }


        if (myOrientation == Shape_Orientation.Flip_OneEighty)
        {
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) == yPos && Mathf.RoundToInt(Hex.transform.position.x) > xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }

        if (myOrientation == Shape_Orientation.Norm_TwoSeventy)
        {
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) > yPos && Mathf.RoundToInt(Hex.transform.position.x) == xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }

        if (myOrientation == Shape_Orientation.Flip_TwoSeventy)
        {
            foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
            {
                if (!Hex.GetComponent<HexBehavior>().isStored)
                {
                    if (Mathf.RoundToInt(Hex.transform.position.y) < yPos && Mathf.RoundToInt(Hex.transform.position.x) == xPos)
                    {
                        Hex.GetComponent<FlipBehavior>().Flip();
                        Hex.GetComponent<HexBehavior>().myColor = touchList[0].GetComponent<HexBehavior>().myColor;
                        AddFlipped();
                        Hex.GetComponent<HexBehavior>().Shatter();
                    }
                }
            }
        }
    }

    bool checkLine()
    {
        if (touchList.Count == 5)
        {
            foreach (GameObject Nut in touchList)
            {

                if (Mathf.RoundToInt(Nut.transform.position.y) != Mathf.RoundToInt(myTransform.position.y))
                {
                    return false;
                }

            }
            return true;
        }
        return false;
    }

    void linePowerUp()
    {
        textDisplay.transform.GetChild(0).gameObject.SetActive(true);
        textDisplay.transform.GetChild(0).GetComponent<ShapeTextBehavior>().Activate("Layering");
        int xPos = Random.Range(1, 6);
        int yPos = Mathf.RoundToInt(myTransform.position.y);

        ScoringSystem.CurrentProfile.AddToLines(myColor.name);

        foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
        {
            if (!Hex.GetComponent<HexBehavior>().isStored)
            {
                if (Mathf.RoundToInt(Hex.transform.position.x) == xPos && Mathf.RoundToInt(Hex.transform.position.y) > yPos && Hex.transform.position.y < 9)
                {
                    Hex.GetComponent<FlipBehavior>().Flip();
                    Hex.GetComponent<HexBehavior>().myColor = gameObject.GetComponent<HexBehavior>().myColor;
                    AddFlipped();
                    Hex.GetComponent<HexBehavior>().Shatter();
                }
            }
        }
    }

    private void ClearHexes()
    {
        foreach (GameObject Hex in SpawnBoxBehavior.activeHexes.ToArray())
        {
            Hex.SetActive(false);
        }
    }

    private void OnApplicationQuit()
    {
        theScoringSystem.ResetScore();
    }

    private void OnMouseDown()
    {
        if (!GameOverBox_Behavior.IsGameOver && TutorialSpawnHandler.CanTap)
        {
            if (gameObject.GetComponent<HexBehavior>().myColor.colorName == Null_ColorName)
            {
                return;
            }
            if (touchList.Count >= MinimumTouchCount && !isStored)
            {
                theScoringSystem.IncreaseColorMultiplier(myColor);
                Pop_SFX.Play();
                if (checkBox())
                {
                    ScreenShake.PowerShake();
                    boxPowerUp();
                    theScoringSystem.UpdateShapeMultiplier(BoxMultiplier);
                }
                else if (checkLine())
                {
                    ScreenShake.PowerShake();
                    linePowerUp();
                    theScoringSystem.UpdateShapeMultiplier(LineMultiplier);
                }
                else if (checkLPiece())
                {
                    ScreenShake.PowerShake();
                    LPowerUp();
                    theScoringSystem.UpdateShapeMultiplier(LMultiplier);
                }
                else if (checkTShape())
                {
                    ScreenShake.PowerShake();
                    tPowerUp();
                    theScoringSystem.UpdateShapeMultiplier(TMultiplier);
                }
                else
                {
                    Time.timeScale = DefaultTime;
                    theScoringSystem.UpdateShapeMultiplier(NormalMultiplier);
                }
                StartCoroutine(Explode());
            }
            else if (!isStored)
            {
                Store();
            }
            else if (isStored)
            {
                Store_SFX.Play();
                if (myTransform.position.x == Column_One)
                {
                    isStored = false;
                    boxOneStored = false;
                    myRigidBody.isKinematic = false;
                    myRigidBody.useGravity = true;
                    myTransform.position = SpawnColumnOne_Pos;
                }
                if (myTransform.position.x == Column_Two)
                {
                    isStored = false;
                    boxTwoStored = false;
                    myRigidBody.isKinematic = false;
                    myRigidBody.useGravity = true;
                    myTransform.position = SpawnColumnTwo_Pos;
                }
                if (myTransform.position.x == Column_Three)
                {
                    isStored = false;
                    boxThreeStored = false;
                    myRigidBody.isKinematic = false;
                    myRigidBody.useGravity = true;
                    myTransform.position = SpawnColumnThree_Pos;
                }
                if (myTransform.position.x == Column_Four)
                {
                    isStored = false;
                    boxFourStored = false;
                    myRigidBody.isKinematic = false;
                    myRigidBody.useGravity = true;
                    myTransform.position = SpawnColumnFour_Pos;
                }
                if (myTransform.position.x == Column_Five)
                {
                    isStored = false;
                    boxFiveStored = false;
                    myRigidBody.isKinematic = false;
                    myRigidBody.useGravity = true;
                    myTransform.position = SpawnColumnFive_Pos;
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        HexBehavior nutBehavior = collision.gameObject.GetComponent<HexBehavior>();

        if (nutBehavior != null)
        {
            if (gameObject.GetComponent<HexBehavior>().myColor.colorName == Null_ColorName)
            {
                return;
            }
            if (myColor == collision.gameObject.GetComponent<HexBehavior>().myColor)
            {
                if (!touchList.Contains(collision.gameObject))
                {
                    touchList.Add(collision.gameObject);
                }
                else if (touchList.Contains(collision.gameObject))
                {
                    foreach (GameObject ball in nutBehavior.touchList)
                    {
                        if (!touchList.Contains(ball))
                        {
                            if (ball.GetComponent<HexBehavior>().myColor == myColor)
                            {
                                touchList.Add(ball);
                            }
                        }
                    }
                }
            }
            else if (myColor != nutBehavior.myColor)
            {
                if (!touchList.Contains(collision.gameObject))
                {
                    foreach (GameObject ball in nutBehavior.touchList)
                    {
                        if (!touchList.Contains(ball))
                        {
                            touchList.Remove(ball);
                        }
                    }
                }
                else if (touchList.Contains(collision.gameObject))
                {
                    touchList.Remove(collision.gameObject);
                }
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        foreach (GameObject ball in touchList.ToArray())
        {
            if (ball != null)
            {
                ball.GetComponent<HexBehavior>().touchList.Clear();
            }
        }

        touchList.Clear();
    }

    private void OnCollisionExit(Collision collision)
    {
        foreach (GameObject ball in touchList.ToArray())
        {
            if (ball != null)
            {
                ball.GetComponent<HexBehavior>().touchList.Clear();
            }
        }
        touchList.Clear();
    }

    private void Store()
    {
        if (!isStored)
        {
            if (myTransform.position.x == 1 && !boxOneStored)
            {
                if (isAccountedForDanger)
                {
                    --numAtDanger;
                    isAccountedForDanger = false;
                }
                Store_SFX.Play();
                isStored = true;
                myRigidBody.isKinematic = true;
                myRigidBody.useGravity = false;
                boxOneStored = true;
                myTransform.position = new Vector3(1, 0, -2);
            }
            else if (boxOneStored)
            {
                No_Store_SFX.Play();
            }

            if (myTransform.position.x == 2 && !boxTwoStored)
            {
                if (isAccountedForDanger)
                {
                    --numAtDanger;
                    isAccountedForDanger = false;
                }
                Store_SFX.Play();
                isStored = true;
                myRigidBody.isKinematic = true;
                myRigidBody.useGravity = false;
                boxTwoStored = true;
                myTransform.position = new Vector3(2, 0, -2);
            }
            else if (boxTwoStored)
            {
                No_Store_SFX.Play();
            }

            if (myTransform.position.x == 3 && !boxThreeStored)
            {
                if (isAccountedForDanger)
                {
                    --numAtDanger;
                    isAccountedForDanger = false;
                }
                Store_SFX.Play();
                isStored = true;
                boxThreeStored = true;
                myRigidBody.isKinematic = true;
                myRigidBody.useGravity = false;
                myTransform.position = new Vector3(3, 0, -2);
            }
            else if (boxThreeStored)
            {
                No_Store_SFX.Play();
            }

            if (myTransform.position.x == 4 && !boxFourStored)
            {
                if (isAccountedForDanger)
                {
                    --numAtDanger;
                    isAccountedForDanger = false;
                }
                Store_SFX.Play();
                isStored = true;
                boxFourStored = true;
                myRigidBody.isKinematic = true;
                myRigidBody.useGravity = false;
                myTransform.position = new Vector3(4, 0, -2);
            }
            else if (boxFourStored)
            {
                No_Store_SFX.Play();
            }

            if (myTransform.position.x == 5 && !boxFiveStored)
            {
                if (isAccountedForDanger)
                {
                    --numAtDanger;
                    isAccountedForDanger = false;
                }
                Store_SFX.Play();
                isStored = true;
                boxFiveStored = true;
                myRigidBody.isKinematic = true;
                myRigidBody.useGravity = false;
                myTransform.position = new Vector3(5, 0, -2);
            }
            else if (boxFiveStored)
            {
                No_Store_SFX.Play();
            }
        }
    }



    private void Update()
    {

        if (transform.position.y >= 4 && !isAccountedForDanger && myRigidBody.velocity.y >= 0)
        {
            ++numAtDanger;
            isAccountedForDanger = true;
        }
        if (transform.position.y < 4 && isAccountedForDanger && myRigidBody.velocity.y >= 0)
        {
            isAccountedForDanger = false;
        }
        if (numAtDanger >= 30)
        {
            BG_Music.pitch = 1.25f;
        }
        else
        {
            BG_Music.pitch = 1.0f;
        }
    }
}