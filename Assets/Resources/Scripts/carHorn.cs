using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carHorn : MonoBehaviour
{

    public GameObject hornSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool wait = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horn") != 0)
        {
            if(!wait)
            {
                Instantiate(hornSound, transform);
                wait = true;
            }
        }
        else
        {
            wait = false;
        }
    }
}
