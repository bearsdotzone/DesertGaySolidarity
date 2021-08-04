using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioLevelScroller : MonoBehaviour
{

    Text volumeText;
    string baseText = "VOLUME -- ";

    // Start is called before the first frame update
    void Start()
    {
        volumeText = GetComponent<Text>();
    }

    float scrollTime = 0f;

    // Update is called once per frame
    void Update()
    {
        string newText = string.Format(baseText + "{0:00%} ", volumeScript.volume);
        scrollTime += Time.deltaTime;

        if (scrollTime > newText.Length)
        {
            scrollTime = 0f;
        }

        float roughTime = Mathf.Floor(scrollTime);

        newText = newText.Substring((int)roughTime) + newText.Substring(0, (int)roughTime);

        volumeText.text = newText;
    }
}
