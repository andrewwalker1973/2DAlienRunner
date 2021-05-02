using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;      // The platform game object array
    public Transform generationPoint;   // where to generate the platform
    private float distanceBetween;      // distance between the platforms
    private float platformWidth;        // the Width of the platform

    //Variables for Randomize the distance between platfroms
    public float distanceBetweenMin;        // Min distance between the platforms
    public float distanceBetweenMax;        // Max distance between the platforms
    private float minHeight;                // Min height from bottom of screen
    public Transform maxHeightPoint;        // Max height of gameobject on screen
    private float maxHeight;                
    public float maxHeightChange;           // how much can we increase in height
    private float heightChange;

   // public GameObject[] thePlatforms;
    private int platformSelector;           // int to number the platforms

    public float[] platformWidths;          // array to manage the widths of the platforms 

    public ObjectPooler[] theObjectPools;      // Refernce the object pooler script

    private CoinGenerator theCoinGenerator;      // reference the coin genertion script
    public float randomCoinThreshold;           // Randomize if coin appears
  
    void Start()
    {


        
        platformWidths = new float[theObjectPools.Length];      // create array with all the platfomr widths for the platforms chosen
        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.transform.localScale.x;
        }

        minHeight = transform.position.y;               // set min height to be the height of the current platfomr in y
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
    }

   
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)          // if current point less than gen point on camera, create a platform
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);     // randomize the distance between platforms

            platformSelector = Random.Range(0, theObjectPools.Length);              // Randomize which platfomr to select

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);      // randomize the height to be chnaged
           
            // Code to ensure we dont go to high or too low off screen
            if (heightChange > maxHeight )
            {
                heightChange = maxHeight;
            }
            else if(heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);  // Determine the position to spawn new platform

            
            
         //   Instantiate(/*thePlatform*/  thePlatforms[platformSelector], transform.position, transform.rotation);           // Create the actual platform piece in the game world
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();                        // run the function in the ObjectPool script called GetpooledObject to find the next game object and make it a game object
            newPlatform.transform.position = transform.position;                            // set the new platforms position
            newPlatform.transform.rotation = transform.rotation;                            // Set the new platforms rotation
            newPlatform.SetActive(true);                                                    // Set it active in the game

            if (Random.Range(0f, 100f) < randomCoinThreshold)        // if random value below threshold spawn a coin set
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }
           

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);  // Determine the position to spawn new platform

        }
    }
}
