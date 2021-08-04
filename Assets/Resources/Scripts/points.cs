using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class points : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //other.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        //other.transform.position += new Vector3(0f, 0f, 10f);
        // disable convention center from giving additional points
        //other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.SendMessage("addPoints", transform.parent.name);
    }
}
