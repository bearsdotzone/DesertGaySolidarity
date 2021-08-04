using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeScript : MonoBehaviour
{

    public static float volume = 0.10f;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        volume = PlayerPrefs.GetFloat("MusicVolume", 0.10f);
    }

    float lastAngle = 0f;
    bool reset = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("VolumeScroll") != 0)
        {
            volume += Input.GetAxis("VolumeScroll") * Time.deltaTime;
            volume = Mathf.Clamp((float)volume, 0f, 1f);
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        float angle = Mathf.Atan2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical")) * Mathf.Rad2Deg;

        if(Vector2.SqrMagnitude(new Vector2(Input.GetAxis("RightStickHorizontal"), Input.GetAxis("RightStickVertical"))) >= 1f)
        {
            if (Input.GetAxis("RightStickHorizontal") < 0)
            {
                angle = angle + 360f;
            }

            if (!reset)
            {
                if(!((lastAngle - angle) > 100f) && !((lastAngle - angle) < -100f))
                {
                    volume -= (lastAngle - angle) * Time.deltaTime * 0.08f;
                    volume = Mathf.Clamp((float)volume, 0f, 1f);
                    PlayerPrefs.SetFloat("MusicVolume", volume);
                }
            }

            lastAngle = angle;
            reset = false;
        }
        else
        {
            reset = true;
        }
    }
}
