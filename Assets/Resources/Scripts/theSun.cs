using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class theSun : MonoBehaviour
{
    public Light sun;
    public GameObject clock;
    public float secondsInFullDay = 120f;
    [Range(0, 1)]
    public float currentTimeOfDay = 0.33f;
    [HideInInspector]
    public float timeMultiplier = 1f;

    float sunInitialIntensity;

    void Start()
    {
        sunInitialIntensity = sun.intensity;
    }

    void Update()
    {
        UpdateSun();

        Text clockText = clock.GetComponentInChildren<Text>();
        

        int hour = (int)Mathf.Floor(currentTimeOfDay * 24f);
        int minute = (int)Mathf.Floor(currentTimeOfDay * 1440f % 60f);

        clockText.text = string.Format("{0:00}",hour) + ":" + string.Format("{0:00}", minute);

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if (currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
        }
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.AngleAxis((currentTimeOfDay * 360f), Vector3.up);

        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * intensityMultiplier;

        RenderSettings.ambientIntensity = sun.intensity;

        RenderSettings.skybox.SetFloat("_Exposure", (0.5f - Mathf.Abs(0.5f - currentTimeOfDay )) * 1.53f + 0.1f);
    }
}
