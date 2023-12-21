using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCodeEquipPanel : MonoBehaviour, IRenewal
{
    [Header("�̸� �ؽ�Ʈ")]
    [SerializeField]
    private TextMeshProUGUI nameText;

    [Header("������ �̹���")]
    public Image iconImage;

    [Header("��ų��")]
    [SerializeField]
    private TextMeshProUGUI infoText;

    [Header("���� ��ư")]
    [SerializeField]
    private Button equipButton;

    [Header("���� ��ư")]
    [SerializeField]
    private Button unEquipButton;

    private SkillCode skillcode = null;

    public void EquipMode(bool isOn = true)
    {
        equipButton.gameObject.SetActive(isOn);
        unEquipButton.gameObject.SetActive(!isOn);
    }

    public void SetSkillCode(SkillCode code)
    {
        skillcode = code;
    }

    public void EquipItem()
    {
        if (!PlayDataManager.EquipSkillCode(skillcode))
        {
            MyNotice.Instance.Notice("��ų�ڵ带 ������ �� �����ϴ�.");
        }
    }

    public void UnEquipItem()
    {
        PlayDataManager.UnEquipSkillCode(skillcode.id);
    }

    public void Renewal()
    {
        gameObject.SetActive(true);

        var code = CsvTableMgr.GetTable<CodeTable>().dataTable[skillcode.id];
        var st = CsvTableMgr.GetTable<StringTable>().dataTable;

        nameText.text = st[code.name];

        //��ų ���
        infoText.text = (code.skill1_id != -1) ? $"{code.skill1_id}\tLv.{code.skill1_lv}\n" : string.Empty;
        infoText.text += (code.skill2_id != -1) ? $"{code.skill2_id}\tLv.{code.skill2_lv}\n" : string.Empty;
    }
}
