using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostWall : MonoBehaviour
{

    public bool rightWall = false;

    private void OnTriggerEnter(Collider other)
    {
        int modifier = 1;

        if(rightWall)
        {
            modifier *= -1;
        }

        if (modifier == 1)
        {
            //other.attachedRigidbody.velocity = new Vector3(Mathf.Clamp(other.attachedRigidbody.velocity.x, 0f, 30f), other.attachedRigidbody.velocity.y, other.attachedRigidbody.velocity.z);
        }
        else
        {
            //other.attachedRigidbody.velocity = new Vector3(Mathf.Clamp(other.attachedRigidbody.velocity.x, -30f, 0f), other.attachedRigidbody.velocity.y, other.attachedRigidbody.velocity.z);
        }
    }
}
