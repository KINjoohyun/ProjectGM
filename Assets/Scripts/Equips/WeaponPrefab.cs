using UnityEngine;

public class WeaponPrefab : MonoBehaviour, IEquip
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

    public void OnEquip()
    {
        // Define AttackType
        type = item.attackType;
    }

    public void OnEquip(Item item)
    {
        this.item = item as Weapon;
        OnEquip();
    }

    public void OnEquip(Item item, Animator anim)
    {
        OnEquip(item);

        anim.runtimeAnimatorController = animationSO.GetAnimator((Weapon.WeaponID)item.id);
    }
}
