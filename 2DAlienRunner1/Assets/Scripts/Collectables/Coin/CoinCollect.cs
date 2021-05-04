using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{

    private AudioManager audioManager;
    public string coinSoundName;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect()
    {
        Debug.Log("Coin collected");
        audioManager.PlaySound(coinSoundName);
        

    }
}
