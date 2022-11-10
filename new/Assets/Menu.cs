using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("Player");

    }

    void dificuldade()
    {

    }

    public void quit()
    {
        Application.Quit();
    }

    public void reiniciar()
    {
        SceneManager.LoadScene("Player");
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
