using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassGenerator : MonoBehaviour
{
    public GameObject cactus;

    // Start is called before the first frame update
    void Start()
    {

        //add some cactoids
        //1/5 to add cactoid
        while (Random.Range(0f, 1f) > 0.6f)
        {
            GameObject newCactoid = Instantiate(cactus, this.transform);
            // even distribution off of the road
            float cactoidX = Random.Range(0f, 1f) >= 0.5f ? Random.Range(-50f, -5.5f) : Random.Range(5.5f, 50f);
            // even distribution along y
            //float cactoidY = Random.Range(0, 0.5f);
            float cactoidY = 0.3f;
            // even distribution along the Z
            float cactoidZ = this.transform.position.z + Random.Range(-5f, 5f);
            newCactoid.transform.position = new Vector3(cactoidX, cactoidY, cactoidZ);

            float randomScale = Random.Range(1f, 5f);

            newCactoid.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            newCactoid.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f,360f), 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
