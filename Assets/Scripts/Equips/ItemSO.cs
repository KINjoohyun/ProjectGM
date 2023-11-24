using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("������ ����")]
    public Item.ItemType Type;

    [Header("������ ID")]
    public int[] ID;

    [Header("������ ������")]
    public GameObject[] Prefab;

    public GameObject MakeItem(Item item)
    {
        // Item.ItemType Exception
        if (item.type == Item.ItemType.None)
        {
            Debug.LogWarning("Wrong Item Type!");
            return null;
        }

        int index = -1;
        for (int i = 0; i < ID.Length; i++)
        {
            if (ID[i] == item.id)
            {
                index = i;
                break;
            }
        }

        // Item index Exception
        if (index < 0)
        {
            Debug.LogWarning("Not Exist Item!");
            return null;
        }

        var go = Instantiate(Prefab[index]);
        return go;
    }

    public GameObject MakeItem(Item item, Transform tr)
    {
        var go = MakeItem(item);
        go.transform.SetParent(tr, false);
        return go;
    }

    public Weapon MakeItem(Item item, Transform tr, Animator anim)
    {
        var go = MakeItem(item, tr);
        var weapon = go.GetComponent<Weapon>();
        weapon.OnEquip(item, anim);
        return weapon;
    }
}
