using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    private void Start()
    {
        Resume();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //if escape key is pressed 
        {
            if (GameIsPaused) // checks if game is already paused.
            {
                Resume(); // resumes game
            } else
            {
                Pause(); // pauses game
            }

        }
    }


    public void Resume()
    {
        //closes the pause menu and sets the time scale to normal
        pauseMenuUI.SetActive(false); //disables pause screen
        Time.timeScale = 1f; // set time scale to normal
        GameIsPaused = false; // sets pause to false
        pauseButton.SetActive(true); //shows pause button
    }

   public void Pause()
    {
        pauseMenuUI.SetActive(true); // enable the pause ui
        pauseButton.SetActive(false); // hides the pause button
        Time.timeScale = 0f; //sets time sacle to 0
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Main Menu...");
        SceneManager.LoadScene("Start_Menu"); //loads main menu if retern to menu button pressed
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main_game"); // reloads the scene if the player presses restart.
        
    }

}