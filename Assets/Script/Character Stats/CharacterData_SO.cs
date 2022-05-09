using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data ")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public string myName; //名称
    public int maxHealth; //最大血量
    public int currentHealth; //当前血量

    [Header("Attack Info")]
    public float attackRange; //普通攻击距离
    public float attackCoolDown; //普通攻击冷却时间 只有怪物有
    public int damage; //普通攻击伤害
    public int damageQ; //Q技能伤害
    public float coolDownQ; //Q技能CD
    public float q1Duration;//Q1 持续时间
    public float q2Duration;//Q2 持续时间
    public float q3Duration;//Q3 持续时间
    public int damageW; //W技能伤害 0
    public float coolDownW; //W技能CD
    public int damageE; //E技能伤害
    public float coolDownE; //E技能CD
    public int damageR; //R技能伤害
    public float coolDownR; //R技能CD


    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;

    [Header("Kill")]
    public int killExp; //击杀后获得经验值

    public float LevelMultiplier
    {
        get { return 1 + (currentLevel - 1) * levelBuff; }
    }

    public bool UpdateExp(int point)
    {
        currentExp += point;

        if (currentExp >= baseExp)
        {
            currentExp = 0;
            LevelUp();
            return true;
        }

        return false;
    }

    private void LevelUp()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1,0,maxLevel);

        baseExp += (int)(baseExp*LevelMultiplier);

        maxHealth = (int)(maxHealth * LevelMultiplier);
        currentHealth = maxHealth;

        Debug.Log("Level UP!");
    }
}
