using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    public int scoreToGive;     // what point value to give

    private ScoreManager theScoreManager;

   // private AudioSource coinSound;
    
  
    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
   //     coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Player"))       // if hit the player
        {
            theScoreManager.AddScore(scoreToGive);          // send point value to scoreManger
            gameObject.SetActive(false);

      /*      if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
      */
        }
    }
}
