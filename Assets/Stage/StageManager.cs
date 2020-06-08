using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public GameObject backBlock;
    public GameObject foreBlock;

    public int STAGE_X = 6;
    public int STAGE_Y = 9;

    public float cameraXPos=2;
    public float cameraYPos=5;


    private void createBackground(GameObject Background)
    {
        for (int x = -50; x < 50; ++x)
        {
            for (int y = -50; y < 50; ++y)
            {
                GameObject BackBlock = Instantiate(backBlock, new Vector3(x, y, 15), Quaternion.identity);
                BackBlock.transform.SetParent(Background.transform);

            }
        }
    }

    private void createStage(GameObject Stage)
    {
        for (int x = 0; x < STAGE_X; ++x)
        {
            for(int y = 0; y < STAGE_Y; ++y)
            {
                GameObject BackBlock = Instantiate(backBlock, new Vector3(x, y, 0),Quaternion.identity);
                BackBlock.transform.SetParent(Stage.transform);
            }
        }
    }

    private void createForeStage(GameObject Stage)
    {

        for (int y = 0; y < STAGE_Y; ++y)
        {
            GameObject ForeBlock = Instantiate(foreBlock, new Vector3(0, y, -1), Quaternion.identity);
            ForeBlock.transform.SetParent(Stage.transform);

        }
        for (int y = 0; y < STAGE_Y; ++y)
        {
            GameObject ForeBlock = Instantiate(foreBlock, new Vector3(STAGE_X, y, -1), Quaternion.identity);
            ForeBlock.transform.SetParent(Stage.transform);
        }

        for (int x = 0; x < STAGE_X; ++x)
        {
            GameObject ForeBlock = Instantiate(foreBlock, new Vector3(x, 0, -1), Quaternion.identity);
            ForeBlock.transform.SetParent(Stage.transform);

        }

    }

    /*public void createSpawnPoints()
    {
        GameObject spawnPointOne = new GameObject("SpawnPoint_One");
        spawnPointOne.transform.position = new Vector3(1, 10, -1);

        GameObject spawnPointTwo = new GameObject("SpawnPoint_Two");
        spawnPointTwo.transform.position = new Vector3(2, 10, -1);

        GameObject spawnPointThree = new GameObject("SpawnPoint_Three");
        spawnPointThree.transform.position = new Vector3(3, 10, -1);

        GameObject spawnPointFour = new GameObject("SpawnPoint_Four");
        spawnPointFour.transform.position = new Vector3(4, 10, -1);

        GameObject spawnPointFive = new GameObject("SpawnPoint_Five");
        spawnPointFive.transform.position = new Vector3(5, 10, -1);
    } */



    public void createStageFunction()
    {
        GameObject Stage = new GameObject("Stage");
        //GameObject Background = new GameObject("Stage_Background");
        //createBackground(Background);
        createStage(Stage);
        createForeStage(Stage);
    }

}
