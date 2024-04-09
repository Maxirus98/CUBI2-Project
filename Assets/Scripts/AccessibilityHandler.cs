using UnityEngine;

/// <summary>
/// Script sur les objets qui ont besoin de changer de couleurs selon le mode d'accessibilite
/// </summary>
public class AccessibilityHandler : MonoBehaviour
{
    [SerializeField]
    private Material colorBlindMat;
    [SerializeField]
    private Material normalVisionMat;

    private void Start()
    {
        SetColorToCurrentColorScheme();
    }

    private void SetColorToCurrentColorScheme()
    {
        var skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        var meshRenderer = GetComponent<MeshRenderer>();
        var isNormalVision = PlayerPrefs.GetInt(AccessibilityManager.Key) == AccessibilityManager.NORMAL_VISION_MODE;

        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.material = isNormalVision ? normalVisionMat : colorBlindMat;
        }

        if (meshRenderer != null)
        {
            meshRenderer.material = isNormalVision ? normalVisionMat : colorBlindMat;
        }
    }
}
