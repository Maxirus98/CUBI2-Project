using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script attaché à un élément healthbar pour le contrôler
/// </summary>
public class PlayerResourceScript : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Gradient healthGradient;
    [SerializeField]
    private Image fill;

    [SerializeField]
    private Slider useableSlider;
    [SerializeField]
    private Image useableFill;

    public void SetMaxHealth(int health)
    {
        Debug.Log("Setting max health to " + health);
        healthSlider.maxValue = health;
        healthSlider.value = health;

        fill.color = healthGradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        Debug.Log("Setting health to " + health);
        healthSlider.value = health;
        fill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void SetMaxUseable(int resource)
    {
        Debug.Log("Setting max useable to " + resource);
        useableSlider.maxValue = resource;
        useableSlider.value = resource / 2;
    }

    public void SetUseable(int resource)
    {
        Debug.Log("Setting useable to " + resource);
        useableSlider.value = resource;
    }
}
