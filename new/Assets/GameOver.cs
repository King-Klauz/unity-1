using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject painelGameOver;

    void start()
    {
        Time.timeScale = 1;
    }


    void update()
    {

    }

    public void quit()
    {
        Application.Quit();
    }

    public void reiniciar()
    {
        SceneManager.LoadScene(0);
    }

    /*public void gameover()
    {
        Time.timeScale = 0f;
        painelGameOver.setActive(true);
    }*/
}
