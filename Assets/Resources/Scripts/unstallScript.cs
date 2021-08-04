using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unstallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.GetComponent<driving>().stall && int.Parse(transform.parent.name) > 1)
        {
            collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        collider.gameObject.GetComponent<driving>().setStall(false);
    }
}
