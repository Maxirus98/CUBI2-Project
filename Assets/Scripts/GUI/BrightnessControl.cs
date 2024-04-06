using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class brightnessControl : MonoBehaviour
{
    float rgbValue = 0.5f;
    [SerializeField] Slider brightnessSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void adjustBrightness() 
    {
        rgbValue = brightnessSlider.value;
        RenderSettings.ambientLight = new Color(rgbValue, rgbValue, rgbValue,1);
    }

}
