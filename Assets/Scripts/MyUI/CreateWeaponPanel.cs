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

        atkText.text = weapon.atk.ToString();
        attackTypeText.text = weapon.property.ToString();
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
        var weapon = new Weapon(item.id);

        PlayDataManager.Purchase(ct[item.id].gold);
        PlayDataManager.DecreaseMat(ct[item.id].mf_module, ct[item.id].number_1);
        PlayDataManager.data.WeaponInventory.Add(weapon);
        PlayDataManager.Save();

        UpgradeManager.Instance.ShowWeapons(true);
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
