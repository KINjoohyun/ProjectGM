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

    public AnimatorOverrideController GetAnimator(Weapon.WeaponID id)
    {
        var table = CsvTableMgr.GetTable<WeaponTable>().dataTable;
        switch (table[id].type)
        {
            case 1: // ����
                return anim_Tonpa;

            case 2: // �μհ�
                return anim_Two_Hand_Sword;

            case 3: // �Ѽհ�
                return anim_One_Hand_Sword;

            case 4: // â
                return anim_Spear;

            default:
                return null;
        }
    }
}
