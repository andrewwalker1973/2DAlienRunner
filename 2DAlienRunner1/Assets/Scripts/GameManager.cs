using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform platformGenerator;             // needs the platform generator game aboject
    private Vector3 platformStartPoint;             // Where to start creating platforms

    public PlayerController thePlayer;              // reference to player object
    private Vector3 playerStartPoint;               // where does the player start

    private PlatformDestroyer[] platformList;       // Create an array of platforms to disable when restarting the game

    private ScoreManager theScoreManager;           // reference the scoremanager script
    // Start is called before the first frame update
    void Start()
    {

        platformStartPoint = platformGenerator.position;            // set the platform startpoint
        playerStartPoint = thePlayer.transform.position;             // set the player start point
        theScoreManager = FindObjectOfType<ScoreManager>();         // find the score Manager script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()           // function to be called from other scripts to restart the game
    {
        StartCoroutine("RestartGameCo");
    }

    public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;         // stop increasing score
        thePlayer.gameObject.SetActive(false);              // disable the player
        yield return new WaitForSeconds(1f);                    // wait 1 sec
        platformList = FindObjectsOfType<PlatformDestroyer>();      // generate a list of all active platfomrs and disable
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;            // reset player to start point
        platformGenerator.position = platformStartPoint;            // reset platfomr generation to start point
        thePlayer.gameObject.SetActive(true);                       // re-enable the player
        theScoreManager.scoreCount = 0;                             // reset score back to 0
        theScoreManager.scoreIncreasing = true;                     // let score being increasing again


    }
}
