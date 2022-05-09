using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    string name;//技能名称
    string describe;//技能描述
    int damage;//技能伤害
    int level;//技能等级
    float coldTime;//冷却时间
    float coldPassedTime;//当前度过的冷却时间

    #region 技能属性获取
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public string Describe
    {
        get
        {
            return describe;
        }
        set
        {
            describe = value;
        }
    }
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
    public float ColdTime
    {
        get
        {
            return coldTime;
        }
        set
        {
            coldTime = value;
        }
    }
    public float ColdPassedTime
    {
        get
        {
            return coldPassedTime;
        }
        set
        {
            coldPassedTime = value;
        }
    }

    #endregion
}

public enum SkillType { Q, W, E, R }
public enum SkillQType { Q1, Q2, Q3}

public class SkillController : Role
{
    [Header("Skill Q")]
    public Transform q1EffectTransform;
    public GameObject q1EffectPrefab;
    public Transform q2EffectTransform;
    public GameObject q2EffectPrefab;
    public Transform q2WindEffectTransform;
    public GameObject q2WindEffectPrefab;//Q2命中后 人物身上起风特效
    public Transform q3EffectTransform;
    public GameObject q3EffectPrefab;

    //检测Q碰撞到的敌人
    /*
        public static int BoxCastNonAlloc (
            Vector3 center, 
            Vector3 halfExtents, 
            Vector3 direction, 
            RaycastHit[] results, 
            Quaternion orientation= Quaternion.identity, 
            float maxDistance= Mathf.Infinity, 
            int layerMask= DefaultRaycastLayers, 
            QueryTriggerInteraction queryTriggerInteraction= QueryTriggerInteraction.UseGlobal);
    */
    public Transform rayTransform;
    RaycastHit[] m_Results = new RaycastHit[20];//随便存多少个吧
    bool isQHit;//当前Q是否命中敌人

    private GameObject q2WindEffect;
    private float qDurationTime;//当前Q持续时间 时间到了Q的状态要返回skillQType.Q1

    [Header("Skill e")]
    public Transform eqEffectTransform;
    public GameObject eqEffectPrefab;
    public GameObject eqEffectPrefabWind;
    public float eqForce;

    //检测Q碰撞到的敌人
    /*
        public static int SphereCastNonAlloc (
            Vector3 origin, 
            float radius, 
            Vector3 direction, 
            RaycastHit[] results, 
            float maxDistance= Mathf.Infinity, 
            int layerMask= DefaultRaycastLayers, 
            QueryTriggerInteraction queryTriggerInteraction= QueryTriggerInteraction.UseGlobal);
    */
    public Transform rayEQTransform;

    [Header("Skill R")]
    public Transform rEffectTransform;
    public GameObject rEffectPrefab;
    

    private GameObject curShowEffect;

    Dictionary<SkillType,Skill> skillTable;//技能表

    void Start()
    {
        InputManager.Instance.OnKeyboardDown += EventUseSkill;
        InitSkillData();
    }

    private void InitSkillData()
    {
        skillTable = new Dictionary<SkillType, Skill>();

        Skill Q = new Skill();
        Q.Name = "斩钢闪";
        Q.Damage = characterStats.DamageQ;
        Q.ColdTime = characterStats.CoolDownQ;
        Q.ColdPassedTime = 0;
        skillTable.Add(SkillType.Q,Q);

        Skill W = new Skill();
        W.Name = "风之障壁";
        W.Damage = characterStats.DamageW;
        W.ColdTime = characterStats.CoolDownW;
        W.ColdPassedTime = 0;
        skillTable.Add(SkillType.W,W);
        
        Skill E = new Skill();
        E.Name = "踏前斩";
        E.Damage = characterStats.DamageE;
        E.ColdTime = characterStats.CoolDownE;
        E.ColdPassedTime = 0;
        skillTable.Add(SkillType.E,E);

        Skill R = new Skill();
        R.Name = "狂风绝息斩";
        R.Damage = characterStats.DamageR;
        R.ColdTime = characterStats.CoolDownR;
        R.ColdPassedTime = 0;
        skillTable.Add(SkillType.R,R);
    }

