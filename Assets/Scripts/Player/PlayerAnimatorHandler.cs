using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode.Components;
using UnityEngine;

/// <summary>
/// Script pour Clamp les valeurs du blend des mouvements et pour animer programmatiquement par nom
/// </summary>
public class PlayerAnimatorHandler : MonoBehaviour
{
    public Animator Anim;

    public void Initialize()
    {
        Anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Méthode pour attribuer les valeurs aux propriétés d'animation de mouvement du joueur
    /// </summary>
    /// <param name="moveAmount"></param>
    public void UpdateAnimatorValues(float moveAmount)
    {
        float v = moveAmount > 0f ? 1f: 0f;

        // Valeurs du BlendTree de mouvements
        Anim.SetFloat("Movement", v, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimationByName(string targetAnim)
    {
        Anim.CrossFade(targetAnim, 0.2f);
        Anim.SetBool("isAttacking", true);
    }

    public void Hit()
    {
        Anim.SetTrigger("Hit");
    }

    public bool IsShooting()
    {
        return Anim.GetBool("isAttacking");
    }

    private void ClampMovement(out float direction, float movement)
    {
        if (movement > 0f && movement < -0.1f)
        {
            direction = 1f;
        }
        else if (movement > 0.1f)
        {
            direction = 1f;
        }
        else if (movement < 0f && movement > -0.1f)
        {
            direction = -1f;
        }
        else if (movement < -0.1f)
        {
            direction = -1f;
        }
        else
        {
            direction = 0f;
        }
    }
}
