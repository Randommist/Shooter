using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Networking;

public class Sun : MonoBehaviour
{
    public Light2D Солнце;
    public float SpeedSunriseSunset = 1f;
    public float Minintensity = 0.1f;
    public float TimeSpeed = 5;

    public int Hours;
    public float Minutes;

    // Update is called once per frame
    void Update()
    {
        // Часики идут
        Minutes += TimeSpeed * Time.deltaTime;
        if (Minutes >= 60)
        {
            Minutes = 0;
            Hours++;
            if (Hours >= 24)
            {
                Hours = 0;
            }
        }
        // Даже назад
        if (Minutes < 0)
        {
            Minutes = 60;
            Hours--;
            if (Hours < 0)
            {
                Hours = 24;
            }
        }

        if (Hours >= 22 || Hours < 8)
        {
            Солнце.intensity = Mathf.Lerp(Солнце.intensity, Minintensity, SpeedSunriseSunset * Time.deltaTime);
        }
        else
        {
            Солнце.intensity = Mathf.Lerp(Солнце.intensity, 1, SpeedSunriseSunset * Time.deltaTime);
        }
    }
}
