using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;           //TextMesh Pro Text Field for score
    public TextMeshProUGUI hiScoreText;         //TextMesh Pro Text Field for Hiscore



    public float scoreCount;                    // What is the score count
    private float hiScoreCount;                  // what is the hi score

    public float pointsPerSecond;               // how much to increase score by
    public bool scoreIncreasing;                // is score increasing ? dont want to increase while dead

    public bool shouldDouble;                   // if powerup double active;

    
    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))                // if highscore exists in playprefs
        {
            hiScoreCount = PlayerPrefs.GetFloat("HighScore");       // pull from player prefs
            
        }
    }


    void Update()
    {
       
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;     // how much to increase by per second
        }
        

        if (scoreCount > hiScoreCount)                      // if score > hghscore update highscore
        {
            hiScoreCount = scoreCount;                      // update highscore
             PlayerPrefs.SetFloat("HighScore", hiScoreCount);            // AW save highscore to playerPrefs may not be the best place for it as this happens while player is running
            
        }

        scoreText.text = "Score : " + Mathf.Round (scoreCount);           // set the scorecout on screen rount to solid number
        hiScoreText.text = "HiScore : " + Mathf.Round(hiScoreCount);     // Set the hi score on screen

    }

    public void AddScore(int pointsToAdd)
    {
        if (shouldDouble)               // if we are doubling points
        {
            pointsToAdd = pointsToAdd * 2;  // double the points
        }
        scoreCount += pointsToAdd;      // otherwise normal add
     }   
}
