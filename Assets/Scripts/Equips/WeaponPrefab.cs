using UnityEngine;

public class WeaponPrefab : MonoBehaviour, IWear
{
    public Weapon item = null;

    public PlayerAnimationSO animationSO;

    [Header("���� �Ӽ�"), Tooltip("None: -, Hit: Ÿ��, Slash: ����, Pierce: ����")]
    public AttackType type = AttackType.None;

    [Header("���ݷ�")]
    public float attack;

    [Header("��Ÿ�")]
    public float attackRange = 2f;

    [Header("�Ӽ� ����")]
    public float weakDamage;
    
    public bool IsDualWield
    {
        get
        {
            if (item.weaponType == WeaponType.Tonpa)
            {
                return true;
            }
            return false;
        }
    }

    public void OnEquip(Weapon item)
    {
        type = item.attackType;

        var table = CsvTableMgr.GetTable<WeaponTable>().dataTable[(Weapon.WeaponID)item.id];
        attack = table.atk;
        weakDamage = table.weakpoint;
    }

    public void OnEquip(Weapon item, Animator anim)
    {
        OnEquip(item);

        anim.runtimeAnimatorController = animationSO.GetAnimator((Weapon.WeaponID)item.id);
    }
}
