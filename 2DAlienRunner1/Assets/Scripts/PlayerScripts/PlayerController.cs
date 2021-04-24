using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Jump Settings
    [SerializeField] private float jumpForce = 16f;
    private float gravity = 12f;
    private float verticalVelocity;

    // speed Modifier
    private float originalSpeed = 9.0f;
    private float speed = 0.5f;
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    //Environment Settings
    public bool isOnGround = true;

    // Bring in other references
    private Rigidbody playerRb;
    private SphereCollider groundCollider;

    // Movement settings
    Vector3 moveRight;



    // Start is called before the first frame update
    void Start()
    {
       // speed = originalSpeed;

        // Get Components
        playerRb = GetComponent<Rigidbody>();
        groundCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        //Code to increase speed over time
      //  if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
     //   {
     //       speedIncreaseLastTick = Time.time;
     //       speed += speedIncreaseAmount;

            // change modifer text display
            //   GameManager.Instance.UpdateModifer(speed - originalSpeed);
     //   }




        // Code to manage mobile and keyboard inputs
        // gather the inputs on which lane we should be in

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
            //Jump
            //anim.SetTrigger("Jump");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            Debug.Log(" Jump");
        }
        else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
        {

            // StartSliding();
            Debug.Log(" Slide");
        }



        moveRight = gameObject.transform.position;    //When the game starts it will start to go to the right
        moveRight.x -= speed;
        gameObject.transform.position = moveRight;


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"));
        {
            isOnGround = true;
        }

    }
}
