using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum PlayerStates {IDLE,NORMAL_RUN,S}
public class PlayerController : Role
{
    private GameObject attackTarget;

    bool isDrawSword;//剑是否是拔出状态
    bool isNormalRun;
    bool isSwordRun;
    bool isAttack;//是否在普通攻击
    bool isReady2CloseSword;//是否准备开始收剑
    bool isUsingSkill;//是否正在使用技能
    

    private float lastQTime;
    protected override void Awake()
    {
        base.Awake();
        //animx相关
        isNormalRun = false;
        isSwordRun = false;
        isAttack = false;
        isReady2CloseSword = false;
        //其他标志位
        isDrawSword = false;
        isUsingSkill = false;
    }

    void Start()
    {
        InputManager.Instance.OnMouseClicked += MoveToTarget;
        InputManager.Instance.OnEnemyClicked += EventAttack;
    }

    void Update()
    {
        switchAnimation();
    }

    //Animation
    private void switchAnimation()
    {
        anim.SetBool("Attack",isAttack);
        anim.SetBool("Normal Run",isNormalRun);
        anim.SetBool("Sword Run",isSwordRun);
        anim.SetFloat("Speed",agent.velocity.sqrMagnitude);
        anim.SetBool("Ready Close Sword",isReady2CloseSword);
    }

    #region 关于移动内容
    private void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        if (IsDeath()) return;

        StartCoroutine(MoveToGroundTarget(target));
    }

    private void CheckState()
    {
        agent.isStopped = false;
        isAttack = false;
        
        if (IsReady2CloseSword())
        {
            isSwordRun = false;
            isNormalRun = false;
        }
        else
        {
            if (isUsingSkill)
                return;
            isSwordRun = isDrawSword;
            isNormalRun = !isDrawSword;
        }
    }

    IEnumerator MoveToGroundTarget(Vector3 target)
    {
        CheckState();

        //根据agent.stoppingDistance移动人物
        while (Vector3.Distance(target,transform.position) > agent.stoppingDistance)
        {
             agent.destination = target;
             yield return null;
        }

        agent.isStopped = true;

        isNormalRun = false;
        isSwordRun = false;
        isAttack = false;
    }

    private void EventAttack(GameObject target)
    {
        StopAllCoroutines();
        if (IsDeath()) return;

        if (target != null)
        {
            attackTarget = target;
            StartCoroutine(MoveToAttackTarget());
        }
    }

    IEnumerator MoveToAttackTarget()
    {
        CheckState();

        transform.LookAt(attackTarget.transform);

        //根据攻击范围移动人物
        while (Vector3.Distance(attackTarget.transform.position,transform.position) > characterStats.AttackRange)
        {
             agent.destination = attackTarget.transform.position;
             yield return null;
        }

        agent.isStopped = true;

        isNormalRun = false;
        isSwordRun = false;
        isAttack = true;
    }

    public void SetNormalRun(bool temp)
    {
        isNormalRun = temp;
    }

    public void SetSwordRun(bool temp)
    {
        isSwordRun = temp;
    }

    #endregion
    
    #region 关于是否拔剑设置
    public void DrawSword()
    {
        isDrawSword = true;
    }

    public void CloseSword()
    {
        isDrawSword = false;
    }

    public bool IsDrawSword()
    {
        return isDrawSword;
    }
    #endregion

    #region 关于是否准备收剑设置
    public void Ready2CloseSword()
    {
        isReady2CloseSword = true;
    }

    public void NotReady2CloseSword()
    {
        isReady2CloseSword = false;
    }

    public bool IsReady2CloseSword()
    {
        return isReady2CloseSword;
    }

    #endregion

    #region 关于技能模块

    public void SetUsingSkill(bool temp)
    {
        //这里使用技能 需要打断普通攻击 和移动
        isSwordRun = false;
        isNormalRun = false;
        isAttack = false;
        isUsingSkill = temp;
    }

    #endregion
}
