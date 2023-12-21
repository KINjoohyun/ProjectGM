using UnityEngine;

[CreateAssetMenu(menuName = "Player Animation SO")]
public class PlayerAnimationSO : ScriptableObject
{
    [Header("���� �ִϸ��̼�")]
    public RuntimeAnimatorController anim_Tonpa;

    [Header("��� �ִϸ��̼�")]
    public RuntimeAnimatorController anim_Two_Hand_Sword;

    [Header("�Ѽհ� �ִϸ��̼�")]
    public RuntimeAnimatorController anim_One_Hand_Sword;

    [Header("â �ִϸ��̼�")]
    public RuntimeAnimatorController anim_Spear;

    public RuntimeAnimatorController GetAnimator(int id)
    {
        var table = CsvTableMgr.GetTable<WeaponTable>().dataTable;
        switch (table[id].type)
        {
            case WeaponType.Tonpa: // ����
                return anim_Tonpa;

            case WeaponType.Two_Hand_Sword: // �μհ�
                return anim_Two_Hand_Sword;

            case WeaponType.One_Hand_Sword: // �Ѽհ�
                return anim_One_Hand_Sword;

            case WeaponType.Spear: // â
                return anim_Spear;

            default:
                return null;
        }
    }
}
