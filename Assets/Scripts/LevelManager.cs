using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int requiredScore;
    public Text text;
    float endGameCountdown = 5f;
    private void Start()
    {
        Cursor.visible = false;
        text.text = "";
    }

    private void Update()
    {
        if (ScoreManager.score >= requiredScore)
        {
            MissionGiver.isMissionFinished = true;
        }
    }
    public void winCon()
    {
        if (ScoreManager.score >= requiredScore)
        {
            text.text = "YOU WIN!";
            endGameCountdown -= Time.deltaTime;
            if (endGameCountdown <= 0)
            {
                ScoreManager.score = 0;
                SceneManager.LoadScene("Menu");
            }
            Cursor.visible = true;
        }
        else
            text.text = "";
    }
}
