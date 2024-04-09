using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script pour set le ColorScheme color blind ou non
/// </summary>
public class AccessibilityManager : MonoBehaviour
{
    public static readonly int COLOR_BLIND_MODE = 1;
    public static readonly int NORMAL_VISION_MODE = 0;

    public static readonly string Key = "Accessbility";

    [SerializeField]
    private Image accessibilityButtonImage;

    public void ToggleColorblindMode()
    {
        var currentMode = PlayerPrefs.GetInt(Key);
        Debug.Log("Current mode: " + currentMode);
        var nextMode = currentMode == NORMAL_VISION_MODE ? COLOR_BLIND_MODE : NORMAL_VISION_MODE;
        PlayerPrefs.SetInt(Key, nextMode);

        accessibilityButtonImage.gameObject.SetActive(!accessibilityButtonImage.gameObject.activeInHierarchy);
    }
}
