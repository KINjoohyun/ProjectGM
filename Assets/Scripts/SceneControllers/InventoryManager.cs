using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    public ItemButton buttonPrefab;

    [Header("무기/방어구")]
    public ItemPanel itemPanel;

    [Header("장식주")]
    public GameObject decoPanel;

    [Header("재료")]
    public MatPanel matPanel;

    [Header("재료 IconSO")]
    public IconSO matIconSo;

    [Header("일괄판매")]
    public GameObject sellPanel;
    private Equip[] sellList = new Equip[10]; // 최대 판매 개수
    private bool sellMode = false;

    private ObjectPool<ItemButton> buttonPool;
    private List<ItemButton> releaseList = new List<ItemButton>();

    private void Start()
    {
        buttonPool = new ObjectPool<ItemButton>(() => 
        {
            var button = Instantiate(buttonPrefab);
            button.OnCountAct();
            button.gameObject.SetActive(false);
            return button;
        },
        delegate (ItemButton button)
        {
            button.gameObject.SetActive(true);
        },
        delegate (ItemButton button)
        {
            button.OnCountAct();
            button.iconImage.sprite = null;
            button.button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        });

        if (PlayDataManager.data == null)
        {
            PlayDataManager.Init();
        }

        //TestAddItem();
        ShowWeapons();
    }

    private void TestAddItem()
    {
        StartCoroutine(AllAddTester());
    }

    private IEnumerator AllAddTester()
    {
        for (int i = 0; i < 8; i++)
        {
            var weapon = new Weapon(Weapon.WeaponID.Simple_Tonpa_Lv1 + i * 200);
            PlayDataManager.data.WeaponInventory.Add(weapon);
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < 5; i++)
        {
            var armor = new Armor(Armor.ArmorID.HMD + i);
            PlayDataManager.data.ArmorInventory.Add(armor);

            yield return new WaitForEndOfFrame();
        }

        PlayDataManager.Save();

        ShowWeapons();
    }

    public void ShowWeapons()
    {
        ClearItemButton();

        var weapons = PlayDataManager.data.WeaponInventory;
        foreach (var weapon in weapons)
        {
            var go = buttonPool.Get();
            go.transform.SetParent(inventoryPanel.transform);

            go.button.onClick.AddListener(() => 
            {
                if (sellMode)
                {
                    //sellList.Add(weapon);

                }
                else
                {
                    itemPanel.SetItem(weapon);
                    itemPanel.iconImage.sprite = go.iconImage.sprite;
                    itemPanel.Renewal();
                }
                
            });

            releaseList.Add(go);
        }
    }

    public void ShowArmors()
    {
        ClearItemButton();

        // Object Pool로 최적화할 것
        var armors = PlayDataManager.data.ArmorInventory;
        foreach (var armor in armors)
        {
            var go = buttonPool.Get();
            go.transform.SetParent(inventoryPanel.transform);

            go.button.onClick.AddListener(() =>
            {
                if (sellMode)
                {

                }
                else
                {
                    itemPanel.SetItem(armor);
                    itemPanel.iconImage.sprite = go.iconImage.sprite;
                    itemPanel.Renewal();
                }
                
            });

            releaseList.Add(go);
        }
    }

    public void ShowDecorations()
    {
        ClearItemButton();

    }

    public void ShowMaterials()
    {
        ClearItemButton();

        var mats = PlayDataManager.data.MatInventory;
        foreach (var mat in mats)
        {
            var go = buttonPool.Get();
            go.transform.SetParent(inventoryPanel.transform);

            go.OnCountAct(true);
            go.SetCount(mat.count);
            go.iconImage.sprite = matIconSo.GetSprite(mat.id);

            go.GetComponent<ItemButton>().button.onClick.AddListener(() =>
            {
                // if (sellMode)
                matPanel.SetMaterials(mat);
                matPanel.iconImage.sprite = go.iconImage.sprite;
                matPanel.Renewal();
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

    public void SellMode()
    {
        if (!sellMode)
        {
            sellMode = true;

            sellPanel.SetActive(true);
        }
    }

    public void Tester()
    {
        PlayDataManager.data.MatInventory.Add(new Materials(71001));
        PlayDataManager.data.MatInventory.Add(new Materials(72001));
    }
}
