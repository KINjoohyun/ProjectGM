using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Animation SO")]
public class PlayerAnimationSO : ScriptableObject
{
    [Header("���� �ִϸ��̼�")]
    public AnimatorController anim_Tonpa;

    [Header("��� �ִϸ��̼�")]
    public AnimatorController anim_Two_Hand_Sword;

    [Header("�Ѽհ� �ִϸ��̼�")]
    public AnimatorController anim_One_Hand_Sword;

    [Header("â �ִϸ��̼�")]
    public AnimatorController anim_Spear;

    public AnimatorController GetAnimator(int id)
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
