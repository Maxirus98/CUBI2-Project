using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAccessibilityHandler : MonoBehaviour
{
    [SerializeField]
    private Color normalVisionColor;

    [SerializeField]
    private Color colorAcc;
    
    private void Start()
    {
        SetColorToCurrentColorScheme();
    }

    private void SetColorToCurrentColorScheme()
    {
        var particleSystem = GetComponent<ParticleSystem>();
        var isNormalVision = PlayerPrefs.GetInt(AccessibilityManager.Key) == AccessibilityManager.NORMAL_VISION_MODE;

        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.startColor = isNormalVision ? normalVisionColor : colorAcc;
        }

        var trailRenderer = GetComponent<TrailRenderer>();
        if(trailRenderer != null)
        {
            trailRenderer.startColor = isNormalVision ? normalVisionColor: colorAcc;
            trailRenderer.endColor = isNormalVision ? Color.yellow : Color.white;
        }
    }
        
}
