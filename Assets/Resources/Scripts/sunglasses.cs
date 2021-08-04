using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunglasses : MonoBehaviour
{

    public GameObject sunglassMesh;

    // Start is called before the first frame update
    void Start()
    {
        if(Random.Range(0f, 1000f) > 1f)
        {
            sunglassMesh.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
