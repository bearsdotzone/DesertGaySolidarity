using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stallScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<driving>().addTrigger(1);
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<driving>().addStallTime(Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<driving>().addTrigger(-1);
    }
}
