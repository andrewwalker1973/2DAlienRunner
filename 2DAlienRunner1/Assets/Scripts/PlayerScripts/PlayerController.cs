using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Jump Settings
    [SerializeField] private float jumpForce;
    public bool isOnGround;
 

    // speed Modifier
    private float originalSpeed = 9.0f;
    [SerializeField]  private float speed = 9f;

    // Bring in other references
    private Rigidbody playerRb;
    private BoxCollider playerBoxCollider;

    //Slide Settings
    private Vector3 slideColliderSizeRestore = new Vector3(1, 2, 1);
    private Vector3 slideColliderCenterRestore = new Vector3(0, 0, 0);
    public bool isSliding = false;





    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;

        // Get Components off Player object
        playerRb = GetComponent<Rigidbody>();
        playerBoxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
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
        playerRb.velocity = new Vector3(speed, playerRb.velocity.y);


        
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
        }
        if (collision.gameObject.CompareTag("Enemy"))  // If hit Enemy
        {
            if (isSliding == true)                  // If the Player is sliding, they kick the feet out from under enemy and they die
            {
                Debug.Log("Killed Enemy");
                Destroy(collision.gameObject);      // Code to destroy the object that we collided with
            }
            else
            {
                Debug.Log("Hit Enemy");
                // AW DIE process
            }
        }
    }



    #region Slide and Jump functions
    private void StartSliding()
    {
        // Code executed when the player slides
        
        // anim.SetBool("Sliding", true);
        isSliding = true;
        playerBoxCollider.size -= new Vector3(0 , playerBoxCollider.size.y / 2, 0);
        playerBoxCollider.center -= new Vector3(0, playerBoxCollider.size.y / 2, 0);
        Invoke("StopSliding", 1.0f);
    }
    private void StopSliding()
    {
        // Code executed when the player stops sliding
        //  anim.SetBool("Sliding", false);
        isSliding = false;
        playerBoxCollider.size = slideColliderSizeRestore;
        playerBoxCollider.center = slideColliderCenterRestore;
    }

    private void StartJump()
    {
        // Code executed when the player jumps
        //anim.SetTrigger("Jump");
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }
    #endregion
}
