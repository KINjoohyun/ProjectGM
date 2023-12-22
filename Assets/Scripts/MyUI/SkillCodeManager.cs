using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SkillCodeManager : MonoBehaviour, IRenewal
{
    //[Header("��ų�ڵ� �г�")]
    //public SkillCodePanel skillCodePanel
    [Header("��ų�ڵ� IconSO")]
    [SerializeField]
    private IconSO skillcodeIconSO;

    [Header("������ Content")]
    [SerializeField]
    private GameObject equipContent;

    [Header("�κ��丮 Content")]
    [SerializeField]
    private GameObject invContent;

    [Header("��ư ������")]
    [SerializeField]
    private ItemButton buttonPrefab;

    [Header("��ݹ�ư ������")]
    [SerializeField]
    private LockerButton lockPrefab;

    [Header("��ų�ڵ� ���� �г�")]
    [SerializeField]
    private SkillCodeEquipPanel equipPanel;

    private ObjectPool<ItemButton> buttonPool;
    private List<ItemButton> releaseList = new List<ItemButton>();

    private ObjectPool<LockerButton> lockPool;
    private List<LockerButton> lockList = new List<LockerButton>();

    private void Awake()
    {
        buttonPool = new ObjectPool<ItemButton>
            (() => // createFunc
            {
                var button = Instantiate(buttonPrefab, invContent.transform);
                button.Clear();
                button.gameObject.SetActive(false);

                return button;
            },
            delegate (ItemButton button) // actionOnGet
            {
                button.gameObject.SetActive(true);
                button.transform.SetParent(invContent.transform, true);
            },
            delegate (ItemButton button) // actionOnRelease
            {
                button.Clear();
                button.transform.SetParent(gameObject.transform, true); // ItemButton Transform Reset
                button.gameObject.SetActive(false);
            });

        lockPool = new ObjectPool<LockerButton>
            (() => // createFunc
            {
                var go = Instantiate(lockPrefab, equipContent.transform);
                go.gameObject.SetActive(false);
                return go;
            },
            delegate (LockerButton go) // actionOnGet
            {
                go.gameObject.SetActive(true);
                go.transform.SetParent(equipContent.transform, true);
            },
            delegate (LockerButton go) // actionOnRelease
            {
                go.transform.SetParent(gameObject.transform, true);
                go.gameObject.SetActive(false);
            });

        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        Renewal();
    }

    public void ShowAll()
    {
        ClearItemButton();

        var table = CsvTableMgr.GetTable<CodeTable>().dataTable;
        foreach (var code in PlayDataManager.data.CodeInventory)
        {
            var go = buttonPool.Get();

            go.iconImage.sprite = skillcodeIconSO.GetSprite(table[code.id].type);
            go.OnCountAct(true, code.count);

            go.button.onClick.AddListener(() =>
            {
                equipPanel.iconImage.sprite = go.iconImage.sprite;
                equipPanel.SetSkillCode(code);
                equipPanel.EquipMode();
                equipPanel.Renewal();
            });
            releaseList.Add(go);
        }
    }

    public void ShowEquip()
    {
        ClearLock();

        var table = CsvTableMgr.GetTable<CodeTable>().dataTable;
        foreach (var id in PlayDataManager.data.SkillCodes)
        {
            var code = new SkillCode(id, 1);
            var go = buttonPool.Get();

            go.iconImage.sprite = skillcodeIconSO.GetSprite(table[id].type);
            go.transform.SetParent(equipContent.transform, true);
            go.OnCountAct();

            go.button.onClick.AddListener(() =>
            {
                equipPanel.iconImage.sprite = go.iconImage.sprite;
                equipPanel.SetSkillCode(code);
                equipPanel.EquipMode(false);
                equipPanel.Renewal();
            });
            releaseList.Add(go);
        }

        // Left Lock
        var left = PlayDataManager.GetSocket() - PlayDataManager.data.SkillCodes.Count;
        for (int i = left; i > 0; i--)
        {
            var go = lockPool.Get();
            go.LockMode(false);

            lockList.Add(go);
        }
        // Full Lock
        for (int i = 15 - PlayDataManager.GetSocket(); i > 0; i--)
        {
            var go = lockPool.Get();
            go.LockMode();

            lockList.Add(go);
        }
    }

    private void Clear()
    {
        ClearItemButton();
        ClearLock();
    }

    private void ClearItemButton()
    {
        foreach (var item in releaseList)
        {
            buttonPool.Release(item);
        }

        releaseList.Clear();
    }

    private void ClearLock()
    {
        foreach (var item in lockList)
        {
            lockPool.Release(item);
        }

        lockList.Clear();
    }

    public void Renewal()
    {
        Clear();

        ShowAll(); // test code
        ShowEquip();
    }

    
    private void OnEnable()
    {
        Renewal();
    }
}
