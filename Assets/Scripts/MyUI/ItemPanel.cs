using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour, IRenewal
{ 
    [Header("������ �̹���")]
    public Image iconImage;

    [Header("�ؽ�Ʈ ����")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI statText;
    public TextMeshProUGUI infoText;

    public Equip item = null;

    public void Renewal()
    {
        gameObject.SetActive(true);

        if (item.isEquip)
        {
            statText.color = Color.red;
        }
        else
        {
            statText.color = Color.white;
        }
    }

    public void SetItem(Equip item)
    {
        this.item = item;
        var st = CsvTableMgr.GetTable<StringTable>().dataTable;

        switch (item.type)
        {
            case Equip.EquipType.Weapon:
                {
                    var table = CsvTableMgr.GetTable<WeaponTable>().dataTable[(Weapon.WeaponID)item.id];
                    //iconImage.sprite = ;
                    nameText.text = st[table.weapon_name];
                    statText.text = $"[���ݷ�] {table.atk}\n[����Ӽ�] {table.property}";
                    infoText.text = table.weapon_name.ToString(); // string table
                }
                
                break;

            case Equip.EquipType.Armor:
                {
                    var table = CsvTableMgr.GetTable<ArmorTable>().dataTable[(Armor.ArmorID)item.id];
                    //iconImage.sprite = ;

                    nameText.text = st[table.Armor_name];
                    statText.text = $"[����] {table.def}\n[����] {table.Armor_type}";
                    infoText.text = $"[��Ʈȿ��] {table.set_skill}";
                }

                break;
        }
    }

    public void WearItem()
    {
        if (item == null)
        {
            return;
        }

        PlayDataManager.WearItem(item);

        Renewal();
    }
}
