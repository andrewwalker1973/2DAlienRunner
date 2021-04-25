using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Jump Settings
    [SerializeField] private float jumpForce = 370f;

    // speed Modifier
    private float originalSpeed = 9.0f;
    [SerializeField]  private float speed = 9f;


    //Environment Settings
    public bool isOnGround = true;

    // Bring in other references
    private Rigidbody playerRb;
    private BoxCollider playerBoxCollider;

    // Movement settings
    //Vector3 moveRight;



    // Start is called before the first frame update
    void Start()
    {
        speed = originalSpeed;

        // Get Components off Player 
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
            StartSliding();
        }

        #endregion
        playerRb.velocity = new Vector3(speed, playerRb.velocity.y);


        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit Obstacle");
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
        }
    }



    #region Slide and Jump functions
    private void StartSliding()
    {
        // anim.SetBool("Sliding", true);
           playerBoxCollider.size -= new Vector3(0 , playerBoxCollider.size.y / 2, 0);
          playerBoxCollider.center -= new Vector3(0, playerBoxCollider.size.y / 2, 0);
          Invoke("StopSliding", 1.0f);
    }
    private void StopSliding()
    {
        //  anim.SetBool("Sliding", false);
        playerBoxCollider.size = new Vector3(1, 2, 1);
        playerBoxCollider.center = new Vector3(0, 0, 0);
    }

    private void StartJump()
    {
        //anim.SetTrigger("Jump");
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
    }
    #endregion
}
