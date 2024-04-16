using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f; // Factor by which the button scales up on hover
    public float animationDuration = 0.2f; // Duration of the scaling animation

    private Vector3 initialScale;
    private Vector3 targetScale;
    private bool isHovered = false;
    private float timer = 0f;
    private Button button;

    void Start()
    {
        // Get the initial scale of the button
        initialScale = transform.localScale;
        // Calculate the target scale for scaling up
        targetScale = initialScale * scaleFactor;
        // Get the button component
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (isHovered && button.interactable)
        {
            // Increment the timer
            timer += Time.deltaTime;
            // Calculate the scale based on the animation progress
            float progress = Mathf.Clamp01(timer / animationDuration);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
        }
        else if (!isHovered && button.interactable)
        {
            // Increment the timer
            timer += Time.deltaTime;
            // Calculate the scale based on the animation progress
            float progress = Mathf.Clamp01(timer / animationDuration);
            transform.localScale = Vector3.Lerp(targetScale, initialScale, progress);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        timer = 0f; // Reset timer
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        timer = 0f; // Reset timer
    }
}
