using UnityEngine;

public class ResetAnimatorBool : StateMachineBehaviour
{
    /// <summary>
    /// Targeted parameter 
    /// </summary>
    public string targetBool;

    public bool status;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(targetBool, status);
    }
}
