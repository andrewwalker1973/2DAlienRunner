using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Jump Settings
    [SerializeField] private float jumpForce;               // How much force to apply to jump
    public bool isOnGround;                                 // bool to check if grounded
 


    // speed Modifier
    [SerializeField]  private float movespeed = 9f;         // Start MoveSpeed
    private float moveSpeedStore;                           // store of start speed to be used when restarting
    private float speedMilestoneCountStore;                 // store of start milestone to be used when restarting
    public float speedMultiplier;                           // how much to multiple spped by yo increase
    private float speedMilestoneCount;                      // Milestone value for first speed increase
    public float speedIncreaseMilestone;                    // How much to increase the distance between milestones
    private float speedIncreaseMilestoneStore;              // store of initial milestore to be used when restarting



    // Bring in other references
    private Rigidbody playerRb;
    private BoxCollider playerBoxCollider;
    


    //Slide Settings
    private Vector3 slideColliderSizeRestore = new Vector3(1, 2, 1);            // Collider settings for when sliding
    private Vector3 slideColliderCenterRestore = new Vector3(0, 0, 0);          // Collider settings for when sliding
    public bool isSliding = false;                                 // Sliding true/false

    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions

  //  public AudioSource deathSound;
  //  public AudioSource jumpSound;

    
    void Start()
    {
       

        // Get Components off Player object
        playerRb = GetComponent<Rigidbody>();
        playerBoxCollider = GetComponent<BoxCollider>();
        



        // Save settings to reset when restarting game
        speedMilestoneCount = speedIncreaseMilestone;                   
        moveSpeedStore = movespeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
        
    }

   
    void Update()
    {

        #region Inputs for Player
        // Code to manage mobile and keyboard inputs
        if (MobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log(" Go Left");
        }
        if (MobileInput.Instance.SwipeRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(" Go Right");
        }

        if (MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
        {
            StartJump();
        }
        else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
        {
            // AW maybe check to make sure we are on ground, cant slide in the air ?
            StartSliding();
        }

        #endregion

        // Code to move the player to the right at spped value
        if (transform.position.x > speedMilestoneCount)                     // if > milestone increase speed
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            movespeed = movespeed * speedMultiplier;
        }

        playerRb.velocity = new Vector3(movespeed, playerRb.velocity.y);        // Constanlty move to the right


        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))  // if Hit Ground then we are grounded
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Obstacle")) //If hit Obstacle process
        {
            Debug.Log("Hit Obstacle");
          //  deathSound.Play();
            theGameManager.RestartGame();       // AW want pause and choose to continue later
        }
        // AW add below in here somewher
        /*
         * Coin coin = other.GetComponent<Coin>();
          //  Debug.Log("Coin Collide");
          coin.Collect();

        */

          if (collision.gameObject.CompareTag("Enemy"))  // If hit Enemy
          {
              if (isSliding == true)                      // If the Player is sliding, they kick the feet out from under enemy and they die
              {
                  Debug.Log("Killed Enemy");
                  Destroy(collision.gameObject);          // Code to destroy the object that we collided with
              }
              else
              {
                  Debug.Log("Hit Enemy");
                //  deathSound.Play();
                  theGameManager.RestartGame();  // AW want pause and choose to continue later
                  movespeed = moveSpeedStore;     //Reset back to starting game speed
                  speedMilestoneCount = speedMilestoneCountStore;  //Reset back to starting game speed increase
                  speedIncreaseMilestone = speedIncreaseMilestoneStore; //Reset back to starting game spped milestone
              }
          }
      }



      #region Slide and Jump functions
      private void StartSliding()
      {
          // Code executed when the player slides

          // anim.SetBool("Sliding", true);
          isSliding = true;
          playerBoxCollider.size -= new Vector3(0 , playerBoxCollider.size.y / 2, 0);     // shrink collider 
          playerBoxCollider.center -= new Vector3(0, playerBoxCollider.size.y / 2, 0);    // shrink collider 
          Invoke("StopSliding", 1.0f);
      }
      private void StopSliding()
      {
          // Code executed when the player stops sliding
          //  anim.SetBool("Sliding", false);
          isSliding = false;
          playerBoxCollider.size = slideColliderSizeRestore;                  // Restore collider
          playerBoxCollider.center = slideColliderCenterRestore;              // Restore collider
      }

      private void StartJump()
      {
          // Code executed when the player jumps
          //anim.SetTrigger("Jump");
          playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);       // Actual jump using physics to jump
          isOnGround = false;
         // jumpSound.Play();
      }
      #endregion

    /*  private void CoinCollision(Collider other)
      {
          Coin coin = other.GetComponent<Coin>();
          //  Debug.Log("Coin Collide");
          coin.Collect();

      }
    */
    }
