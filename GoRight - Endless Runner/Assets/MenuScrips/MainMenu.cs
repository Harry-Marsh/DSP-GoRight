using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Function for Changing to next scene based off of build order
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //laods scene with index of +1 to prevous Scene
    }

    //Function for Exiting Application
    public void QuitGame()
    {
        Debug.Log("Applecation Exited."); //debug output
        Application.Quit(); //exits apllication 
    }

}
