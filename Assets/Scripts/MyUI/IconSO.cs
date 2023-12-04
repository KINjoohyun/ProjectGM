using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "IconSO")]
public class IconSO : ScriptableObject
{
    [Header("��� ID")]
    public int[] IDs;

    [Header("�̹���")]
    public Sprite[] IMAGEs;

    public Sprite GetSprite(int id)
    {
        for (int i = 0; i < IDs.Length; i++)
        {
            if (IDs[i] == id)
            {
                return IMAGEs[i];
            }
        }
        return null;
    }
}
