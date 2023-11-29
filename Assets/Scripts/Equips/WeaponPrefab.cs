using UnityEngine;

public class WeaponPrefab : MonoBehaviour, IEquip
{
    public Item item = null;

    public PlayerAnimationSO animationSO;

    [Header("���� �Ӽ�"), Tooltip("None: -, Hit: Ÿ��, Slash: ����, Pierce: ����")]
    public AttackType type = AttackType.None;

    [Header("���ݷ�")]
    public float attack;

    [Header("��Ÿ�")]
    public float attackRange = 2f;

    [Header("�Ӽ� ����")]
    public float weakDamage;

    public void OnEquip()
    {
        // Define AttackType
        type = item.id switch
        {
            // Hit
            (int)Weapon.WeaponID.Simple_Hammer => AttackType.Hit,
            (int)Weapon.WeaponID.Gold_Hammer => AttackType.Hit,

            // Slash
            (int)Weapon.WeaponID.Go_Work_Sword => AttackType.Slash,
            (int)Weapon.WeaponID.Vigil_Sword => AttackType.Slash,

            (int)Weapon.WeaponID.Glory_Sword => AttackType.Slash,
            (int)Weapon.WeaponID.Darkness_Sword => AttackType.Slash,

            // Pierce
            (int)Weapon.WeaponID.Simple_Spear => AttackType.Pierce,
            (int)Weapon.WeaponID.Gold_Spear => AttackType.Pierce,

            _ => AttackType.None,
        };
    }

    public void OnEquip(Item item)
    {
        this.item = item;
        OnEquip();
    }

    public void OnEquip(Item item, Animator anim)
    {
        OnEquip(item);

        anim.runtimeAnimatorController = animationSO.GetAnimator((Weapon.WeaponID)item.id);
    }
}
