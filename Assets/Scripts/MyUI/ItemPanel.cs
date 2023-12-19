using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour, IRenewal
{ 
    [Header("������ �̹���")]
    public Image iconImage;

    [Header("���� ��ư")]
    public Button equipButton;

    [Header("�ؽ�Ʈ ����")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI additionalText;

    [Header("���׷��̵� �г�")]
    public UpgradeEquipPanel upgradePanel;

    public Equip item = null;

    public void Renewal()
    {
        gameObject.SetActive(true);

        var st = CsvTableMgr.GetTable<StringTable>().dataTable;

        switch (item.type)
        {
            case Equip.EquipType.Weapon:
                {
                    var table = CsvTableMgr.GetTable<WeaponTable>().dataTable[item.id];
                    //iconImage.sprite = ;
                    nameText.text = st[table.name];
                    valueText.text = table.atk.ToString();
                    additionalText.text = table.property.ToString();
                }

                break;

            case Equip.EquipType.Armor:
                {
                    var table = CsvTableMgr.GetTable<ArmorTable>().dataTable[item.id];
                    //iconImage.sprite = ;

                    nameText.text = st[table.name];
                    valueText.text = table.defence.ToString();
                    additionalText.text = $"[��Ʈȿ��] {table.set_skill_id}";
                }

                break;
        }

        equipButton.gameObject.SetActive(!item.isEquip);

        /*
        if (item.isEquip)
        {
            valueText.color = Color.red;
        }
        else
        {
            valueText.color = Color.white;
        }
        */
    }

    public void SetItem(Equip item)
    {
        this.item = item;
        
    }

    public void WearItem()
    {
        if (item == null)
        {
            return;
        }

        PlayDataManager.WearItem(item);
        switch (item.type)
        {
            case Equip.EquipType.Weapon:
                InventoryManager.Instance.ShowWeapons(true);

                break;

            case Equip.EquipType.Armor:
                InventoryManager.Instance.ShowArmors(true);

                break;
        }

        Renewal();
    }

    public void UpgradeItem()
    {
        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;
        if (!ct.ContainsKey(item.id + 1))
        {
            MyNotice.Instance.Notice("��ȭ�� ������ �� �����ϴ�.");
            return;
        }

        upgradePanel.SetEquip(item);
        upgradePanel.SetIconImage(iconImage.sprite);
        upgradePanel.SetItemPanel(this);
        upgradePanel.Renewal();
    }
}
