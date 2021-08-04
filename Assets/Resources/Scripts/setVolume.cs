using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setVolume : MonoBehaviour
{

    public float gainAmount = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<AudioSource>().volume = Mathf.Clamp(volumeScript.volume + gainAmount, 0f, 1f);
    }
}
