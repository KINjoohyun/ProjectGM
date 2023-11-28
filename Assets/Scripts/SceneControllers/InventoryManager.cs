using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel;

    public GameObject buttonPrefab;

    [Header("����/��")]
    public GameObject itemPanel;
    public TextMeshProUGUI itemPanelInfoText;
    private Item curItem = null;

    [Header("�����")]
    public GameObject decoPanel;
    private List<Item> sellList = new List<Item>();

    [Header("���")]
    public GameObject matPanel;

    private void Start()
    {
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
            var weapon = new Item(Item.ItemType.Weapon, (int)Item.WeaponID.Simple_Hammer + i);
            PlayDataManager.data.Inventory[Item.ItemType.Weapon].Add(weapon);

            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < 3; i++)
        {
            var armor = new Item(Item.ItemType.Armor, (int)Item.ArmorID.HMD + i);
            PlayDataManager.data.Inventory[Item.ItemType.Armor].Add(armor);

            yield return new WaitForEndOfFrame();
        }

        PlayDataManager.Save();
    }

    public void ShowWeapons()
    {
        ClearItemButton();

        var weapons = PlayDataManager.data.Inventory[Item.ItemType.Weapon];
        foreach (var weapon in weapons)
        {
            var go = Instantiate(buttonPrefab);
            go.transform.SetParent(inventoryPanel.transform);

            var button = go.GetComponent<Button>();
            button.onClick.AddListener(() => 
            {
                itemPanel.SetActive(true);
                itemPanelInfoText.text = ((Item.WeaponID)weapon.id).ToString();

                curItem = weapon;
            });

            var text = go.GetComponentInChildren<TextMeshProUGUI>();
            text.text = ((Item.WeaponID)weapon.id).ToString();
        }
    }

    public void ShowArmors()
    {
        ClearItemButton();

        var armors = PlayDataManager.data.Inventory[Item.ItemType.Armor];
        foreach (var armor in armors)
        {
            var go = Instantiate(buttonPrefab);
            go.transform.SetParent(inventoryPanel.transform);

            var button = go.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                itemPanel.SetActive(true);
                itemPanelInfoText.text = ((Item.ArmorID)armor.id).ToString();

                curItem = armor;
            });

            var text = go.GetComponentInChildren<TextMeshProUGUI>();
            text.text = ((Item.ArmorID)armor.id).ToString();
        }
    }

    public void ShowDecorations()
    {
        ClearItemButton();

    }

    public void ShowMaterials()
    {
        ClearItemButton();

    }

    public void ClearItemButton()
    {
        curItem = null;

        var arr = inventoryPanel.GetComponentsInChildren<Button>();
        if (arr != null)
        {
            foreach (var item in arr)
            {
                Destroy(item.gameObject);
            }
        }
    }

    public void EquipItem()
    {
        if (curItem == null)
        {
            return;
        }

        PlayDataManager.WearItem(curItem);
    }

    
}
