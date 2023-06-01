using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoopMode = false;
    [SerializeField]
    private bool _isGameOver;
    private SpawnManadger _spawnManager;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject _coopPlayers;
    [SerializeField]
    private GameObject _pauseMenuPanel;

    private Animator _pauseAnimator;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manadger").GetComponent<SpawnManadger>();
        //_pauseAnimator = GameObject.Find("Pause_Menu").GetComponent<Animator>(); //test
        //_pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime; //test
    }

    private void Update()
    {
        //if the r key was pressed
        //restart the current scene
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            //SceneManager.LoadScene("Game");
            //or
            SceneManager.LoadScene(0); //current Game Scene
        }

        //if the "Escape" is pressed 
        //quit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //pause menu if the "P" is pressed 
        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        _pauseAnimator.SetBool("isPuased", true); //bug
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() Called");
        _isGameOver = true;
    }
}
