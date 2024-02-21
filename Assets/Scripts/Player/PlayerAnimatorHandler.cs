using UnityEngine;

// TODO: Unused for now
/// <summary>
/// Script pour Clamp les valeurs du blend des mouvements et pour animer programmatiquement par nom
/// </summary>
public class PlayerAnimatorHandler : MonoBehaviour
{
    public Animator Anim;
    private int vertical;
    private int horizontal;

    public void Initialize()
    {
        Anim = GetComponent<Animator>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    /// <summary>
    /// Méthode pour attribuer les valeurs aux propriétés d'animation de mouvement du joueur
    /// </summary>
    /// <param name="verticlalMovement"></param>
    /// <param name="horizontalMovement"></param>
    /// <param name="isSprinting"></param>
    public void UpdateAnimatorValues(float verticlalMovement, float horizontalMovement, bool isSprinting)
    {
        float v;
        float h;
        ClampMovement(out v, verticlalMovement);
        ClampMovement(out h, horizontalMovement);

        if (isSprinting)
        {
            v = 2;
            h = horizontalMovement;
        }

        // Valeurs du BlendTree de mouvements
        Anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        Anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimationByName(string targetAnim, bool isInteracting)
    {
        Anim.applyRootMotion = isInteracting;
        Anim.SetBool("isInteracting", isInteracting);
        Anim.CrossFade(targetAnim, 0.2f);
    }

    private void ClampMovement(out float direction, float movement)
    {
        if (movement > 0f && movement < -0.55f)
        {
            direction = 0.5f;
        }
        else if (movement > 0.55f)
        {
            direction = 1f;
        }
        else if (movement < 0f && movement > -0.55f)
        {
            direction = -0.5f;
        }
        else if (movement < -0.55f)
        {
            direction = -1f;
        }
        else
        {
            direction = 0f;
        }
    }
}
