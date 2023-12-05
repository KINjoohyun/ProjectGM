using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentItemButton : MonoBehaviour, IRenewal
{
    [Header("������ �з�")]
    public Equip.EquipType Type;

    [Header("�� �з�"), Tooltip("�������� ������ NoneŸ������ ������ ��")]
    public Armor.ArmorType armorType;

    [Header("���� IconSO")]
    public IconSO weaponIconSO;

    [Header("�� IconSO")]
    public IconSO armorIconSO;

    private Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }

    public void Renewal()
    {
        switch (Type)
        {
            case Equip.EquipType.Weapon:
                if (PlayDataManager.curWeapon == null)
                {
                    iconImage.sprite = null;

                    break;
                }
                iconImage.sprite = weaponIconSO.GetSprite(PlayDataManager.curWeapon.id);

                break;

            case Equip.EquipType.Armor:
                if (PlayDataManager.curArmor[armorType] == null)
                {
                    iconImage.sprite = null;

                    break;
                }
                iconImage.sprite = armorIconSO.GetSprite(PlayDataManager.curArmor[armorType].id);
                break;

            default:
                Debug.LogWarning("Not Exist Item.ItemType! - CurrentItemButton");
                break;
        }
    }

    public void UnWear()
    {
        PlayDataManager.UnWearItem(Type, armorType);
        Renewal();
    }
}
