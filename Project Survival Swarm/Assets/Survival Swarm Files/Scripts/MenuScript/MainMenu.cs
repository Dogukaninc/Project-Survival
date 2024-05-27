using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject creditsPanel;

   
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }


    public void QuitGame()
    {

        Application.Quit();
        Debug.Log("Game is exiting");
    }


    public void OpenCredits()
    {

        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void CloseCredits()
    {
        // Credits panelini kapatýr
        creditsPanel.SetActive(false);
    }
}
