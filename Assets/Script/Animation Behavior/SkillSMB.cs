using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillSMB : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        InputManager.Instance.SetisKeyQWER(true);
        PlayerController ctrl = animator.GetComponent<PlayerController>();
        ctrl.SetUsingSkill(true);//正在使用 技能别跑了
        ctrl.DrawSword();//拔剑
        ctrl.NotReady2CloseSword();//不打算收剑
        NavMeshAgent agent = animator.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController ctrl = animator.GetComponent<PlayerController>();
        if (ctrl.IsReady2CloseSword())
        {
            ctrl.SetUsingSkill(false);//技能使用结束 可以跑了
        }
        else
        {
            NavMeshAgent agent = animator.GetComponent<NavMeshAgent>();
            agent.isStopped = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //准备收剑
        PlayerController ctrl = animator.GetComponent<PlayerController>();
        
        ctrl.Ready2CloseSword();//打算收剑
        InputManager.Instance.SetisKeyQWER(false);

        // SkillController ctrl2 = animator.GetComponent<SkillController>();
        // ctrl2.HideEffect();
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
