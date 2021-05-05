using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    public int scoreToGive;     // what point value to give

    private ScoreManager theScoreManager;       // reference the score manager


    
  
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script

    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))       // if hit the player
        {
            theScoreManager.AddScore(scoreToGive);          // send point value to scoreManger
            gameObject.SetActive(false);                    // set game object disable

     
        }
    }
}
