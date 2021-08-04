using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleAnimations : MonoBehaviour
{

    Transform body;

    float idleTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        body = this.transform;
        idleTime = Random.Range(0f, 10f);

        for(int a = 1; a < transform.childCount; a++)
        {
            transform.GetChild(a).gameObject.SetActive(false);
        }

        int enable = Mathf.RoundToInt(Random.Range(1, transform.childCount));
        transform.GetChild(enable).gameObject.SetActive(true);
    }

    float currentRotation = 0f;
    float desiredRotation = 0f;

    float counter = 0f;
    bool achievedCompletion = true;

    // Update is called once per frame
    void Update()
    {
        if(achievedCompletion)
        {
            counter += Time.deltaTime;
        }

        if(counter > 5f + idleTime)
        {
            desiredRotation = Random.Range(-30f, 30f);
            counter = 0f;
            idleTime = Random.Range(0f, 10f);
        }

        if(Mathf.Abs(currentRotation) < Mathf.Abs(desiredRotation))
        {
            int direction = currentRotation < desiredRotation ? 1 : -1;
            currentRotation += 5f * direction * Time.deltaTime;
            body.Rotate(new Vector3(0f, 5f * direction * Time.deltaTime, 0f));
        }
        else
        {
            achievedCompletion = true;
        }

    }
}
