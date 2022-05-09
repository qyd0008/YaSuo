using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillQSMB : StateMachineBehaviour
{
    public List<AudioClip> q12List;
    public List<AudioClip> q3List;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SkillController ctrl = animator.GetComponent<SkillController>();
        float qType = animator.GetFloat("Skill Q Type");
        switch ((SkillQType)qType)
        {
            case SkillQType.Q1:
                AudioManager.Instance.PlayRandomYaSuoSound(q12List);
                break;
            case SkillQType.Q2:
                AudioManager.Instance.PlayRandomYaSuoSound(q12List);
                break;
            case SkillQType.Q3:
                AudioManager.Instance.PlayRandomYaSuoSound(q3List);
                ctrl.HideQ2WindEffect();
                break;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SkillController ctrl = animator.GetComponent<SkillController>();
        ctrl.useQSkill();
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