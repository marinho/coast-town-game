using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public float alpha = .6f;
    [SerializeField] private Gradient lightColor;
    [SerializeField] private GameObject light;

    public void UpdateCurrentTime(int timestamp)
    {
        var t = new TimeStructure(timestamp);
        var lightRate = t.hours / 24;
        var newLightColor = lightColor.Evaluate(lightRate);
        newLightColor.a = alpha;
        light.GetComponent<Image>().color = newLightColor;
        //light.GetComponent<Light2D>().color = lightColor.Evaluate(lightRate);
    }
}
