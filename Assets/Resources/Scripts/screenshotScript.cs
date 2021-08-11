using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class screenshotScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float timerino = 0f;
    bool screenShotted = false;

    // Update is called once per frame
    void Update()
    {
        #if (UNITY_EDITOR)
        timerino += Time.deltaTime;

        if (timerino > 15f && !screenShotted)
        {
            string screenshotsFolder = "C:/Users/bears/Desktop/screeny/";

            string screenshotName = "Screenshot_" + System.IO.Directory.GetFiles(screenshotsFolder).Length + ".png";

            ScreenCapture.CaptureScreenshot(screenshotsFolder + screenshotName);

            screenShotted = true;
        }
        #endif
    }
}
