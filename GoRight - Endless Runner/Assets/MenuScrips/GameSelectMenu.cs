using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelectMenu : MonoBehaviour
{
    //Function for Changing to next scene based off of build order
    public void LoadGame1()
    {
        SceneManager.LoadScene(1); //laods scene with index of +1 to prevous Scene
    }

    public void LoadGame2()
    {
        SceneManager.LoadScene(2); //laods scene with index of +1 to prevous Scene
    }

    public void LoadGame3()
    {
        SceneManager.LoadScene(3); //laods scene with index of +1 to prevous Scene
    }


}
