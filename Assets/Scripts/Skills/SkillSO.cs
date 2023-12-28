using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SkillSO")]
public class SkillSO : ScriptableObject
{
    [Header("��ų ID")]
    [SerializeField]
    private List<int> ID;

    [Header("��ų")]
    [SerializeField]
    private List<Skill> SKILL;

    public Skill GetSkill(int id, int level)
    {
        var index = ID.FindIndex(x => x == id);
        if (index == -1)
        {
            return null;
        }
        var skill = Instantiate(SKILL[index]);
        skill.SetSkill(id, level);

        return skill;
    }
}