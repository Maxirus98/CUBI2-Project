using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script attaché à un élément healthbar pour le contrôler
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;

    public void SetMaxHealth(int health)
    {
        Debug.Log("Setting max health to " + health);
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        Debug.Log("Setting health to " + health);
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
