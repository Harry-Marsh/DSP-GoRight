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

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = -25;
    }

    // Update is called once per frame
    void Update()
    {
        scoreValue = GameObject.Find("PlatformGenerator").GetComponent<PlatformGeneration>().blockScore;
        //scoreValue = Mathf.FloorToInt(Time.timeSinceLevelLoad) * 10;

        ScoreText.text = "SCORE: " + Math.Truncate(scoreValue);
    }
}
