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
        var skt = CsvTableMgr.GetTable<SkillTable>().dataTable;
        var st = CsvTableMgr.GetTable<StringTable>().dataTable;

        nameText.text = st[code.name];

        infoText.text = (code.skill1_id != -1) ? 
            $"{st[skt[code.skill1_id].name]}\tLv.{code.skill1_lv}\n" : 
            string.Empty;

        infoText.text += (code.skill2_id != -1) ? 
            $"{st[skt[code.skill2_id].name]}\tLv.{code.skill2_lv}\n" : 
            string.Empty;
    }
}
