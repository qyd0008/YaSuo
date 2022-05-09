using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPanel : MonoBehaviour
{
    public GameObject player;
    public Image canUseRImage;
    public List<Image> cdImage;
    public List<Text> cdText;

    SkillController skillController;
    void Awake()
    {
        skillController = player.GetComponent<SkillController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateSkillCDTime();
        UpdateCanUseRImage();
    }

    private void UpdateCanUseRImage()
    {
        Dictionary<SkillType,Skill> skillTable = skillController.GetSkillTable();
        // 当前场景内有被击飞的僵尸 并且 不冷却
        canUseRImage.gameObject.SetActive(GameManager.Instance.HasFlyingZombie() && skillTable[SkillType.R].ColdPassedTime<=0.0f);
    }

    private void UpdateSkillCDTime()
    {
        Dictionary<SkillType,Skill> skillTable = skillController.GetSkillTable();

        foreach (KeyValuePair<SkillType, Skill> skill in skillTable)
        {
            if (skill.Value.ColdPassedTime > 0)
            {
                cdImage[(int)skill.Key].gameObject.SetActive(true);
                cdText[(int)skill.Key].gameObject.SetActive(true);

                float sliderPercent = (float)skill.Value.ColdPassedTime / skill.Value.ColdTime;
                cdImage[(int)skill.Key].fillAmount = sliderPercent;

                cdText[(int)skill.Key].text = String.Format("{0:N1}", skill.Value.ColdPassedTime);
            }
            else
            {
                cdImage[(int)skill.Key].gameObject.SetActive(false);
                cdText[(int)skill.Key].gameObject.SetActive(false);
            }
        }
    }
}
