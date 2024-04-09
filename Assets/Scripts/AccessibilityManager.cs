using UnityEngine;

/// <summary>
/// Script pour set le ColorScheme color blind ou non
/// </summary>
public class AccessibilityManager : MonoBehaviour
{
    public static readonly int COLOR_BLIND_MODE = 1;
    public static readonly int NORMAL_VISION_MODE = 0;

    public static readonly string Key = "Accessbility";

    public void ToNormalMode()
    {
        PlayerPrefs.SetInt(Key, NORMAL_VISION_MODE);
    }

    public void ToColorBlind()
    {
        PlayerPrefs.SetInt(Key, COLOR_BLIND_MODE);
    }
}
