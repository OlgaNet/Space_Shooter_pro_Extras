using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        //load the game scene
        SceneManager.LoadScene(1); //game scene
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