    public Dictionary<SkillType,Skill> GetSkillTable()
    {
        return skillTable;
    }

    void Update()
    {
        UpdateSkillColdTime();
        UpdateSkillQDurationTime();
    }

    private void UpdateSkillColdTime()
    {
        foreach (KeyValuePair<SkillType, Skill> skill in skillTable)
        {
            if (skill.Value.ColdPassedTime > 0)
            {
                skill.Value.ColdPassedTime -= Time.deltaTime;
            }
        }
    }

    private void EventUseSkill(SkillType skillType,Vector3 mousePosition)
    {
        //如果还在冷却时间内 不可以使用该技能
        if (skillTable[skillType].ColdPassedTime > 0)
        {
            Debug.Log("该技能还在冷却");
            return;
        }
            
        agent.velocity = new Vector3(0f,0f,0f);//圆圆解决的BUG 移动过程中 释放技能 需要重置速度 

        switch (skillType)
        {
            case SkillType.Q:
                //面向鼠标方向 释放技能
                transform.LookAt(mousePosition);
                anim.SetTrigger("Skill Q");
                break;
            case SkillType.W:
                //面向鼠标方向 释放技能
                transform.LookAt(mousePosition);
                anim.SetTrigger("Skill W");
                break;
            case SkillType.E:
                //面向鼠标方向 释放技能
                transform.LookAt(mousePosition);
                // Debug.Log("AddForce");
                Vector3 direction = (mousePosition - transform.position + Vector3.up).normalized;
                rig.AddForce(direction * eqForce,ForceMode.Impulse);
                anim.SetTrigger("Skill E");
                break;
            case SkillType.R:
                GameManager.Instance.StopAllFlyingZombie();
                Vector3 dire = GameManager.Instance.GetFlyingZombieDirection();

                transform.position = new Vector3(dire.x-2f,dire.y,dire.z+1.5f);
                transform.LookAt(dire);

                anim.SetTrigger("Skill R");
                break;
        }
        
        skillTable[skillType].ColdPassedTime = skillTable[skillType].ColdTime;
    }
    
    void ShowEffect(GameObject prefab, Transform skillTransform)
    {
        Quaternion rotation = skillTransform.rotation;
        curShowEffect = Instantiate(prefab,skillTransform.position,rotation);
        Destroy(curShowEffect,2);
    }

    public void HideEffect()
    {
        if(curShowEffect)
        {
            curShowEffect.SetActive(false);
            Destroy(curShowEffect,2);
        }
    }

    void Q1Effect()
    {
        ShowEffect(q1EffectPrefab, q1EffectTransform);
    }

    void Q2Effect()
    {
        ShowEffect(q2EffectPrefab, q2EffectTransform);
    }

    public void EQEffect()
    {
        ShowEffect(eqEffectPrefab, eqEffectTransform);
    }

    public void EQWindEffect()
    {
        ShowEffect(eqEffectPrefabWind, eqEffectTransform);
    }

    public void ShowQ2WindEffect()//展示Q2命中敌人后 身上起风的特效
    {
        AudioManager.Instance.PlaySwordSound("Sound/wind");
        q2WindEffect = Instantiate(q2WindEffectPrefab,q2WindEffectTransform);
    }

    public void HideQ2WindEffect()//隐藏身上起风的特效
    {
        Destroy(q2WindEffect);
    }

    void Q3Effect()
    {
        AudioManager.Instance.PlaySwordSound("Sound/q3_wind");
        ShowEffect(q3EffectPrefab, q3EffectTransform);
    }

    void REffect()
    {
        ShowEffect(rEffectPrefab, rEffectTransform);
    }

