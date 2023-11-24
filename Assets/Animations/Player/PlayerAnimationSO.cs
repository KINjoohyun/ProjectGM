using UnityEngine;

[CreateAssetMenu(menuName = "Player Animation SO")]
public class PlayerAnimationSO : ScriptableObject
{
    [Header("���� �ִϸ��̼�")]
    public AnimatorOverrideController anim_Tonpa;

    [Header("��� �ִϸ��̼�")]
    public AnimatorOverrideController anim_Two_Hand_Sword;

    [Header("�Ѽհ� �ִϸ��̼�")]
    public AnimatorOverrideController anim_One_Hand_Sword;

    [Header("â �ִϸ��̼�")]
    public AnimatorOverrideController anim_Spear;

    public AnimatorOverrideController GetAnimator(Item.WeaponID id)
    {
        switch (id)
        {
            case Item.WeaponID.Simple_Hammer:
            case Item.WeaponID.Gold_Hammer:
                return anim_Tonpa;

            case Item.WeaponID.Go_Work_Sword:
            case Item.WeaponID.Vigil_Sword:
                return anim_Two_Hand_Sword;

            case Item.WeaponID.Glory_Sword:
            case Item.WeaponID.Darkness_Sword:
                return anim_One_Hand_Sword;

            case Item.WeaponID.Simple_Spear:
            case Item.WeaponID.Gold_Spear:
                return anim_Spear;
 
            default:
                return null;
        }
    }
}
