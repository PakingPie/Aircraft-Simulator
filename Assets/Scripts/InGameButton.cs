using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameButton : MonoBehaviour
{
    public Button menuButton;
    public Button pauseButton;
    public Text pauseText;
    bool isPauseClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(clickMenu);
        pauseButton.onClick.AddListener(clickPause);
    }
    void clickMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    // Update is called once per frame
    void clickPause()
    {
        if(!isPauseClicked)
        {
            Time.timeScale = 0;
            isPauseClicked = true;
            pauseText.text = "Resume";
        }
        else
        {
            Time.timeScale = 1;
            isPauseClicked = false;
            pauseText.text = "Pause";
        }
    }
}
