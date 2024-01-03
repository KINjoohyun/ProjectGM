using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("��ų SO")]
    [SerializeField]
    private SkillSO skillSO;
    
    private void Awake()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }    

        foreach (var item in PlayDataManager.curSkill)
        {
            var skill = skillSO.GetSkill(item.Key, item.Value);
            skill.transform.SetParent(transform, false);
        }

        var setinfo = PlayDataManager.curSetSkill;
        {
            var skill = skillSO.GetSkill(setinfo.id, setinfo.level);
            if (skill != null)
            {
                skill.transform.SetParent(transform, false);
            }
        }

        var player = GetComponent<Player>();
        if (player != null) 
        {
            InGameManager.Instance.SetHP(player.HP, player.Stat.HP);
        }
    }
}
