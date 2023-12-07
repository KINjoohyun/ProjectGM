using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UpgradeManager : MonoBehaviour
{
    [Header("���� ��� ��ư��")]
    public List<CurrentItemButton> equipButtons;

    [Header("�÷��̾� ����")]
    public UpgradeInfoPanel upgradeInfoPanel;

    [Header("����/�� ���� �г�")]
    public CreateWeaponPanel createWeaponPanel;
    public CreateArmorPanel createArmorPanel;

    [Space(10.0f)]

    public UpgradeEquipButton buttonPrefab;

    public GameObject content;

    private ObjectPool<UpgradeEquipButton> buttonPool;
    private List<UpgradeEquipButton> releaseList = new List<UpgradeEquipButton>();

    private void Awake()
    {
        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        
    }

    private void Start()
    {
        buttonPool = new ObjectPool<UpgradeEquipButton>(
            () => // createFunc
            {
                var button = Instantiate(buttonPrefab);
                button.gameObject.SetActive(false);

                return button;
            },
        delegate (UpgradeEquipButton button) // actionOnGet
        {
            button.gameObject.SetActive(true);
        },
        delegate (UpgradeEquipButton button) // actionOnRelease
        {
            button.iconImage.sprite = null;
            button.gameObject.SetActive(false);
        });

        foreach (var renewal in equipButtons)
        {
            renewal.Renewal();
        }
        upgradeInfoPanel.Renewal();

        ShowWeapons(true);
    }

    public void ShowWeapons(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        Clear();

        var inv = PlayDataManager.data.WeaponInventory;
        foreach (var item in inv)
        {
            var go = buttonPool.Get();
            go.transform.SetParent(content.transform);

            go.SetEquip(item);
            go.UpgradeMode(this);
            go.Renewal();

            releaseList.Add(go);
        }

        var ct = CsvTableMgr.GetTable<CraftTable>().dataTable;
        foreach (var data in ct)
        {
            if (data.Value.mf_module != -1)
            {
                var item = new Weapon(data.Key);
                var go = buttonPool.Get();
                go.transform.SetParent(content.transform);

                go.SetEquip(item);
                go.CreateMode(this);
                go.Renewal();

                releaseList.Add(go);
            }
            
        }
    }

    public void ShowArmors(bool isOn)
    {
        if (!isOn)
        {
            return;
        }
        Clear();


    }

    public void Clear()
    {
        foreach (var item in releaseList)
        {
            buttonPool.Release(item);
        }

        releaseList.Clear();
    }
}
