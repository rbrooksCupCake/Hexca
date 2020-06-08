using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class HighScoreTable_Behavior : MonoBehaviour
{
    public GameObject HighScore_Table_Obj;
    public GameObject Row_Obj;

    public Sprite GoldenEmblem;
    public Sprite SilverEmblem;
    public Sprite BronzeEmblem;
    public Sprite BlueEmblem;
    public Sprite GreenEmblem;
    public Sprite RedEmblem;
    public Sprite OrangeEmblem;
    public Sprite PurpleEmblem;

    public List<HighScore_Row> HighScores = new List<HighScore_Row>();

    public bool areScoresUpdated;

    public IntVariable finalScore;
    public StringVariable finalName;


    public void Sort()
    {
        HighScores  = HighScores.OrderByDescending(x => x.playerScore).ToList();
    }

    private void OnEnable()
    {
        UpdateHighScoreTable();
    }

    public void AddScore()
    {
        HighScore_Row myScore = new HighScore_Row(finalName.RuntimeValue, finalScore.RuntimeValue);
        HighScores.Add(myScore);
    }

    public Sprite RandomEmblem()
    {
        int choice = Random.Range(0, 4);
        if(choice==0)
        {
            return BlueEmblem;
        }
        else if(choice==1)
        {
            return GreenEmblem;
        }
        else if (choice == 2)
        {
            return RedEmblem;
        }
        else if (choice == 3)
        {
            return PurpleEmblem;
        }
        return null;
    }

    private void ClearHighScoreTable_Obj()
    {
        foreach (Transform child in GetComponent<Transform>())
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void UpdateHighScoreTable()
    {
        ClearHighScoreTable_Obj();
        Sort();
        areScoresUpdated = false;
        for (int i = 0; i <= HighScores.Count; ++i)
        {
            if (i == HighScores.Count)
            {
                areScoresUpdated = true;
                break;
            }
            GameObject HighScore_Row_Instance = Instantiate(Row_Obj, Vector3.zero, Quaternion.identity);
            HighScore_Row_Instance.GetComponent<RectTransform>().position = new Vector3(0, -400f, 0);
            HighScore_Row_Instance.GetComponent<RowBehavior>().playerName = HighScores[i].playerName;
            HighScore_Row_Instance.GetComponent<RowBehavior>().playerScore = HighScores[i].playerScore;
            HighScore_Row_Instance.GetComponent<RowBehavior>().index = i;
            HighScore_Row_Instance.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>(),false);
            HighScore_Row_Instance.GetComponent<RectTransform>().localScale = Vector3.one;
            HighScore_Row_Instance.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
        if(areScoresUpdated)
        {
            UpdateHighScores_View();
        }
    }

    private void UpdateHighScores_View()
    {
        for (int i=0; i<GetComponent<Transform>().childCount;++i)
        {
            if (i==0)
            {
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).gameObject.name = "Golden Emblem";
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = GoldenEmblem;
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(1).GetComponent<Text>().text = GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerName + " - " + GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerScore;
            }
            else if(i==1)
            {
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).gameObject.name = "Silver Emblem";
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = SilverEmblem;
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(1).GetComponent<Text>().text = GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerName + " - " + GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerScore; 
            }
            else if(i==2)
            {
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).gameObject.name = "Bronze Emblem";
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = BronzeEmblem;
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(1).GetComponent<Text>().text = GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerName + " - " + GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerScore; 

            }
            else if(i>=3)
            {
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).gameObject.name = "Emblem";
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(0).GetComponent<Image>().sprite = RandomEmblem();
                GetComponent<Transform>().GetChild(i).gameObject.transform.GetChild(1).GetComponent<Text>().text = GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerName + " - " + GetComponent<Transform>().GetChild(i).gameObject.GetComponent<RowBehavior>().playerScore; 
            }
        }
    }
}

