using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class CraftManager : MonoBehaviour
{
    public static CraftManager Instance;

    /*
    [Header("���� ��� ��ư��")]
    public List<CurrentItemButton> equipButtons;

    [Header("�÷��̾� ����")]
    public UpgradeInfoPanel upgradeInfoPanel;
    */

    [Header("����/�� ���� �г�")]
    public CreateWeaponPanel createWeaponPanel;
    public CreateArmorPanel createArmorPanel;

    /*
    [Header("����/�� ���׷��̵� �г�")]
    public UpgradeEquipPanel upgradeWeaponPanel;
    public UpgradeEquipPanel upgradeArmorPanel;
    */

    [Space(10.0f)]

    public CraftEquipButton buttonPrefab;

    public GameObject content;

    private ObjectPool<CraftEquipButton> buttonPool;
    private List<CraftEquipButton> releaseList = new List<CraftEquipButton>();

    private void Awake()
    {
        Instance = this;

        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        
    }

    private void Start()
    {
        buttonPool = new ObjectPool<CraftEquipButton>(
            () => // createFunc
            {
                var button = Instantiate(buttonPrefab);
                button.transform.SetParent(content.transform, false);
                button.gameObject.SetActive(false);

                return button;
            },
        delegate (CraftEquipButton button) // actionOnGet
        {
            button.gameObject.SetActive(true);
            button.transform.SetParent(content.transform, false);
        },
        delegate (CraftEquipButton button) // actionOnRelease
        {
            button.iconImage.sprite = null;
            button.transform.SetParent(transform, false);
            button.gameObject.SetActive(false);
        });

        /*
        foreach (var renewal in equipButtons)
        {
            renewal.Renewal();
        }
        upgradeInfoPanel.Renewal();
        */

        ShowWeapons(true);
    }

    public void ShowWeapons(bool isOn = true)
    {
        if (!isOn)
        {
            return;
        }
        Clear();
        createArmorPanel.gameObject.SetActive(false);

        /*
        var inv = PlayDataManager.data.WeaponInventory;
        foreach (var item in inv)
        {
            var go = buttonPool.Get();

            go.SetEquip(item);
            go.UpgradeMode(this);
            go.Renewal();

            releaseList.Add(go);
        }
        */

        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;
        foreach (var data in ct)
        {
            if (data.Value._class != Equip.EquipType.Weapon)
            {
                continue;
            }

            if (data.Value.mf_module != -1)
            {
                var item = new Weapon(data.Key);
                var go = buttonPool.Get();

                go.SetEquip(item);
                go.CreateMode(this);
                go.Renewal();

                releaseList.Add(go);
            }
            
        }
    }

    public void ShowArmors(bool isOn = true)
    {
        if (!isOn)
        {
            return;
        }
        Clear();
        createWeaponPanel.gameObject.SetActive(false);

        /*
        var inv = PlayDataManager.data.ArmorInventory;
        foreach (var item in inv)
        {
            var go = buttonPool.Get();

            go.SetEquip(item);
            go.UpgradeMode(this);
            go.Renewal();

            releaseList.Add(go);
        }
        */

        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;
        foreach (var data in ct)
        {
            if (data.Value._class != Equip.EquipType.Armor)
            {
                continue;
            }

            if (data.Value.mf_module != -1)
            {
                var item = new Armor(data.Key);
                var go = buttonPool.Get();

                go.SetEquip(item);
                go.CreateMode(this);
                go.Renewal();

                releaseList.Add(go);
            }

        }
        
    }

    public void Clear()
    {
        foreach (var item in releaseList)
        {
            buttonPool.Release(item);
        }

        releaseList.Clear();
    }

    private void OnDisable()
    {
        createWeaponPanel.gameObject.SetActive(false);
        createArmorPanel.gameObject.SetActive(false);
    }
}
