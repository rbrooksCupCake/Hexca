using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOverBox_Behavior : MonoBehaviour
{
    public GameEventListener GameOverListener;
    public GameEvent GameOver;
    public static GameEvent GameOverStatic;
    public static bool IsGameOver;
    public static bool IsNewRecord;
    public ScoringSystem CurrentScoringSystem;
    public GameObject NewRecordExplosion;
    public GameObject RecordWindow;

    private void Awake()
    {
        GameOverStatic = GameOver;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="Hex")
        {
            if(!IsGameOver)
            {
                StartCoroutine(CheckScore());
            }
            SetGameOverToTrue();
        }
    }

    public static void OpenGameOverWindow()
    {
        GameOverStatic.Raise();
    }

    IEnumerator CheckScore()
    {
        if(StatViewer_Handler.BestRoundProfile!=null)
        {
            if (StatViewer_Handler.BestRoundProfile.Score < CurrentScoringSystem.playerScore.RuntimeValue)
            {
                if (NewRecordExplosion != null)
                {
                    IsNewRecord = true;
                    NewRecordExplosion.SetActive(true);
                    yield return new WaitForSeconds(0.75f);
                    NewRecordExplosion.SetActive(false);
                    ScoringSystem.CurrentProfile.SetScore(CurrentScoringSystem.playerScore.RuntimeValue);
                    StatViewer_Handler.SaveRecordToFile(ScoringSystem.CurrentProfile);
                    RecordWindow.SetActive(true);
                    Time.timeScale = 0f;
                    RecordWindow.SetActive(true);
                }
            }
        }
        else
        {
            if (NewRecordExplosion != null)
            {
                IsNewRecord = true;
                NewRecordExplosion.SetActive(true);
                yield return new WaitForSeconds(0.75f);
                NewRecordExplosion.SetActive(false);
                Time.timeScale = 0f;
                ScoringSystem.CurrentProfile.SetScore(CurrentScoringSystem.playerScore.RuntimeValue);
                StatViewer_Handler.SaveRecordToFile(ScoringSystem.CurrentProfile);
                RecordWindow.SetActive(true);
            }
        }
        yield return StartCoroutine(WaitForFileSave(StatViewer_Handler.Round_FileName));
        if(!IsNewRecord)
        {
            OpenGameOverWindow();
        }
    }

    IEnumerator WaitForFileSave(string fileName)
    {
        File.Delete(Application.dataPath + fileName);
        yield return new WaitForEndOfFrame();
        ScoringSystem.CurrentProfile.SetRoundTime();
        if (ScoringSystem.CurrentProfile.RoundTime < 0)
        {
            Debug.LogError("ROUND TIME IS NEGATIVE. POSSIBLE OVERFLOW.");
        }
        ScoringSystem.CurrentProfile.SetScore(CurrentScoringSystem.playerScore.RuntimeValue);
        StatViewer_Handler.SaveRoundToFile(ScoringSystem.CurrentProfile);
        yield return new WaitForEndOfFrame();
    }
    public void SetGameOverToTrue()
    {
        IsGameOver = true;
    }

}
