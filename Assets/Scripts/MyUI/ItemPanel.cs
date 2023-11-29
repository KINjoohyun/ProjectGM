using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ItemPanel : MonoBehaviour, IRenewal
{ 
    [Header("������ �̹���")]
    public Image iconImage;

    [Header("�ؽ�Ʈ ����")]
    //public TextMeshProUGUI nameText;
    public TextMeshProUGUI statText;
    public TextMeshProUGUI infoText;

    public Item item = null;

    public void Renewal()
    {
        gameObject.SetActive(true);

        infoText.text = ((WeaponID)item.id).ToString();
        statText.text = $"instanceID : {item.instanceID} \ntype : {item.type}";
        if (item.isEquip)
        {
            statText.color = Color.red;
        }
        else
        {
            statText.color = Color.white;
        }
    }

    public void SetItem(Item item)
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

        Renewal();
    }
}
