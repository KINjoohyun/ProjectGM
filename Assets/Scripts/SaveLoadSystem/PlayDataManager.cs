using System.Collections.Generic;
using SaveDataVC = SaveDataV3; // Version Change?

public static class PlayDataManager
{
    public static SaveDataVC data;

    public static Weapon curWeapon = null;

    public static readonly Dictionary<Armor.ArmorType, Armor> curArmor
        = new Dictionary<Armor.ArmorType, Armor>();

    public static void Init()
    {
        data = SaveLoadSystem.Load("savefile.json") as SaveDataVC;
        if (data == null)
        {
            data = new SaveDataVC();

            // �⺻ ���� 4�� ����
            //data.WeaponInventory.Add(new Weapon(Weapon.WeaponID.Simple_Tonpa_Lv1));
            //data.WeaponInventory.Add(new Weapon(Weapon.WeaponID.Go_Work_Sword_Lv1));
            data.WeaponInventory.Add(new Weapon(Weapon.WeaponID.Glory_Sword_Lv1));
            data.WeaponInventory.Add(new Weapon(Weapon.WeaponID.Simple_Spear_Lv1));
        }
        SaveLoadSystem.Save(data, "savefile.json");

        // ���� �κ��丮�� ��ȸ�ؼ� curWeapon�� �Ҵ�
        foreach (var weapon in data.WeaponInventory)
        {
            if (weapon.isEquip)
            {
                curWeapon = weapon;
                break;
            }
        }

        // �� �κ��丮�� ��ȸ�ؼ� curArmor 5���� �Ҵ�
        for (Armor.ArmorType i = Armor.ArmorType.Head; i <= Armor.ArmorType.Glove; i++)
        {
            curArmor.Add(i, null);
        }

        foreach (var armor in data.ArmorInventory)
        {
            if (armor.isEquip)
            {
                curArmor[armor.armorType] = armor;
            }
        }
    }

    public static void Save()
    {
        SaveLoadSystem.Save(data, "savefile.json");
    }

    public static void Reset()
    {
        data = new SaveDataVC();
        curWeapon = null;
        curArmor.Clear();
        Save();
    }

    public static bool Purchase(int pay)
    {
        if (pay > data.Gold)
        {
            return false;
        }

        data.Gold -= pay;
        Save();
        return true;
    }

    public static void UnlockQuest(int quest)
    {
        if (data.Quest != quest)
        {
            return;
        }

        data.Quest++;
        Save();
    }

    public static void WearItem(Item item)
    {
        if (item == null)
        {
            // Null Exception
            return;
        }

        switch (item.type)
        {
            case Item.ItemType.Weapon:
                var weapon = item as Weapon;

                if (curWeapon == null)
                {
                    curWeapon = weapon;
                    curWeapon.isEquip = true;

                    break;
                }

                if (curWeapon.instanceID == weapon.instanceID)
                {
                    return;
                }

                curWeapon.isEquip = false;
                curWeapon = weapon;
                curWeapon.isEquip = true;

                break;

            case Item.ItemType.Armor:
                var armor = item as Armor;

                if (curArmor[armor.armorType] == null)
                {
                    curArmor[armor.armorType] = armor;
                    curArmor[armor.armorType].isEquip = true;

                    break;
                }

                if (curArmor[armor.armorType].instanceID == armor.instanceID)
                {
                    return;
                }

                curArmor[armor.armorType].isEquip = false;
                curArmor[armor.armorType] = armor;
                curArmor[armor.armorType].isEquip = true;

                break;

            default:
                return;
        }
        Save();
    }

    public static void UnWearItem(Item.ItemType type, Armor.ArmorType armorType = Armor.ArmorType.None)
    {
        switch (type)
        {
            case Item.ItemType.Weapon:
                curWeapon.isEquip = false;
                curWeapon = null;
                break;

            case Item.ItemType.Armor:
                curArmor[armorType].isEquip = false;
                curArmor[armorType] = null;
                break;

            default:
                return;
        }
        Save();
    }
}