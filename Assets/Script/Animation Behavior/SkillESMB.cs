using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillESMB : StateMachineBehaviour
{
    public List<AudioClip> eList;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.Instance.PlayRandomYaSuoSound(eList);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetKeyUp("q"))
        {
            // Debug.Log("E的时候按下了Q");
            //要判断现在Q在不在冷却期间
            SkillController ctrl = animator.GetComponent<SkillController>();
            Dictionary<SkillType,Skill> skillTable = ctrl.GetSkillTable();
            if (skillTable[SkillType.Q].ColdPassedTime <= 0)
            {
                animator.SetTrigger("Skill EQ");
                skillTable[SkillType.Q].ColdPassedTime = skillTable[SkillType.Q].ColdTime;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
