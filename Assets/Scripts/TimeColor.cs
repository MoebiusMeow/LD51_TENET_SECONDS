using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeColor : MonoBehaviour
{
    public TimeInverse localTime;
    public SpriteRenderer spriteRenderer;
    public TrailRenderer trailRenderer;
    public Light2D light2D;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer)
        {
            spriteRenderer.color = Color.HSVToRGB((localTime.timeDirection == 1 ? 16 : 224) / 360f, 1, 1);
        }
        if (trailRenderer)
        {
            trailRenderer.startColor = Color.HSVToRGB((localTime.timeDirection == 1 ? 16 : 224) / 360f, 1, 1);
            trailRenderer.endColor = Color.HSVToRGB((localTime.timeDirection == 1 ? 16 : 224) / 360f, 1, 1);
        }
        if (light2D)
        {
            light2D.color = Color.HSVToRGB((localTime.timeDirection == 1 ? 16 : 224) / 360f, 1, 1);
        }
    }
}
