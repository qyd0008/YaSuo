using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // public event Action<int,int> UpdateHealthBarOnAttack;
    // public event Action<int,int> UpdateExpBarOnAttack;
    // public event Action<int,bool> BleedingOnAttack;
    public CharacterData_SO templateData;
    public CharacterData_SO characterData;

    void Awake()
    {
        if (templateData != null)
            characterData = Instantiate(templateData);
    }

    #region Read From Data_SO
    public string MyName
    {
        get
        {
            if (characterData != null)
                return characterData.myName;
            return "";
        }
        set
        {
            if (characterData != null)
                characterData.myName = value;
        }
    }
    
    public int MaxHealth
    {
        get
        {
            if (characterData != null)
                return characterData.maxHealth;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.maxHealth = value;
        }
    }
    public int CurrentHealth
    {
        get
        {
            if (characterData != null)
                return characterData.currentHealth;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.currentHealth = value;
        }
    }

    public int KillExp
    {
        get
        {
            if (characterData != null)
                return characterData.killExp;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.killExp = value;
        }
    }

    public int CurrentLevel
    {
        get
        {
            if (characterData != null)
                return characterData.currentLevel;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.currentLevel = value;
        }
    }

    public int BaseExp
    {
        get
        {
            if (characterData != null)
                return characterData.baseExp;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.baseExp = value;
        }
    }

    public int CurrentExp
    {
        get
        {
            if (characterData != null)
                return characterData.currentExp;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.currentExp = value;
        }
    }

    public bool UpdateExp(int point)
    {
        if (characterData != null)
            return characterData.UpdateExp(point);

        return false;
    }
    #endregion

    #region Read From Attack_SO
    public float AttackRange
    {
        get
        {
            if (characterData != null)
                return characterData.attackRange;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.attackRange = value;
        }
    }

    public float AttackCoolDown
    {
        get
        {
            if (characterData != null)
                return characterData.attackCoolDown;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.attackCoolDown = value;
        }
    }

    public int Damage
    {
        get
        {
            if (characterData != null)
                return characterData.damage;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.damage = value;
        }
    }

    public int DamageQ
    {
        get
        {
            if (characterData != null)
                return characterData.damageQ;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.damageQ = value;
        }
    }

    public float CoolDownQ
    {
        get
        {
            if (characterData != null)
                return characterData.coolDownQ;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.coolDownQ = value;
        }
    }

    public int DamageE
    {
        get
        {
            if (characterData != null)
                return characterData.damageE;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.damageE = value;
        }
    }

    public float CoolDownE
    {
        get
        {
            if (characterData != null)
                return characterData.coolDownE;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.coolDownE = value;
        }
    }

    public int DamageW
    {
        get
        {
            if (characterData != null)
                return characterData.damageW;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.damageW = value;
        }
    }

    public float CoolDownW
    {
        get
        {
            if (characterData != null)
                return characterData.coolDownW;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.coolDownW = value;
        }
    }

    public int DamageR
    {
        get
        {
            if (characterData != null)
                return characterData.damageR;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.damageR = value;
        }
    }

    public float CoolDownR
    {
        get
        {
            if (characterData != null)
                return characterData.coolDownR;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.coolDownR = value;
        }
    }

    public float Q1Duration
    {
        get
        {
            if (characterData != null)
                return characterData.q1Duration;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.q1Duration = value;
        }
    }
    public float Q2Duration
    {
        get
        {
            if (characterData != null)
                return characterData.q2Duration;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.q2Duration = value;
        }
    }
    public float Q3Duration
    {
        get
        {
            if (characterData != null)
                return characterData.q3Duration;
            return 0;
        }
        set
        {
            if (characterData != null)
                characterData.q3Duration = value;
        }
    }

    #endregion

    #region Character Combat

    public void TakeDamage(CharacterStats attacker, CharacterStats defener)
    {
        int currentDamage = attacker.Damage;
        defener.CurrentHealth = Mathf.Max(defener.CurrentHealth - currentDamage, 0);


        // defener.BleedingOnAttack?.Invoke(damage,attacker.isCritical);

        Debug.Log(attacker.MyName + "发起攻击，伤害为:" + currentDamage);
        Debug.Log("受击者" + defener.MyName + "剩余血量：" + defener.CurrentHealth);

        //update ui
        // defener.UpdateHealthBarOnAttack?.Invoke(defener.CurrentHealth,defener.MaxHealth);
        //update exp
        // if (defener.CurrentHealth <= 0)
        // {
        //     bool isLevelUp = attacker.UpdateExp(defener.KillPoint);
        //     attacker.UpdateExpBarOnAttack?.Invoke(attacker.CurrentExp,attacker.BaseExp);
        //     //如果升级了 那么更新血条 因为升级血就满了
        //     if (isLevelUp)
        //     {
        //         attacker.UpdateHealthBarOnAttack?.Invoke(attacker.CurrentHealth,attacker.MaxHealth);
        //         attacker.UpdateLevelOnAttack?.Invoke(attacker.CurrentLevel);
        //     }
        // }
    }

    #endregion
}
