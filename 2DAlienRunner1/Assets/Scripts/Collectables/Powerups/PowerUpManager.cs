using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    private bool doublePoints;          // which powerup is being activated
    private bool safeMode;              // which powerup is being activated

    private bool  powerupActive;        // which powerup is active
    private float powerUpLenghtCounter; // How long is it active for

    private float normalPointsPerSecond;        // to store normal points per second
    private float LowObstacleRate;              // to store lowobstacle percentage rate

    private ScoreManager theScoreManager;       // need the score manager script
    private PlatformGenerator thePlatformGenerator;     // need the platformmanager script

    // Start is called before the first frame update
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerupActive)
        {
            powerUpLenghtCounter -= Time.deltaTime;     // Start decreasing the time

            if (doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond * 2;        // double the points per second
                theScoreManager.shouldDouble = true;
            }

            if (safeMode)
            {
                thePlatformGenerator.randomLowObstacleThreshold = 0;        // set low obstacle rate to 0
            }


            if (powerUpLenghtCounter <= 0)      // when at 0
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond;        // set points per second back
                thePlatformGenerator.randomLowObstacleThreshold = LowObstacleRate;  // set low obstacle rate back
                theScoreManager.shouldDouble = false;
                powerupActive = false;      // disable the powerup
            }
        }
    }

    public void ActivatePowerUp(bool points, bool safe, float time)     // get values from powerup scripts
    {
        doublePoints = points;              // set value to be local value
        safeMode = safe;                    // set value to be local value
        powerUpLenghtCounter = time;        // set value to be local value

        normalPointsPerSecond = theScoreManager.pointsPerSecond;    // Save original settings
        LowObstacleRate = thePlatformGenerator.randomLowObstacleThreshold;  // Save original settings

        powerupActive = true;       // set power up to be true


    }
}
