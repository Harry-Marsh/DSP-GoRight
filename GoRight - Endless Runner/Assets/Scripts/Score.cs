using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    [SerializeField] public static float scoreValue = 0;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] public float timer;
    [SerializeField] public float score;


    // Update is called once per frame
    void Update()
    {
        scoreValue = GameObject.Find("PlatformGenerator").GetComponent<PlatformGeneration>().blockScore; // gets score from the platofrm generator
        

        ScoreText.text = "SCORE: " + Math.Truncate(scoreValue); // displays score as text.
    }
}
