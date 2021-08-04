using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float blinkTime = 0f;
    public bool rightBlinker = false;
    bool held = false;

    // Update is called once per frame
    void Update()
    {
        blinkTime += Time.deltaTime;

        if (blinkTime > 0.321f)
        {
            if(GetComponent<AudioSource>().enabled)
            {
                GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            }
            blinkTime = 0f;
        }

        float button = 0f;
        if(rightBlinker)
        {
            button = Input.GetAxis("RightBlinker");
        }
        else
        {
            button = Input.GetAxis("LeftBlinker");
        }

        if(button != 0)
        {
            if(!held)
            {
                GetComponent<AudioSource>().enabled = !GetComponent<AudioSource>().enabled;
                GetComponent<Light>().enabled = GetComponent<AudioSource>().enabled;
                blinkTime = 0f;
                held = true;
            }
        }
        else
        {
            held = false;
        }
    }

    public void setBlinkerOff()
    {
        GetComponent<AudioSource>().enabled = false;
        GetComponent<Light>().enabled = GetComponent<AudioSource>().enabled;
        blinkTime = 0f;
    }

    public void setBlinkerOn()
    {
        GetComponent<AudioSource>().enabled = true;
        GetComponent<Light>().enabled = GetComponent<AudioSource>().enabled;
        blinkTime = 0f;
    }
}
