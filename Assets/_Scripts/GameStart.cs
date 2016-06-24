using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour
{

    public void startGame()
    {
        Application.LoadLevel(1);
    }
    public void loadGame()
    {
        PlayerPrefs.SetInt("loadgame", 1);
        Application.LoadLevel(1);
    }
    public void closeGame()
    {
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