    //使用Q技能 Q技能状态不断变化
    public void useQSkill()
    {
        
        SkillQType qType = (SkillQType)anim.GetFloat("Skill Q Type");
        
        switch ((SkillQType)qType)
        {
            case SkillQType.Q1:
                if (isQHit)//命中敌人才能积攒风
                {
                    qDurationTime = characterStats.Q1Duration;
                    qType = SkillQType.Q2;
                }
                break;
            case SkillQType.Q2:
                if (isQHit)//命中敌人才能积攒风
                {
                    qDurationTime = characterStats.Q2Duration;
                    qType = SkillQType.Q3;
                    ShowQ2WindEffect();
                }
                break;
            case SkillQType.Q3:
                qDurationTime = characterStats.Q3Duration;
                qType = SkillQType.Q1;
                break;
        }

        isQHit = false;
        anim.SetFloat("Skill Q Type",(float)qType);
    }

    void UpdateSkillQDurationTime() // 当前Q持续时间 时间到了Q的状态要返回skillQType.Q1
    {
        if (qDurationTime > 0)
        {
            qDurationTime -= Time.deltaTime;
        }
        else
        {
            if (q2WindEffect)
            {
                HideQ2WindEffect();
            }
            anim.SetFloat("Skill Q Type",(float)SkillQType.Q1);
        }
    }

    void CheckBoxCastNonAlloc() //Q了以后 看看Q命中了吗？命中并且对敌人造成伤害
    {
        //清空数据
        // System.Array.Clear(m_Results,0,10);

        Ray ray = new Ray(rayTransform.position, rayTransform.right);
        Debug.DrawRay(ray.origin, ray.direction * 2.3f, Color.red,5);
        int hits = Physics.BoxCastNonAlloc(rayTransform.position,new Vector3(0.5f,1.15f,0.5f), rayTransform.right, m_Results,Quaternion.identity,2.3f);
        int hitEnemyCount = 0;
        for (int i = 0; i < hits; i++)
        {
            if (m_Results[i].collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Hit " + m_Results[i].collider.gameObject.name);
                hitEnemyCount++;
                isQHit = true;
                //TODO:造成傷害
            }
        }
        if (hitEnemyCount == 0)
        {
            Debug.Log("Did not hit");
        }
        else
        {
            AudioManager.Instance.PlaySwordSound("Sound/q_hit");
        }
    }

    void CheckSphereCastNonAlloc() //EQ了以后 看看EQ命中了吗？命中并且对敌人造成伤害
    {
        //清空数据
        // System.Array.Clear(m_Results,0,10);

        Ray ray = new Ray(rayEQTransform.position, rayEQTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 1.5f, Color.red,5);
        
        int hits = Physics.SphereCastNonAlloc(rayEQTransform.position,2f,rayEQTransform.forward,m_Results,1.5f);
        int hitEnemyCount = 0;

        //当前是Q几？
        SkillQType qType = (SkillQType)anim.GetFloat("Skill Q Type");

        Debug.Log("hits " + hits);
        for (int i = 0; i < hits; i++)
        {
            Debug.Log("Hit " + m_Results[i].collider.gameObject.name);
            if (m_Results[i].collider.gameObject.tag == "Enemy")
            {
                GameObject zombie = m_Results[i].collider.gameObject;
                // Debug.Log("Hit " + zombie.name);
                hitEnemyCount++;
                isQHit = true;
                //TODO:造成傷害

                if (qType == SkillQType.Q3)
                {
                    //击飞
                    ZombieController ctrl = zombie.GetComponent<ZombieController>();
                    Debug.Log("击飞 " + zombie.name);
                    ctrl.Flying();
                }
            }
        }
        if (hitEnemyCount == 0)
        {
            Debug.Log("Did not hit");
        }
        else
        {
            AudioManager.Instance.PlaySwordSound("Sound/q_hit");
        }

        
    }
}
