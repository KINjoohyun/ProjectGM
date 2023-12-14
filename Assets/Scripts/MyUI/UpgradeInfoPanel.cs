using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UpgradeInfoPanel : MonoBehaviour, IRenewal
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void Renewal()
    {
        PrintAtk();
        PrintDef();
    }

    private void PrintAtk()
    {
        if (PlayDataManager.curWeapon == null)
        {
            text.text = $"[���ݷ�] : 0\n[���� �Ӽ�] : {AttackType.None}";
            return;
        }

        var wt = CsvTableMgr.GetTable<WeaponTable>().dataTable;
        var weapon = wt[PlayDataManager.curWeapon.id];

        // ���ݷ� �ջ��
        var atk = 0.0f;
        if (weapon != null)
        {
            atk = weapon.atk;
        }

        // ���� �Ӽ�
        var attackType = AttackType.None;
        if (weapon != null)
        {
            attackType = weapon.property;
        }

        text.text = $"[���ݷ�] : {atk}\n[���� �Ӽ�] : {attackType}";
    }

    private void PrintDef()
    {
        var at = CsvTableMgr.GetTable<ArmorTable>().dataTable;

        // ���� �ջ��
        var def = 0;
        foreach (var armor in PlayDataManager.curArmor)
        {
            if (armor.Value != null)
            {
                def += at[armor.Value.id].defence;
            }
        }

        text.text += $"\n[����] {def}";
    }
}
