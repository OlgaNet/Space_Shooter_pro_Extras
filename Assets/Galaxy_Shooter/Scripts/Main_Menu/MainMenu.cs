using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        Debug.Log("Single Player game Loading...");
        //load the game scene
        //SceneManager.LoadScene("Single_Player");
        SceneManager.LoadScene(1); //game scene
    }

    public void LoadCoOpMode()
    {
        Debug.Log("Co-Op Mode Loading...");
        SceneManager.LoadScene(2);
    }

    private void Update()
    {
        //if the "Escape" is pressed 
        //quit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
