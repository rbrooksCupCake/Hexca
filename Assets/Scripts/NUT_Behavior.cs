using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NUT_Behavior : MonoBehaviour
{

    public const int NUT_LAYER_INDEX = 10;

    public enum nutColor { Blue, Green, Red, Orange, Purple, Null };
    public nutColor myColor;

    public enum nutState { Loading, Normal, Shattering, Stored }
    public nutState myState;
    public float timeToLoad;

    public enum nutShape { None, Line, Box, Plus };
    public nutShape myShape;

    public static int groupScore;

    public List<GameObject> touchList;

    public Renderer rend = null;
    public Material currentMaterial = null;
    public Material[] NUT_Materials = null;
    public float loadDuration = 2;
    public float shatterDuration = 1.0F;


    [Header("Default Materials")]
    public static Material blueDefault;
    public static Material greenDefault;
    public static Material redDefault;
    public static Material orangeDefault;
    public static Material purpleDefault;
    public static Material nullNut_Mat;

    [Header("Loading Materials")]
    public static Material blueLoading;
    public static Material greenLoading;
    public static Material redLoading;
    public static Material orangeLoading;
    public static Material purpleLoading;

    [Header("Shatter Materials")]
    public static Material blueShattering;
    public static Material greenShattering;
    public static Material redShattering;
    public static Material orangeShattering;
    public static Material purpleShattering;

    [Header("Explosions")]
    public GameObject blueExplosion;
    public GameObject greenExplosion;
    public GameObject redExplosion;
    public GameObject orangeExplosion;
    public GameObject purpleExplosion;

    public static bool boxOneStored;
    public static bool boxTwoStored;
    public static bool boxThreeStored;
    public static bool boxFourStored;
    public static bool boxFiveStored;

    public enum Shape_Orientation { Norm_Zero, Norm_Ninty, Norm_OneEighty, Norm_TwoSeventy, Flip_Zero, Flip_Ninty, Flip_OneEighty, Flip_TwoSeventy };
    public Shape_Orientation PowerUp_Orientation;

    public Transform myTransform;
    public Rigidbody myRigidbody;

    public GameState.Announcements theAnnouncement;

    public Mesh myMesh;
    public Mesh CB_Mesh;

    public MaterialPack myMaterial;

    private void LoadMaterials()
    {
        blueDefault = NUT_Materials[0];
        greenDefault = NUT_Materials[1];
        redDefault = NUT_Materials[2];
        orangeDefault = NUT_Materials[3];
        purpleDefault = NUT_Materials[4];


        blueLoading = NUT_Materials[5];
        greenLoading = NUT_Materials[6];
        redLoading = NUT_Materials[7];
        orangeLoading = NUT_Materials[8];
        purpleLoading = NUT_Materials[9];

        blueShattering = NUT_Materials[10];
        greenShattering = NUT_Materials[11];
        redShattering = NUT_Materials[12];
        orangeShattering = NUT_Materials[13];
        purpleShattering = NUT_Materials[14];

        nullNut_Mat = NUT_Materials[15];
        
    }

    private void OnCollisionStay(Collision collision)
    {

        NUT_Behavior nutBehavior = collision.gameObject.GetComponent<NUT_Behavior>();

        if (nutBehavior != null)
        {
            if (myColor == collision.gameObject.GetComponent<NUT_Behavior>().myColor)
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
                            if(ball.GetComponent<NUT_Behavior>().myColor==myColor)
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
                ball.GetComponent<NUT_Behavior>().touchList.Clear();
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
                ball.GetComponent<NUT_Behavior>().touchList.Clear();
            }
        }
        touchList.Clear();
    }

    public IEnumerator Loading()
    {
        if (GameState.tutorialActive == true)
        {
            timeToLoad = (GameState.TUTORIALTIMEBETWEENLOADS / 2.0f);
        }
        updateMaterial();
        yield return new WaitForSeconds(timeToLoad);
        myState = nutState.Normal;
        updateMaterial();
        yield return new WaitForSeconds(0.001f);
    }

    private void OnDisable()
    {
        groupScore = 0;
    }

    private void OnEnable()
    {
        if (myColor != nutColor.Null)
        {
            myColor = (nutColor)Random.Range(0, System.Enum.GetValues(typeof(nutColor)).Length - 1);
        }
        myState = nutState.Loading;
        myRigidbody.useGravity = false;
        StartCoroutine(Loading());

    }


    public void removesphere()
    {
        transform.position = new Vector3(-1000, -1000, -1000);
        touchList.Clear();
        myRigidbody.useGravity = false;
        gameObject.SetActive(false);
        GameState.nutPool.Add(gameObject);

    }

    private void Tap()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject && hit.collider.gameObject.GetComponent<NUT_Behavior>() != null)
                    {

                        hit.collider.gameObject.GetComponent<NUT_Behavior>().Tapped();
                    }
                }
            }
        }
    }

    private void Tapped()
    {
        if (GameState.canTap)
        {
            if (touchList.Count >= 2 && myColor != nutColor.Null && myState == nutState.Normal)
            {
                GameState.isScoreChanging = true;
                GameState.firstPassScore = false;
                GameState.shapeMultiplier = 1;
                GameState.GetGameState().GetComponent<AudioHandler>().playPopEffect();
                foreach (GameObject piece in touchList)
                {
                    piece.GetComponent<Rigidbody>().useGravity = false;
                }
                if (checkLine())
                {
                    GameState.shapeMultiplier = 50;
                    linePowerUp();
                }

                else if (checkBox())
                {
                    GameState.shapeMultiplier = 20;
                    boxPowerUp();
                    theAnnouncement = GameState.Announcements.Boxy;
                }

                else if (checkLPiece())
                {
                    GameState.shapeMultiplier = 40;
                    LPowerUp();
                    theAnnouncement = GameState.Announcements.Linear;
                }
                else if (checkPlus())
                {
                    if (!GameState.isTimeSlowed)
                    {
                        GameState.shapeMultiplier = 100;
                        GameState.isTimeSlowed = true;
                    }
                }
                StartCoroutine(Explode());
            }
            else if (myColor != nutColor.Null)
            {
                if (myState == nutState.Normal)
                {
                    storeSphere();
                }
                else if (myState == nutState.Stored)
                {
                    swapSphere();
                }
            }
        }
    }
    
    private void OnMouseDown()
    {
        if (Application.isEditor)
        {
            if (GameState.canTap)
            {
                if (touchList.Count >= 2 && myColor != nutColor.Null && myState == nutState.Normal)
                {
                    GameState.isScoreChanging = true;
                    GameState.firstPassScore = false;
                    GameState.shapeMultiplier = 1;
                    GameState.GetGameState().GetAudioHandler().playPopEffect();
                    foreach (GameObject piece in touchList)
                    {
                        piece.GetComponent<Rigidbody>().useGravity = false;
                    }
                    if (checkLine())
                    {
                        GameState.shapeMultiplier = 50;
                        linePowerUp();
                        theAnnouncement = GameState.Announcements.Linear;
                    }

                    else if (checkBox())
                    {
                        GameState.shapeMultiplier = 20;
                        boxPowerUp();
                        theAnnouncement = GameState.Announcements.Boxy;
                    }

                    else if (checkLPiece())
                    {
                        GameState.shapeMultiplier = 40;
                        LPowerUp();
                        theAnnouncement = GameState.Announcements.L_Piece;
                    }
                    else if (checkPlus())
                    {
                        if (!GameState.isTimeSlowed)
                        {
                            GameState.shapeMultiplier = 100;
                            GameState.isTimeSlowed = true;
                            theAnnouncement = GameState.Announcements.Plus_Time;
                        }
                    }
                    StartCoroutine(Explode());
                }
                else if (myColor != nutColor.Null)
                {
                    if (myState == nutState.Normal)
                    {
                        storeSphere();
                        updateMaterial();
                    }
                    else if (myState == nutState.Stored)
                    {
                        swapSphere();
                        updateMaterial();
                    }
                }
            }
        }
    }
    
    void swapSphere()
    {
        if (myTransform.position.x == 1 && boxOneStored == true)
        {
            GameState.GetGameState().GetAudioHandler().playSwapInEffect();
            myRigidbody.isKinematic = false;
            transform.position = new Vector3(1, 9, -1);
            myState = nutState.Normal;
            boxOneStored = false;

        }
        else if (myTransform.position.x == 2 && boxTwoStored == true)
        {
            GameState.GetGameState().GetAudioHandler().playSwapInEffect();
            myRigidbody.isKinematic = false;
            transform.position = new Vector3(2, 9, -1);
            myState = nutState.Normal;
            boxTwoStored = false;
        }
        else if (myTransform.position.x == 3 && boxThreeStored == true)
        {
            GameState.GetGameState().GetAudioHandler().playSwapInEffect();
            myRigidbody.isKinematic = false;
            transform.position = new Vector3(3, 9, -1);
            myState = nutState.Normal;
            boxThreeStored = false;
        }
        else if (myTransform.position.x == 4 && boxFourStored == true)
        {
            GameState.GetGameState().GetAudioHandler().playSwapInEffect();
            myRigidbody.isKinematic = false;
            transform.position = new Vector3(4, 9, -1);
            myState = nutState.Normal;
            boxFourStored = false;
        }
        else if (myTransform.position.x == 5 && boxFiveStored == true)
        {
            GameState.GetGameState().GetAudioHandler().playSwapInEffect();
            myRigidbody.isKinematic = false;
            transform.position = new Vector3(5, 9, -1);
            myState = nutState.Normal;
            boxFiveStored = false;
        }
    }

    void storeSphere()
    {
        if (myTransform.position.x == 1 && boxOneStored == false)
        {
            GameState.GetGameState().GetAudioHandler().playStoreEffect();
            transform.position = new Vector3(1, 0, -2);
            myState = nutState.Stored;
            myRigidbody.isKinematic = true;
            boxOneStored = true;

        }
        else if (myTransform.position.x == 2 && boxTwoStored == false)
        {
            GameState.GetGameState().GetAudioHandler().playStoreEffect();
            transform.position = new Vector3(2, 0, -2);
            myState = nutState.Stored;
            myRigidbody.isKinematic = true;
            boxTwoStored = true;
        }
        else if (myTransform.position.x == 3 && boxThreeStored == false)
        {
            GameState.GetGameState().GetAudioHandler().playStoreEffect();
            transform.position = new Vector3(3, 0, -2);
            myState = nutState.Stored;
            myRigidbody.isKinematic = true;
            boxThreeStored = true;
        }
        else if (myTransform.position.x == 4 && boxFourStored == false)
        {
            GameState.GetGameState().GetAudioHandler().playStoreEffect();
            transform.position = new Vector3(4, 0, -2);
            myState = nutState.Stored;
            myRigidbody.isKinematic = true;
            boxFourStored = true;
        }
        else if (myTransform.position.x == 5 && boxFiveStored == false)
        {
            GameState.GetGameState().GetAudioHandler().playStoreEffect();
            transform.position = new Vector3(5, 0, -2);
            myState = nutState.Stored;
            myRigidbody.isKinematic = true;
            boxFiveStored = true;
        }
        else
        {
            GameState.GetGameState().GetAudioHandler().playNoSwapEffect();
        }
    }


    void updateMaterial()
    {
        if (myState == nutState.Loading)
        {
            if (myColor == nutColor.Blue)
            {
                rend.sharedMaterial = blueLoading;
            }
            else if (myColor == nutColor.Green)
            {
                rend.sharedMaterial = greenLoading;
            }
            else if (myColor == nutColor.Red)
            {

                rend.sharedMaterial = redLoading;
               
            }
            else if (myColor == nutColor.Orange)
            {

                rend.sharedMaterial = orangeLoading;


            }
            else if (myColor == nutColor.Purple)
            {

                rend.sharedMaterial = purpleLoading;

                

            }
            else if (myColor == nutColor.Null)
            {

                    rend.sharedMaterial = nullNut_Mat;
                
            }
        }
        else if (myState == nutState.Normal)
        {
            if (myColor == nutColor.Blue)
            {

                rend.sharedMaterial = blueDefault;

                
            }
            else if (myColor == nutColor.Green)
            {

                rend.sharedMaterial = greenDefault;

                
            }
            else if (myColor == nutColor.Red)
            {

                rend.sharedMaterial = redDefault;
                

            }
            else if (myColor == nutColor.Orange)
            {

                    rend.sharedMaterial = orangeDefault;
                

            }
            else if (myColor == nutColor.Purple)
            {

                rend.sharedMaterial = purpleDefault;
                

            }
            else if (myColor == nutColor.Null)
            {

                rend.sharedMaterial = nullNut_Mat;
                
            }
        }
        else if (myState == nutState.Shattering)
        {
            if (myColor == nutColor.Blue)
            {

                rend.sharedMaterial = blueShattering;

                
            }
            else if (myColor == nutColor.Green)
            {

                rend.sharedMaterial = greenShattering;
                
            }
            else if (myColor == nutColor.Red)
            {

                rend.sharedMaterial = redShattering;
                
            }
            else if (myColor == nutColor.Orange)
            {

                rend.sharedMaterial = orangeShattering;
                
            }
            else if (myColor == nutColor.Purple)
            {

                rend.sharedMaterial = purpleShattering;
                
            }
            else if (myColor == nutColor.Null)
            {

                rend.sharedMaterial = nullNut_Mat;
                
            }
        }
        else if (myState == nutState.Stored)
        {
            if (myColor == nutColor.Blue)
            {

                rend.sharedMaterial = blueLoading;
                
            }
            else if (myColor == nutColor.Green)
            {

                rend.sharedMaterial = greenLoading;
                
            }
            else if (myColor == nutColor.Red)
            {

                rend.sharedMaterial = redLoading;
                
            }
            else if (myColor == nutColor.Orange)
            {

                rend.sharedMaterial = orangeLoading;
                
            }
            else if (myColor == nutColor.Purple)
            {
                rend.sharedMaterial = purpleLoading;
                
            }
        }
    }

    IEnumerator Explode()
    {
        myState = nutState.Shattering;
        updateMaterial();
        yield return new WaitForSeconds(0.25f);
        ++GameState.numNuts_Score;
        GameState.isScoreChanging = true;


        foreach (GameObject Nut in touchList)
        {
            if (Nut.activeInHierarchy == true)
            {
                if (Nut != gameObject)
                {
                    if (Nut.GetComponent<NUT_Behavior>().myColor == myColor)
                    {
                        Nut.GetComponent<NUT_Behavior>().StartCoroutine(Explode());
                    }
                }

            }
        }

        GameState.addScore(10);
        
        if (myColor == nutColor.Blue)
        {
            GameState.lastColorPopped = nutColor.Blue;
            GameObject Explosion = Instantiate(blueExplosion, myTransform.position, Quaternion.identity);
            GameState.GetGameState().GetAudioHandler().playExplosionEffect();
            Destroy(Explosion, 0.25f);
        }
        else if (myColor == nutColor.Green)
        {

            GameState.lastColorPopped = nutColor.Green;
            GameObject Explosion = Instantiate(greenExplosion, myTransform.position, Quaternion.identity);
            GameState.GetGameState().GetAudioHandler().playExplosionEffect();
            Destroy(Explosion, 0.5f);
        }
        else if (myColor == nutColor.Red)
        {
            GameState.lastColorPopped = nutColor.Red;
            GameObject Explosion = Instantiate(redExplosion, myTransform.position, Quaternion.identity);
            GameState.GetGameState().GetAudioHandler().playExplosionEffect();
            Destroy(Explosion, 0.5f);
        }
        else if (myColor == nutColor.Orange)
        {
            GameState.lastColorPopped = nutColor.Orange;
            GameObject Explosion = Instantiate(orangeExplosion, myTransform.position, Quaternion.identity);
            GameState.GetGameState().GetAudioHandler().playExplosionEffect();
            Destroy(Explosion, 0.5f);
        }
        else if (myColor == nutColor.Purple)
        {
            GameState.lastColorPopped = nutColor.Purple;
            GameObject Explosion = Instantiate(purpleExplosion, myTransform.position, Quaternion.identity);
            GameState.GetGameState().GetAudioHandler().playExplosionEffect();
            Destroy(Explosion, 0.5f);
        }
        removesphere();

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

    void checkFlipped()
    {
        if (gameObject.GetComponent<NUT_Behavior>().myColor == nutColor.Blue)
        {
            StatViewer_Handler.RoundProfile.AddToBlueFlipped();
        }
        else if (gameObject.GetComponent<NUT_Behavior>().myColor == nutColor.Green)
        {
            StatViewer_Handler.RoundProfile.AddToGreenFlipped();
        }
        else if (gameObject.GetComponent<NUT_Behavior>().myColor == nutColor.Red)
        {
            StatViewer_Handler.RoundProfile.AddToGreenFlipped();
        }
        else if (gameObject.GetComponent<NUT_Behavior>().myColor == nutColor.Orange)
        {
            StatViewer_Handler.RoundProfile.AddToOrangeFlipped();
        }
        else if (gameObject.GetComponent<NUT_Behavior>().myColor == nutColor.Purple)
        {
            StatViewer_Handler.RoundProfile.AddToRedFlipped();
        }
    }

    void boxPowerUp()
    {
        int xPos = Mathf.RoundToInt(touchList[0].transform.position.x);
        int yPos = Mathf.RoundToInt(touchList[0].transform.position.y);

        GameObject[] Nuts = GameState.nutActivePool.ToArray();
        foreach (GameObject Nut in Nuts)
        {

            if (Mathf.RoundToInt(Nut.transform.position.x) == xPos && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos + 2 && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;

            }
            if ((Mathf.RoundToInt(Nut.transform.position.x) == (xPos + 1)) && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos + 2 && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
            }

            if ((Mathf.RoundToInt(Nut.transform.position.x) == (xPos)) && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos - 1 && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
            }

            if ((Mathf.RoundToInt(Nut.transform.position.x) == (xPos + 1)) && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos - 1 && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
            }

            if ((Mathf.RoundToInt(Nut.transform.position.x) == (xPos - 1)) && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
            }

            if ((Mathf.RoundToInt(Nut.transform.position.x) == (xPos - 1)) && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos + 1 && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
            }

            if ((Mathf.RoundToInt(Nut.transform.position.x) == (xPos + 2)) && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
            }

            if ((Mathf.RoundToInt(Nut.transform.position.x) == (xPos + 2)) && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) == yPos + 1 && Nut.GetComponent<NUT_Behavior>().myColor != gameObject.GetComponent<NUT_Behavior>().myColor)
            {
                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
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
        int xPos = Random.Range(1, 6);
        int yPos = Mathf.RoundToInt(myTransform.position.y);

        GameObject[] Nuts = GameState.nutActivePool.ToArray();
        foreach (GameObject Nut in Nuts)
        {


            if (Mathf.RoundToInt(Nut.transform.position.x) == xPos && Nut.GetComponent<NUT_Behavior>().myState == nutState.Normal && Mathf.RoundToInt(Nut.transform.position.y) > yPos )
            {

                checkFlipped();
                Nut.GetComponent<NUT_Behavior>().myColor = gameObject.GetComponent<NUT_Behavior>().myColor;
            }
        }
    }

    bool checkLPiece()
    {
        if (touchList.Count == 4)
        {


            foreach (Shape_Orientation myOrientation in (Shape_Orientation[])System.Enum.GetValues(typeof(Shape_Orientation)))
            {
                if (myOrientation == Shape_Orientation.Norm_Zero)
                {
                    touchList = touchList.OrderByDescending(piece => piece.transform.position.x).ThenBy(piece => piece.transform.position.y).ToList<GameObject>();


                    if (Mathf.RoundToInt(touchList[0].transform.position.x) > Mathf.RoundToInt( touchList[1].transform.position.x ))
                    {
                        if (Mathf.RoundToInt(touchList[0].transform.position.y) == Mathf.RoundToInt(touchList[1].transform.position.y))
                        {
                            if (Mathf.RoundToInt(touchList[1].transform.position.y) < Mathf.RoundToInt(touchList[2].transform.position.y))
                            {
                                if (Mathf.RoundToInt(touchList[2].transform.position.y) < Mathf.RoundToInt(touchList[3].transform.position.y))
                                {
                                    if (  (Mathf.RoundToInt(touchList[1].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x)) && (Mathf.RoundToInt(touchList[2].transform.position.x) == Mathf.RoundToInt(touchList[3].transform.position.x)))
                                    {
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Norm_Zero;
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

                    if (Mathf.RoundToInt( touchList[0].transform.position.x) < Mathf.RoundToInt( touchList[1].transform.position.x ) )
                    {
                        if ( Mathf.RoundToInt( touchList[0].transform.position.y) == Mathf.RoundToInt( touchList[1].transform.position.y ) )
                        {
                            if ( Mathf.RoundToInt( touchList[1].transform.position.y) <  Mathf.RoundToInt(touchList[2].transform.position.y))
                            {
                                if ( Mathf.RoundToInt( touchList[2].transform.position.y) <  Mathf.RoundToInt( touchList[3].transform.position.y) )
                                {
                                    if (Mathf.RoundToInt(touchList[1].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x) &&  Mathf.RoundToInt( touchList[2].transform.position.x ) == Mathf.RoundToInt(touchList[3].transform.position.x) )
                                    {
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Flip_Zero;
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
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Norm_Ninty;
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

                    if (Mathf.RoundToInt( touchList[0].transform.position.y ) >  Mathf.RoundToInt ( touchList[1].transform.position.y ))
                    {
                        if ( Mathf.RoundToInt(touchList[0].transform.position.x) == Mathf.RoundToInt( touchList[1].transform.position.x ))
                        {
                            if (Mathf.RoundToInt( touchList[1].transform.position.x ) < Mathf.RoundToInt(touchList[2].transform.position.x ))
                            {
                                if (Mathf.RoundToInt( touchList[2].transform.position.x ) < Mathf.RoundToInt( touchList[3].transform.position.x))
                                {
                                    if (Mathf.RoundToInt(touchList[1].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y) && Mathf.RoundToInt(touchList[2].transform.position.y) == Mathf.RoundToInt(touchList[3].transform.position.y)) 
                                    {
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Flip_Ninty;
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
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Norm_OneEighty;
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
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Flip_OneEighty;
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
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Norm_TwoSeventy;
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
                                        touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation = Shape_Orientation.Flip_TwoSeventy;
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

        int xPos = Mathf.RoundToInt(touchList[0].transform.position.x);
        int yPos = Mathf.RoundToInt(touchList[0].transform.position.y);


        Shape_Orientation myOrientation = touchList[0].GetComponent<NUT_Behavior>().PowerUp_Orientation;

        if (myOrientation == Shape_Orientation.Norm_Zero)
        {

            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if ( Mathf.RoundToInt(Nut.transform.position.y) == yPos && Mathf.RoundToInt(Nut.transform.position.x) > xPos)
                {
                    checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }
        if (myOrientation == Shape_Orientation.Flip_Zero)
        {
            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if (Mathf.RoundToInt(Nut.transform.position.y) == yPos && Mathf.RoundToInt(Nut.transform.position.x) < xPos)
                {
                    checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }

        if (myOrientation == Shape_Orientation.Norm_Ninty)
        {
            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if (Mathf.RoundToInt(Nut.transform.position.y) < yPos && Mathf.RoundToInt(Nut.transform.position.x) == xPos)
                {
                    Nut.GetComponent<NUT_Behavior>().checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }


        if (myOrientation == Shape_Orientation.Flip_Ninty)
        {
            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if (Mathf.RoundToInt(Nut.transform.position.y) > yPos && Mathf.RoundToInt(Nut.transform.position.x) == xPos)
                {
                    checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }

        if (myOrientation == Shape_Orientation.Norm_OneEighty)
        {
            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if (Mathf.RoundToInt(Nut.transform.position.y) == yPos && Mathf.RoundToInt(Nut.transform.position.x) < xPos)
                {
                    checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }


        if (myOrientation == Shape_Orientation.Flip_OneEighty)
        {
            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if (Mathf.RoundToInt(Nut.transform.position.y) == yPos && Mathf.RoundToInt(Nut.transform.position.x) > xPos)
                {
                    checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }

        if (myOrientation == Shape_Orientation.Norm_TwoSeventy)
        {
            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if (Mathf.RoundToInt(Nut.transform.position.y) > yPos && Mathf.RoundToInt(Nut.transform.position.x) == xPos)
                {
                    checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }

        if (myOrientation == Shape_Orientation.Flip_TwoSeventy)
        {
            GameObject[] Nuts = GameState.nutActivePool.ToArray();
            foreach (GameObject Nut in Nuts)
            {
                if (Mathf.RoundToInt(Nut.transform.position.y) < yPos && Mathf.RoundToInt(Nut.transform.position.x) == xPos)
                {
                    checkFlipped();
                    Nut.GetComponent<NUT_Behavior>().myColor = touchList[0].GetComponent<NUT_Behavior>().myColor;
                }
            }
        }

    }

    bool checkPlus()
    {
        if (GetComponent<NUT_Behavior>().touchList.Count == 5)
        {
            touchList = touchList.OrderBy(piece => piece.transform.position.x).ThenBy(piece => piece.transform.position.y).ToList<GameObject>();



            if (Mathf.RoundToInt(touchList[0].transform.position.x) < Mathf.RoundToInt(touchList[2].transform.position.x) && (Mathf.RoundToInt(touchList[0].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y)))
            {
                if (Mathf.RoundToInt(touchList[1].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x) && (Mathf.RoundToInt(touchList[1].transform.position.y) < Mathf.RoundToInt(touchList[2].transform.position.y)))
                {

                    if (Mathf.RoundToInt(touchList[3].transform.position.x) == Mathf.RoundToInt(touchList[2].transform.position.x) && (Mathf.RoundToInt(touchList[3].transform.position.y) > Mathf.RoundToInt(touchList[2].transform.position.y)))
                    {

                        if (Mathf.RoundToInt(touchList[4].transform.position.x) > Mathf.RoundToInt(touchList[2].transform.position.x) && (Mathf.RoundToInt(touchList[4].transform.position.y) == Mathf.RoundToInt(touchList[2].transform.position.y)))
                        {
                            return true;
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

    public void QuitGame()
    {
        Application.Quit();
    }


    private void Awake()
    {
        rend = GetComponent<Renderer>();
        currentMaterial = GetComponent<Renderer>().material;
        myRigidbody = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();
        //LoadMaterials();
    }

    private void Start()
    {
        touchList = new List<GameObject>();
    }

    private void Update()
    {

        if (myRigidbody.IsSleeping())
        {
            myRigidbody.WakeUp();
        }
    }

}
