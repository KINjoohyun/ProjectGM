using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCodePanel : MonoBehaviour
{
    [Header("��ų�ڵ� �̸�")]
    [SerializeField]
    private TextMeshProUGUI nameText;

    [Header("������ �̹���")]
    public Image iconImage;

    [Header("�Ǹ� ����")]
    [SerializeField]
    private TextMeshProUGUI sellText;

    [Header("��ų��")]
    [SerializeField]
    private TextMeshProUGUI infoText;

    [Header("����")]
    [SerializeField]
    private TextMeshProUGUI countText;

    private SkillCode skillcode = null;

    public void SetSkillCode(SkillCode skillcode)
    {
        this.skillcode = skillcode;
    }

    public void Renewal()
    {
        gameObject.SetActive(true);

        var table = CsvTableMgr.GetTable<CodeTable>().dataTable;
        var st = CsvTableMgr.GetTable<StringTable>().dataTable;

        nameText.text = st[table[skillcode.id].name];
        infoText.text = st[table[skillcode.id].script];
        sellText.text = table[skillcode.id].sellgold.ToString();
        countText.text = $"{skillcode.count} / {skillcode.Capacity}";
    }

    public void SellItem()
    {
        PlayDataManager.SellItem(skillcode, 1);

        Renewal();
        InventoryManager.Instance.Renewal();

        gameObject.SetActive(PlayDataManager.IsExistItem(skillcode));
    }
}
