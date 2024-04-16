using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script attaché à un élément healthbar pour le contrôler
/// </summary>
public class PlayerResourceScript : MonoBehaviour
{

    // [SerializeField] public GameObject sandIcon, waterIcon;
    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Slider useableSlider;
    [SerializeField]
    private Image useableFill;

    [SerializeField]
    private TextMeshProUGUI resourceUi;

    void Start() 
    {
        // bool IsSandman = transform.GetChild(0).gameObject.activeInHierarchy;
        // if (IsSandman) 
        // {
        //     sandIcon.SetActive(true);
        // } else {
        //     waterIcon.SetActive(true);
        // }
    }

    public void ToggleResourceText(string text, bool isShowing)
    {
        resourceUi.text = text;
        resourceUi.gameObject.SetActive(isShowing);
    }

    public void SetMaxHealth(int health)
    {
        Debug.Log("Setting max health to " + health);
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        Debug.Log("Setting health to " + health);
        healthSlider.value = health;
    }

    public void SetMaxUseable(int resource)
    {
        Debug.Log("Setting max useable to " + resource);
        useableSlider.maxValue = resource;
        useableSlider.value = resource;
    }

    public void SetUseable(int resource)
    {
        Debug.Log("Setting useable to " + resource);
        useableSlider.value = resource;
    }
}
