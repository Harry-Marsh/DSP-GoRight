using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class highScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    private float theHighScore;
    private float theCurrentHighScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        theHighScore = PlayerPrefs.GetFloat("HighScore", theHighScore); //gets the score from the players saved local
        theCurrentHighScore = GameObject.Find("PlayableCharacter").GetComponent<PlayerController>().theHighScore; //gets the high score fromk the current playthrough.
        if (theCurrentHighScore > theHighScore)
        {
            ScoreText.text = "HIGHEST SCORE: " + theCurrentHighScore;
        }else
        {
            ScoreText.text = "HIGHEST SCORE: " + theHighScore;
        }

    }
}
