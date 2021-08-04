using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headlights : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RenderSettings.ambientIntensity < 0.40f)
        {
            GetComponent<Light>().enabled = true;
        }
        else
        {
            GetComponent<Light>().enabled = false;
        }
    }
}
