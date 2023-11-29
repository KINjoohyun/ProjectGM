using UnityEngine;

public enum AttackType
{
    None = -1,

    Hit, // Ÿ����
    Slash, // ������
    Pierce, // ������

}

public class Weapon : MonoBehaviour, IEquip
{
    public Item item = null;
    public PlayerAnimationSO animationSO;
    [Header("���� �Ӽ�"),Tooltip("None: -, Hit: Ÿ��, Slash: ����, Pierce: ����")]
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
            (int)Item.WeaponID.Simple_Hammer => AttackType.Hit,
            (int)Item.WeaponID.Gold_Hammer => AttackType.Hit,

            // Slash
            (int)Item.WeaponID.Go_Work_Sword => AttackType.Slash,
            (int)Item.WeaponID.Vigil_Sword => AttackType.Slash,

            (int)Item.WeaponID.Glory_Sword => AttackType.Slash,
            (int)Item.WeaponID.Darkness_Sword => AttackType.Slash,

            // Pierce
            (int)Item.WeaponID.Simple_Spear => AttackType.Pierce,
            (int)Item.WeaponID.Gold_Spear => AttackType.Pierce,

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

        anim.runtimeAnimatorController = animationSO.GetAnimator((Item.WeaponID)item.id);
    }
}
