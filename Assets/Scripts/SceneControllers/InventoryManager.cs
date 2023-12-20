using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class InventoryManager : MonoBehaviour, IRenewal
{
    public enum ItemType
    {
        Weapon,
        Armor,
        SkillCode,
        Mat
    }
    private ItemType curType = ItemType.Weapon;
    public static InventoryManager Instance;

    public GameObject inventoryPanel;

    public ItemButton buttonPrefab;

    [Header("����")]
    public ItemPanel weaponPanel;
    public IconSO weaponIconSO;

    [Space(10.0f)]

    [Header("��")]
    public ItemPanel armorPanel;
    public IconSO armorIconSO;

    [Space(10.0f)]

    [Header("��ų�ڵ�")]
    public SkillCodePanel skillCodePanel;
    public IconSO skillIconSO;

    [Space(10.0f)]

    [Header("���")]
    public MatPanel matPanel;
    public IconSO matIconSo;

    [Space(10.0f)]

    [Header("�ϰ��Ǹ�")]
    public GameObject sellArea;
    public GameObject sellPanel;
    public GameObject sellButton;

    [Space(10.0f)]

    [Header("������ �ؽ�Ʈ")]
    public TextMeshProUGUI moneyText;

    private bool sellMode = false;
    private List<Equip> sellEquipList = new List<Equip>();
    private List<Materials> sellMatList = new List<Materials>();

    private ObjectPool<ItemButton> buttonPool;
    private List<ItemButton> releaseList = new List<ItemButton>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        buttonPool = new ObjectPool<ItemButton>(
            () => // createFunc
        {
            var button = Instantiate(buttonPrefab);
            button.transform.SetParent(inventoryPanel.transform);
            button.OnCountAct();
            button.gameObject.SetActive(false);

            return button;
        },
        delegate (ItemButton button) // actionOnGet
        {
            button.gameObject.SetActive(true);
            button.transform.SetParent(inventoryPanel.transform);
        },
        delegate (ItemButton button) // actionOnRelease
        {
            button.Clear();
            button.transform.SetParent(gameObject.transform); // ItemButton Transform Reset
            button.gameObject.SetActive(false);
        });

        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        //TestAddItem();
        Renewal();
    }

    public void ShowWeapons(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        ClearItemButton();
        curType = ItemType.Weapon;

        var weapons = PlayDataManager.data.WeaponInventory;
        foreach (var weapon in weapons)
        {
            var go = buttonPool.Get();

            go.iconImage.sprite = weaponIconSO.GetSprite(weapon.id / 100 * 100 + 1);
            // weapon icon level reset

            go.iconImage.color = Color.white;
            go.OnEquip(weapon.isEquip);

            go.button.onClick.AddListener(() => 
            {
                if (sellMode && sellEquipList.Count < 10)
                {
                    if (weapon.isEquip) // Were Equip Exception
                    {
                        // Notice
                        MyNotice.Instance.Notice("�Ǹ��� �� �����ϴ�.");
                        return;
                    }

                    if (go.iconImage.color == Color.white)
                    {
                        sellEquipList.Add(weapon);
                        go.iconImage.color = Color.red;

                        var newGo = buttonPool.Get();
                        newGo.iconImage.sprite = weaponIconSO.GetSprite(weapon.id / 100 * 100 + 1);
                        // weapon icon level reset

                        newGo.OnEquip(weapon.isEquip);
                        newGo.transform.SetParent(sellPanel.transform);
                        newGo.button.onClick.AddListener(() => 
                        {
                            buttonPool.Release(go.sell);
                            go.sell = null;
                            sellEquipList.Remove(weapon);
                            go.iconImage.color = Color.white;
                        });

                        go.sell = newGo;
                    }
                    else
                    {
                        buttonPool.Release(go.sell);
                        go.sell = null;
                        sellEquipList.Remove(weapon);
                        go.iconImage.color = Color.white;
                    }
                }
                else
                {
                    weaponPanel.SetItem(weapon);
                    weaponPanel.iconImage.sprite = go.iconImage.sprite;
                    weaponPanel.Renewal();
                }
                
            });

            releaseList.Add(go);
        }
    }

    public void ShowArmors(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        ClearItemButton();
        curType = ItemType.Armor;

        var armors = PlayDataManager.data.ArmorInventory;
        foreach (var armor in armors)
        {
            var go = buttonPool.Get();

            go.iconImage.sprite = armorIconSO.GetSprite(armor.id / 100 * 100 + 1);
            go.iconImage.color = Color.white;
            go.OnEquip(armor.isEquip);

            go.button.onClick.AddListener(() =>
            {
                if (sellMode && sellEquipList.Count < 10)
                {
                    if (go.iconImage.color == Color.white)
                    {
                        sellEquipList.Add(armor);
                        go.iconImage.color = Color.red;

                        var newGo = buttonPool.Get();
                        newGo.iconImage.sprite = weaponIconSO.GetSprite(armor.id / 100 * 100 + 1);
                        newGo.OnEquip(armor.isEquip);
                        newGo.transform.SetParent(sellPanel.transform);
                        newGo.button.onClick.AddListener(() =>
                        {
                            buttonPool.Release(go.sell);
                            go.sell = null;
                            sellEquipList.Remove(armor);
                            go.iconImage.color = Color.white;
                        });

                        go.sell = newGo;
                    }
                    else
                    {
                        buttonPool.Release(go.sell);
                        go.sell = null;
                        sellEquipList.Remove(armor);
                        go.iconImage.color = Color.white;
                    }
                }
                else
                {
                    armorPanel.SetItem(armor);
                    armorPanel.iconImage.sprite = go.iconImage.sprite;
                    armorPanel.Renewal();
                }
                
            });

            releaseList.Add(go);
        }
    }

    public void ShowSkillCodes(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        ClearItemButton();
        curType = ItemType.SkillCode;

        var skillcodes = PlayDataManager.data.SkillCodeInventory;
        foreach (var skillcode in skillcodes) 
        {

        }
    }

    public void ShowMaterials(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        ClearItemButton();
        curType = ItemType.Mat;

        var mats = PlayDataManager.data.MatInventory;
        foreach (var mat in mats)
        {
            var go = buttonPool.Get();
            go.transform.SetParent(inventoryPanel.transform);

            go.OnCountAct(true, mat.count);
            go.iconImage.sprite = matIconSo.GetSprite(mat.id);
            go.OnEquip();

            go.GetComponent<ItemButton>().button.onClick.AddListener(() =>
            {
                if (sellMode && sellMatList.Count < 10)
                {
                    if (go.iconImage.color == Color.white)
                    {
                        sellMatList.Add(mat);
                        go.iconImage.color = Color.red;

                        var newGo = buttonPool.Get();
                        newGo.iconImage.sprite = matIconSo.GetSprite(mat.id);
                        newGo.transform.SetParent(sellPanel.transform);
                        newGo.button.onClick.AddListener(() =>
                        {
                            buttonPool.Release(go.sell);
                            go.sell = null;
                            sellMatList.Remove(mat);
                            go.iconImage.color = Color.white;
                        });

                        go.sell = newGo;
                    }
                    else
                    {
                        buttonPool.Release(go.sell);
                        go.sell = null;
                        sellMatList.Remove(mat);
                        go.iconImage.color = Color.white;
                    }
                }
                else
                {
                    matPanel.SetMaterials(mat);
                    matPanel.iconImage.sprite = go.iconImage.sprite;
                    matPanel.Renewal();
                }
            });

            releaseList.Add(go);
        }
    }

    public void ClearItemButton()
    {
        foreach (var item in releaseList)
        {
            buttonPool.Release(item);
        }

        releaseList.Clear();
    }

    public void SellMode(bool mode)
    {
        sellMode = mode;
        sellArea.SetActive(mode);
        sellButton.SetActive(!mode);

        switch (curType)
        {
            case ItemType.Weapon:
            case ItemType.Armor:
                sellEquipList.Clear();
                break;

            case ItemType.SkillCode:
                // ���� �ʿ�
                break;

            case ItemType.Mat:
                sellMatList.Clear();
                break;
        }

        switch (sellMode)
        {
            case true:

                break;

            case false:
                foreach (var item in releaseList)
                {
                    if (item.sell != null)
                    {
                        buttonPool.Release(item.sell);
                        item.sell = null;

                    }
                    item.iconImage.color = Color.white;
                }
                break;
        }
    }

    public void SellItem()
    {
        switch (curType)
        {
            case ItemType.Weapon:
                {
                    foreach (var item in sellEquipList)
                    {
                        PlayDataManager.SellItem(item as Weapon);
                    }
                    ShowWeapons(true);
                }
                break;

            case ItemType.Armor:
                {
                    foreach (var item in sellEquipList)
                    {
                        PlayDataManager.SellItem(item as Armor);
                    }
                    ShowArmors(true);
                }
                break;

            case ItemType.SkillCode:
                {
                    // ���� �ʿ�
                }
                break;

            case ItemType.Mat:
                {
                    foreach (var item in sellMatList)
                    {
                        PlayDataManager.SellItem(item);
                    }
                    ShowMaterials(true);
                }
                break;
        }
        SellMode(false);

    }

    public void Renewal()
    {
        switch (curType)
        {
            case ItemType.Weapon:
                ShowWeapons(true);
                break;

            case ItemType.Armor:
                ShowArmors(true);
                break;

            case ItemType.SkillCode:
                ShowSkillCodes(true);
                break;

            case ItemType.Mat:
                ShowMaterials(true);
                break;
        }

        moneyText.text = PlayDataManager.data.Gold.ToString();
    }

    public void Tester()
    {
        var table = CsvTableMgr.GetTable<MatTable>().dataTable;
        foreach (var mat in table)
        {
            PlayDataManager.IncreaseMat(mat.Key, 99);
        }

        PlayDataManager.AddGold(100000);
    }
}
