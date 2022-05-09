using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { IDLE, CHASE, DEAD }

public class ZombieController : Role
{
    public GameObject attackTarget;
    private EnemyStates enemyStates;
    private float lastAttackTime;//cd时间 用来记录上次攻击
    private float waitTime = 1.0f;
    private float passWaitTime;
    bool playerDead; //玩家是否死了
    bool isAttack;

    bool isFlying;
    // Start is called before the first frame update
    void Start()
    {
        enemyStates = EnemyStates.CHASE;
        attackTarget = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        isDead = characterStats.CurrentHealth == 0;

        if (!playerDead)
        {
            SwitchStates();
            SwitchAnimation();
            lastAttackTime -= Time.deltaTime;
        }   

        if (passWaitTime>0)
        {
            passWaitTime -= Time.deltaTime;
        }
        else
        {
            if (isFlying)
            {
                if (transform.position.y <= -0.1f)
                {
                    agent.enabled = true;
                    // agent.isStopped = false;
                    isFlying = false;
                }
            }
        }
    }

    void SwitchAnimation()
    {
        anim.SetBool("Attack", isAttack);
        anim.SetFloat("Speed",agent.velocity.sqrMagnitude);
    }

    void SwitchStates()
    {
        switch (enemyStates)
        {
            case EnemyStates.IDLE:

                break;
            case EnemyStates.CHASE: //追击
                isAttack = false;
                agent.isStopped = false;
                agent.destination = attackTarget.transform.position;

                //在攻击范围内则攻击
                if (TargetInAttackRange())
                {
                    agent.isStopped = true;

                    if (lastAttackTime < 0)
                    {
                        lastAttackTime = characterStats.AttackCoolDown;
                        //执行攻击
                        Attack();
                    }
                }
                break;
            case EnemyStates.DEAD: //死亡
                coll.enabled = false;
                // agent.enabled = false;
                agent.radius = 0;
                Destroy(gameObject, 2.5f);
                break;
        }
    }

    bool TargetInAttackRange()
    {
        if (attackTarget != null)
            return Vector3.Distance(attackTarget.transform.position,transform.position) <= characterStats.AttackRange;
        return false;
    }

    void Attack()
    {
        transform.LookAt(attackTarget.transform);
        isAttack = true;
    }

    public bool IsFlying()
    {
        return isFlying;
    }

    public void Flying()
    {
        isFlying = true;
        // agent.isStopped = true;
        agent.enabled = false;
        if (rig == null)
        {
            AddRigidbody(2f,2f);
        }
        rig.AddForce(Vector3.up * 30f,ForceMode.Impulse);
        passWaitTime = waitTime;
    }

    public void Downing()
    {
        rig.AddForce(Vector3.down * 25f,ForceMode.Impulse);
    }


}
