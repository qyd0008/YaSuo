using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSwordSMB : StateMachineBehaviour
{
    public AudioClip yaSuoAudio;
    public AudioClip swordAudio;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (yaSuoAudio)
            AudioManager.Instance.PlayYaSuoSound(yaSuoAudio); 
        if (swordAudio)
            AudioManager.Instance.PlaySwordSound(swordAudio); 
        PlayerController ctrl = animator.GetComponent<PlayerController>();
        ctrl.DrawSword();//拔剑
        ctrl.NotReady2CloseSword();//不打算收剑
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 如果攻击动画被打断了 那么准备收剑
        // FixMe:并且不再调用攻击、技能的动画
        if (stateInfo.normalizedTime < 1f)
        {
            PlayerController ctrl = animator.GetComponent<PlayerController>();
            ctrl.Ready2CloseSword();//打算收剑
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
