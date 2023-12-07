using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateWeaponPanel : MonoBehaviour, IRenewal
{
    [Header("���� �̸� �ؽ�Ʈ")]
    public TextMeshProUGUI nameText;

    [Header("������ �̹���")]
    public Image iconImage;

    [Header("���ݷ� �ؽ�Ʈ")]
    public TextMeshProUGUI atkText;

    [Header("���� �Ӽ� �ؽ�Ʈ")]
    public TextMeshProUGUI attackTypeText;

    [Header("�䱸 ��� �г�")]
    public RequireMatPanel require;

    [Header("�䱸 ��� �г� ����")]
    public GameObject content;

    private Equip item = null;

    public void SetEquip(Equip item)
    {
        this.item = item;
    }

    public void Renewal()
    {
        gameObject.SetActive(true);

        var cons = content.GetComponentsInChildren<RequireMatPanel>();
        foreach (var c in cons)
        {
            Destroy(c.gameObject);
        }

        var weapon = CsvTableMgr.GetTable<WeaponTable>().dataTable[item.id];
        var st = CsvTableMgr.GetTable<StringTable>().dataTable;
        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;
        var mt = CsvTableMgr.GetTable<MatTable>().dataTable;

        nameText.text = st[weapon.weapon_name];

        if (ct[item.id].mf_module != -1) // �䱸 ��Ḷ�� �б�
        {
            var go = Instantiate(require, content.transform);
            var mat = PlayDataManager.data.MatInventory.Find(x => x.id == ct[item.id].mf_module);
            var count = 0;
            if (mat != null)
            {
                count = mat.count;
            }
            go.matText.text = st[mt[ct[item.id].mf_module].item_name];
            go.SetSlider(count, ct[item.id].number_1);
            go.Renewal();
        }
    }

    public void CraftEquip()
    {
        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;

        if (!IsCraftable())
        {
            // ���� �Ұ���
            return;
        }
    }

    private bool IsCraftable()
    {
        return false;
    }
}
