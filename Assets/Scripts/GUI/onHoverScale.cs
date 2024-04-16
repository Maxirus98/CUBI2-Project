using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scaleFactor = 1.2f; // Factor by which the UI element scales up on hover
    public float animationDuration = 0.2f; // Duration of the scaling animation

    private Vector3 initialScale;
    private Vector3 targetScale;
    private bool isHovered = false;
    private float timer = 0f;
    private Graphic graphic;

    void Start()
    {
        initialScale = transform.localScale;
        targetScale = initialScale * scaleFactor;
        graphic = GetComponent<Graphic>();
    }

    void Update()
    {
        if (isHovered && graphic != null && graphic.canvasRenderer.GetAlpha() > 0f)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / animationDuration);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, progress);
        }
        else if (!isHovered && graphic != null && graphic.canvasRenderer.GetAlpha() > 0f)
        {
            timer += Time.deltaTime;
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
