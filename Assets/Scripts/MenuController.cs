using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public Button newGame;
    public Button exitGame;
    // Start is called before the first frame update
    private void Start()
    {
        newGame.onClick.AddListener(clickNewGameButton);
        exitGame.onClick.AddListener(clickExitGameButton);
        Cursor.visible = true;
    }
    void clickNewGameButton()
    {
        Debug.Log("Working");
        SceneManager.LoadScene("AircraftSimulator");
    }
    void clickExitGameButton()
    {
        Application.Quit();
    }
}
