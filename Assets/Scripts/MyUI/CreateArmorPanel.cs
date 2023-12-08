using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateArmorPanel : MonoBehaviour, IRenewal
{
    [Header("�� �̸� �ؽ�Ʈ")]
    public TextMeshProUGUI nameText;

    [Header("������ �̹���")]
    public Image iconImage;

    [Header("���� �ؽ�Ʈ")]
    public TextMeshProUGUI defText;

    [Header("��ų �ؽ�Ʈ")]
    public TextMeshProUGUI skillsText;

    [Header("��ų �ؽ�Ʈ")]
    public TextMeshProUGUI setSkillText;

    [Header("�䱸 ��� �г�")]
    public RequireMatPanel require;

    [Header("�䱸 ��� �г� ����")]
    public GameObject content;

    [Header("�䱸 �ݾ� �ؽ�Ʈ")]
    public TextMeshProUGUI priceText;

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

        var armor = CsvTableMgr.GetTable<ArmorTable>().dataTable[item.id];
        var st = CsvTableMgr.GetTable<StringTable>().dataTable;
        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;
        var mt = CsvTableMgr.GetTable<MatTable>().dataTable;

        nameText.text = st[armor.Armor_name];

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

        defText.text = armor.def.ToString();
        skillsText.text = $"{armor.skill1_id} Lv.{armor.skill1_lv}";
        priceText.text = $"��� : {ct[item.id].gold}\n������ : {PlayDataManager.data.Gold}";
    }

    public void CraftEquip()
    {
        if (!IsCraftable())
        {
            // ���� �Ұ���
            return;
        }

        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;
        var armor = new Armor(item.id);

        PlayDataManager.Purchase(ct[item.id].gold);
        PlayDataManager.DecreaseMat(ct[item.id].mf_module, ct[item.id].number_1);
        PlayDataManager.data.ArmorInventory.Add(armor);
        PlayDataManager.Save();

        UpgradeManager.Instance.ShowArmors(true);
        gameObject.SetActive(false);
    }

    private bool IsCraftable()
    {
        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;

        var mat = PlayDataManager.data.MatInventory.Find(x => x.id == ct[item.id].mf_module);
        if (mat == null)
        {
            Debug.Log("Not Exist Materials");
            return false;
        }

        if (mat.count < ct[item.id].number_1)
        {
            Debug.Log("Lack Of Materials Count");
            return false;
        }

        if (PlayDataManager.data.Gold < ct[item.id].gold)
        {
            Debug.Log("Lack Of Gold");
            return false;
        }
        // �κ��丮 ���� ���� (���� �߰� �ʿ�)


        return true;
    }
}
